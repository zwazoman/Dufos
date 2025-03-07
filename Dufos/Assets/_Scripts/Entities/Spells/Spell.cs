using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Spell
{
    public event Action OnPreviewStarted;
    public event Action OnPreviewCanceled;

    [SerializeField] public SpellData Data;

    [HideInInspector] public Entity Caster;
    [HideInInspector] public bool IsPreviewing;

    SpellVisuals spellVisuals = new SpellVisuals();

    protected List<WayPoint> ReadSelectionForm(WayPoint origin)
    {
        Vector3 originPoint = origin.transform.position;

        List<WayPoint> selectionPoints = new List<WayPoint>();

        switch (Data.LaunchForm)
        {
            case SelectionForm.Ray:
                foreach(Vector3 direction in Tools.AllFlatDirections)
                {
                    foreach(RaycastHit hit in Physics.RaycastAll(originPoint + direction * Data.SelectionBypassSize ,direction,Data.SelectionMaxRange - Data.SelectionBypassSize, LayerMask.GetMask("Ground")))
                    {
                        selectionPoints.Add(hit.collider.GetComponent<WayPoint>());
                    }
                }

                break;
            case SelectionForm.Sphere:
                foreach (Collider hit in Physics.OverlapSphere(originPoint, Data.SelectionMaxRange, LayerMask.GetMask("Ground")))
                {
                    selectionPoints.Add(hit.GetComponent<WayPoint>());
                    foreach (Collider removeHit in Physics.OverlapSphere(originPoint, Data.SelectionBypassSize, LayerMask.GetMask("Ground")))
                    {
                        selectionPoints.Remove(removeHit.GetComponent<WayPoint>());
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
                    selectionPoints.Add(point);
                }
                break;
        }

        return selectionPoints;
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

    public Dictionary<WayPoint,WayPoint> ComputeTargetableWaypoints(WayPoint origin)
    {
        Dictionary<WayPoint,WayPoint> targetPointsDict = new Dictionary<WayPoint,WayPoint>();

        StartSelectionPreview(origin,true);

        foreach(WayPoint selectedPoint in GraphMaker.Instance.SelectedPoints)
        {
            StartSpellPreview(selectedPoint,true);
            foreach(WayPoint targetpoint  in GraphMaker.Instance.TargetPoints)
            {
                if (targetPointsDict.ContainsKey(targetpoint))
                    continue;
                targetPointsDict.Add(targetpoint,selectedPoint);
            }
            GraphMaker.Instance.TargetPoints.Clear();
        }
        GraphMaker.Instance.SelectedPoints.Clear();

        return targetPointsDict;
    }


    public void StartSelectionPreview(WayPoint origin = null, bool isAuto = false)
    {
        OnPreviewStarted?.Invoke();
        IsPreviewing = true;

        if (origin == null)
            origin = Caster.CurrentPoint;

        List<WayPoint> selectetPoints = ReadSelectionForm(origin);

        foreach (WayPoint selectedPoint in selectetPoints)
        {
            if (!Data.ThrowableOnWalls && selectedPoint.Content != null && selectedPoint.Content.layer == 6)
                continue;

            GraphMaker.Instance.SelectedPoints.Add(selectedPoint);

            if (!isAuto)
            {
                selectedPoint.OnHovered += StartSpellPreview;
                selectedPoint.OnNotHovered += CancelSpellPreview;
                selectedPoint.OnClicked += StartExecute;

                selectedPoint.ApplySelectVisual();
            }
        }
    }

    public void StartSpellPreview(WayPoint selectedPoint, bool isAuto = false)
    {
        foreach(WayPoint targetPoint in ReadTargetForm(selectedPoint))
        {
            if (targetPoint.Content != null && targetPoint.Content.layer == 6)
                continue;

            GraphMaker.Instance.TargetPoints.Add(targetPoint);

            if (!isAuto)
            {
                targetPoint.ApplyTargetVisual();
            }
        }
    }

    public void CancelSelectionPreview(bool cancelSpellPreview = true)
    {
        foreach(WayPoint selectedPoint in GraphMaker.Instance.SelectedPoints)
        {
            Debug.Log(selectedPoint);
            selectedPoint.OnHovered -= StartSpellPreview;
            selectedPoint.OnNotHovered -= CancelSpellPreview;
            selectedPoint.OnClicked -= StartExecute;

            if (GraphMaker.Instance.TargetPoints.Contains(selectedPoint))
                selectedPoint.ApplyTargetVisual();
            else
                selectedPoint.ApplyDefaultVisual();
        }
        GraphMaker.Instance.SelectedPoints.Clear();

        if (cancelSpellPreview)
        {
            CancelSpellPreview();
            OnPreviewCanceled?.Invoke();
        }

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

    public async void StartExecute(WayPoint origin)
    {

        origin.OnNotHovered -= CancelSpellPreview;

        await Execute(origin);
        CancelSpellPreview();
        OnPreviewCanceled?.Invoke();
    }

    public async Task Execute(WayPoint origin)
    {
        WayPoint[] targets = GraphMaker.Instance.TargetPoints.ToArray();
        CancelSelectionPreview(false);

        switch (Data.Visuals)
        {
            case Visuals.RockProjectile:
                await spellVisuals.RockProjectile(origin);
                break;
            case Visuals.CatchingFire:
                await spellVisuals.CatchingFire(origin);
                break;
            case Visuals.ThunderStrike:
                await spellVisuals.ThunderStrike(origin);
                break;
            case Visuals.BloodMalediction:
                await spellVisuals.BloodMalediction(origin);
                break;
            case Visuals.SpikeLines:
                await spellVisuals.SpikeLines(origin);
                break;
            case Visuals.IceFall:
                await spellVisuals.IceFall(origin);
                break;
        }

        foreach (WayPoint target in targets)
        {
            if(target.Content != Caster.CurrentPoint.Content)
            {
                target.TryApplyDamage(Data.Damage);
            }
        }
    }
}
