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

    [HideInInspector] public Entity Caster;

    private List<WayPoint> _selectedpoints = new List<WayPoint>();
    private List<WayPoint> _targetPoints = new List<WayPoint>();


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
                if (!Data.BypassNearSpell)
                {
                    TargetPoints.Add(GraphMaker.Instance.PointDict[origin.SnapOnGrid()]);
                }
                break;
        }

        return TargetPoints;
    }

    public void StartSelectionPreview()
    {
        Caster.CanMove = false;
        foreach (WayPoint selectedPoint in ReadSelectionForm())
        {
            _selectedpoints.Add(selectedPoint);

            selectedPoint.OnHovered += StartSpellPreview;
            selectedPoint.OnNotHovered += StopSpellPreview;
            selectedPoint.OnClicked += Execute;

            selectedPoint.ApplySelectVisual();
        }
    }

    public void StartSpellPreview(WayPoint selectedPoint)
    {
        Debug.Log("start spell preview");
        foreach(WayPoint targetPoint in ReadTargetForm(selectedPoint))
        {
            _targetPoints.Add(targetPoint);
            
            targetPoint.ApplyTargetVisual();
        }
    }

    public void StopSelectionPreview()
    {
        foreach(WayPoint selectedPoint in _selectedpoints)
        {
            selectedPoint.OnHovered -= StartSpellPreview;
            selectedPoint.OnNotHovered -= StopSpellPreview;
            selectedPoint.OnClicked -= Execute;

            selectedPoint.ApplyDefaultVisual();
        }
        _selectedpoints.Clear();

        StopSpellPreview();
        Caster.CanMove = true;
    }

    public void StopSpellPreview()
    {
        Debug.Log("Stop Spell Review");
        foreach(WayPoint targetPoint in _targetPoints)
        {
            if (_selectedpoints.Contains(targetPoint))
            {
                targetPoint.ApplySelectVisual();
            }
            else
            {
                targetPoint.ApplyDefaultVisual();
            }
        }
        _targetPoints.Clear();
    }

    public virtual async void Execute(WayPoint origin)
    {
        WayPoint[] targets = _targetPoints.ToArray();
        StopSelectionPreview();

        await ShowVisuals(origin);

        foreach (WayPoint target in targets)
       {
            ApplySpell();
       }
    }

    protected virtual void ApplySpell()
    {

    }

    protected virtual async Task ShowVisuals(WayPoint target)
    {
        //faire les visuals et les await bien comme un bon toutou de merde
    }
}
