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
                    foreach(RaycastHit hit in Physics.RaycastAll(origin + direction * Data.SelectionBypassSize ,direction,Data.SelectionMaxRange, LayerMask.GetMask("Ground")))
                    {
                        Debug.Log("selectionne");
                        SelectionPoints.Add(hit.collider.GetComponent<WayPoint>());
                    }
                }

                break;
            case SelectionForm.Sphere:
                Debug.Log("Sphere");
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
                Debug.Log("targeted");
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
                    foreach (RaycastHit hit in Physics.RaycastAll(origin + direction * Data.SpellBypassSize, direction, Data.SpellMaxRange, LayerMask.GetMask("Ground")))
                    {
                        Debug.Log("selectionne");
                        TargetPoints.Add(hit.collider.GetComponent<WayPoint>());
                    }
                }
                //SpellPreviewPoints.Add(GraphMaker.Instance.PointDict[origin.SnapOnGrid()]);

                break;
            case SpellForm.Sphere:
                Debug.Log("Sphere");
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
                TargetPoints.Add(GraphMaker.Instance.PointDict[origin.SnapOnGrid()]);
                break;
        }

        return TargetPoints;
    }

    public void StartSelectionPreview()
    {
        foreach(WayPoint selectedPoint in ReadSelectionForm())
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
    }

    public void StopSpellPreview()
    {
        foreach(WayPoint targetPoint in _targetPoints)
        {
            targetPoint.ApplyDefaultVisual();
        }
        _targetPoints.Clear();
    }

    public virtual async void Execute(WayPoint target)
    {
        //await ShowVisuals(target);

       foreach(WayPoint targetPoint in _targetPoints)
       {
            //apply le sort
       }

    }

    protected virtual async Task ShowVisuals(WayPoint target)
    {
        //faire les visuals et les await bien comme un bon toutou de merde
    }
}
