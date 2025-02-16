using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public class Spell
{
    public event Action OnPreviewStarted;
    public event Action OnPreviewCanceled;

    [SerializeField] public SpellData Data;

    [HideInInspector] public Entity Caster;
    [HideInInspector] public bool IsPreviewing;

    SpellVisuals spellVisuals = new SpellVisuals();

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
                List<WayPoint> targetpoints = new List<WayPoint>();
                foreach(Entity entity in CombatManager.Instance.Entities)
                {
                    targetpoints.Add(entity.CurrentPoint);
                }
                foreach(WayPoint point in targetpoints)
                {
                    SelectionPoints.Add(point);
                }
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
        OnPreviewStarted?.Invoke();
        IsPreviewing = true;
        List<WayPoint> selectetPoints = ReadSelectionForm();
        foreach (WayPoint selectedPoint in selectetPoints)
        {
            if (!Data.ThrowableOnWalls && selectedPoint.Content.layer == 6)
                continue;

            GraphMaker.Instance.SelectedPoints.Add(selectedPoint);

            selectedPoint.OnHovered += StartSpellPreview;
            selectedPoint.OnNotHovered += CancelSpellPreview;
            selectedPoint.OnClicked += Execute;

            selectedPoint.ApplySelectVisual();
        }
    }

    public void StartSpellPreview(WayPoint selectedPoint)
    {
        foreach(WayPoint targetPoint in ReadTargetForm(selectedPoint))
        {
            GraphMaker.Instance.TargetPoints.Add(targetPoint);
            
            targetPoint.ApplyTargetVisual();
        }
    }

    public void CancelSelectionPreview()
    {
        foreach(WayPoint selectedPoint in GraphMaker.Instance.SelectedPoints)
        {
            Debug.Log(selectedPoint);
            selectedPoint.OnHovered -= StartSpellPreview;
            selectedPoint.OnNotHovered -= CancelSpellPreview;
            selectedPoint.OnClicked -= Execute;

            selectedPoint.ApplyDefaultVisual();
        }
        GraphMaker.Instance.SelectedPoints.Clear();

        CancelSpellPreview();
        IsPreviewing = false;
    }

    public void CancelSpellPreview()
    {
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

    public async void Execute(WayPoint origin)
    {
        WayPoint[] targets = GraphMaker.Instance.TargetPoints.ToArray();

        CancelSelectionPreview();

        switch (Data.Visual)
        {
            //case Visuals.FireBall:
            //    await spellVisuals.ShowFireball();
            //    break;

        }

        foreach (WayPoint target in targets)
        {
            target.TryApplyDamage(Data.Damage);
        }

        OnPreviewCanceled?.Invoke();
    }
}
