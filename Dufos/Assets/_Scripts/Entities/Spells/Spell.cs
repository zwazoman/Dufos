using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public class Spell
{
    [SerializeField] public SpellData Data;

    [SerializeField] public SpellVisual Visual;

    [HideInInspector] public Entity Caster;

    protected List<WayPoint> ReadSelectionForm()
    {
        Vector3 origin = Caster.transform.position;

        List<WayPoint> SelectionPoints = new List<WayPoint>();

        switch (Data.LaunchForm)
        {
            case SelectionForm.Ray:
                foreach(Vector3 direction in Tools.AllFlatDirections)
                {
                    foreach(RaycastHit hit in Physics.RaycastAll(origin + direction * Data.SelectionBypassSize ,direction,Data.SelectionMaxRange - Data.SelectionBypassSize, LayerMask.GetMask("Ground")))
                    {
                        SelectionPoints.Add(hit.collider.GetComponent<WayPoint>());
                    }
                }

                break;
            case SelectionForm.Sphere:
                foreach (Collider hit in Physics.OverlapSphere(origin, Data.SelectionMaxRange, LayerMask.GetMask("Ground")))
                {
                    SelectionPoints.Add(hit.GetComponent<WayPoint>());
                    foreach (Collider removeHit in Physics.OverlapSphere(origin, Data.SelectionBypassSize, LayerMask.GetMask("Ground")))
                    {
                        SelectionPoints.Remove(removeHit.GetComponent<WayPoint>());
                    }
                }
                break;
            case SelectionForm.Targeted:
                //choper tous les ennemis ou players et selectionner leurs currentpoints
                break;
        }

        return SelectionPoints;
    }

    protected List<WayPoint> ReadTargetForm(WayPoint selectedPoint)
    {
        Vector3 origin = selectedPoint.transform.position;

        List<WayPoint> TargetPoints = new List<WayPoint>();

        if (!Data.BypassNearSpell)
        {
            TargetPoints.Add(GraphMaker.Instance.PointDict[origin.SnapOnGrid()]);
        }

        switch (Data.TargetForm)
        {
            case SpellForm.Ray:
                foreach (Vector3 direction in Tools.AllFlatDirections)
                {
                    foreach (RaycastHit hit in Physics.RaycastAll(origin + direction * Data.SpellBypassSize, direction, Data.SpellMaxRange - Data.SpellBypassSize, LayerMask.GetMask("Ground")))
                    {
                        TargetPoints.Add(hit.collider.GetComponent<WayPoint>());
                    }
                }
                if (!Data.BypassNearSpell)
                {
                    TargetPoints.Add(GraphMaker.Instance.PointDict[origin.SnapOnGrid()]);
                }
                break;
            case SpellForm.Sphere:
                foreach (Collider hit in Physics.OverlapSphere(origin, Data.SpellMaxRange, LayerMask.GetMask("Ground")))
                {
                    TargetPoints.Add(hit.GetComponent<WayPoint>());
                    foreach (Collider removeHit in Physics.OverlapSphere(origin, Data.SpellBypassSize, LayerMask.GetMask("Ground")))
                    {
                        TargetPoints.Remove(removeHit.GetComponent<WayPoint>());
                    }
                }
                break;
            case SpellForm.Point:
                break;
        }

        return TargetPoints;
    }

    public void StartSelectionPreview()
    {
        Debug.Log("start selection preview");
        Caster.CanInteract = false;
        List<WayPoint> test = ReadSelectionForm();
        foreach (WayPoint selectedPoint in test)
        {
            GraphMaker.Instance.SelectedPoints.Add(selectedPoint);

            selectedPoint.OnHovered += StartSpellPreview;
            selectedPoint.OnNotHovered += StopSpellPreview;
            selectedPoint.OnClicked += Execute;

            selectedPoint.ApplySelectVisual();
        }
        Debug.Log(GraphMaker.Instance.SelectedPoints.Count);
    }

    public void StartSpellPreview(WayPoint selectedPoint)
    {
        Debug.Log("start spell preview");
        foreach(WayPoint targetPoint in ReadTargetForm(selectedPoint))
        {
            GraphMaker.Instance.TargetPoints.Add(targetPoint);
            
            targetPoint.ApplyTargetVisual();
        }
    }

    public void StopSelectionPreview()
    {
        Debug.Log("stop selection preview");

        foreach(WayPoint selectedPoint in GraphMaker.Instance.SelectedPoints)
        {
            Debug.Log(selectedPoint);
            selectedPoint.OnHovered -= StartSpellPreview;
            selectedPoint.OnNotHovered -= StopSpellPreview;
            selectedPoint.OnClicked -= Execute;

            selectedPoint.ApplyDefaultVisual();
        }
        GraphMaker.Instance.SelectedPoints.Clear();

        StopSpellPreview();
        Caster.CanInteract = true;
    }

    public void StopSpellPreview()
    {
        Debug.Log("Stop Spell Review");
        foreach(WayPoint targetPoint in GraphMaker.Instance.TargetPoints)
        {
            if (GraphMaker.Instance.SelectedPoints.Contains(targetPoint))
            {
                targetPoint.ApplySelectVisual();
            }
            else
            {
                targetPoint.ApplyDefaultVisual();
            }
        }
        GraphMaker.Instance.TargetPoints.Clear();
    }

    public virtual async void Execute(WayPoint origin)
    {
        Debug.Log("execute");
        WayPoint[] targets = GraphMaker.Instance.TargetPoints.ToArray();
        StopSelectionPreview();

        if(Visual != null)
        {
            await Visual.ShowVisuals(origin);
        }

        foreach (WayPoint target in targets)
        {
            ApplySpell();
        }
    }

    protected virtual void ApplySpell()
    {

    }
}
