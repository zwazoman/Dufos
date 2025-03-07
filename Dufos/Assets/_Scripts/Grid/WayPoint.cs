using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public bool IsHovered { get; private set; }

    public event Action OnSteppedOn;
    public event Action OnSteppedOff;

    public event Action<WayPoint, bool> OnHovered;
    public event Action OnNotHovered;

    public event Action<WayPoint> OnClicked;

    public List<WayPoint> Neighbours = new List<WayPoint>();

    public GameObject Content;

    public bool IsActive;

    [SerializeField] LayerMask _mask;

    [SerializeField] Material _walkableMat;

    [SerializeField] Material _selectionMat;
    [SerializeField] Material _targettingMat;
    [SerializeField] Material _defaultMat;

    [Header("Juice")]
    [SerializeField] GameObject _visualsObject;
    [SerializeField] float floatTime = .3f;
    [SerializeField] float floatHeight = 1f;

    MeshRenderer _mR;

    #region Astar Fields
    [HideInInspector] public WayPoint FormerPoint;

    [HideInInspector] public bool IsOpen = false;
    [HideInInspector] public bool IsClosed = false;

    [HideInInspector] public float H;
    [HideInInspector] public float G;
    [HideInInspector] public float F => G + H ;
    #endregion

    private void Awake()
    {
        IsActive = true;
        _mR = GetComponentInChildren<MeshRenderer>();
        _mR.material = _defaultMat;
    }

    private void Start()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.up, out hit, 1, _mask))
        {
            StepOn(hit.collider.gameObject);
        }
    }

    public void StepOn(GameObject entity)
    {
        Deactivate();
        Content = entity;
        OnSteppedOn?.Invoke();
    }

    public void StepOff()
    {
        Content = null;
        ApplyDefaultVisual();
        OnSteppedOff?.Invoke();
        Activate();
    }

    public void TryApplyDamage(int damages)
    {
        if (Content == null)
            return;

        if(Content.TryGetComponent(out Health health))
        {
            health.ApplyDamage(damages);
        }
    }

    public void ApplySelectVisual()
    {
        try
        {
            IsHovered = true;
            _mR.material = _selectionMat;
        }

        catch (Exception)
        {
        }

    }

    public void ApplyTargetVisual()
    {
        try
        {
            IsHovered = true;
            _mR.material = _targettingMat;
        }
        catch (Exception)
        {
        }

    }

    public void ApplyDefaultVisual()
    {
        try
        {
            IsHovered = false;
            _mR.material = _defaultMat;
        }

        catch (Exception)
        {
        }
    }

    public void ApplyWalkableVisual()
    {
        try
        {
            _mR.material = _walkableMat;
        }

        catch(Exception)
        {
        }

    }

    public void Clicked() 
    {
        CombatManager combatManager = CombatManager.Instance;

        if(!combatManager.EnemyEntities.Contains(combatManager.CurrentEntity))  combatManager.CurrentEntity.TryMoveTo(this);
        OnClicked?.Invoke(this);
    }

    public void Hovered()
    {
        OnHovered?.Invoke(this,false);
        _visualsObject.transform.DOMoveY(floatHeight, floatTime).SetEase(Ease.OutBack);
    }

    public void NotHovered()
    {
        OnNotHovered?.Invoke();
        _visualsObject.transform.DOMoveY(0, floatTime).SetEase(Ease.InBack);
    }

    #region Astar
    public void TravelThrough(ref List<WayPoint> openPoints,ref List<WayPoint> closedPoints, ref Stack<WayPoint> shorterPath, WayPoint endPoint, WayPoint startPoint)
    {
        if(this == endPoint)
        {
            Close(ref openPoints, ref closedPoints);
            WayPoint currentPoint = endPoint;
            while(currentPoint != startPoint)
            {
                shorterPath.Push(currentPoint);
                currentPoint = currentPoint.FormerPoint;
            }
            return;
        }

        Close(ref openPoints, ref closedPoints);

        foreach(WayPoint point in Neighbours)
        {
            if (point.IsClosed || point.IsOpen || !point.IsActive) continue;

            point.Open(this, endPoint,  ref openPoints);
        }

        if(openPoints.Count == 0)
        {
            return;
        }

        WayPoint bestPoint = null;
        foreach (WayPoint point in openPoints)
        {
            if (bestPoint == null) bestPoint = point;
            else if (point.F < bestPoint.F) bestPoint = point;
        }

        bestPoint.TravelThrough(ref openPoints,ref closedPoints, ref shorterPath, endPoint, startPoint);
    }

    void Open(WayPoint formerPoint, WayPoint endPoint, ref List<WayPoint> openPoints)
    {
        IsOpen = true;

        openPoints.Add(this);

        FormerPoint = formerPoint;

        H = Vector3.Distance(transform.position, endPoint.transform.position);
        G ++;

    }

    void Close(ref List<WayPoint> openPoints, ref List<WayPoint> closedPoints)
    {
        IsClosed = true;
        closedPoints.Add(this);
        if(openPoints.Contains(this)) openPoints.Remove(this);
    }

    public void ResetState()
    {
        FormerPoint = null;
        G = 0;
        H = 0;
        IsClosed = false;
        IsOpen = false;
    }

    public void Activate()
    {
        IsActive = true;
        //gameObject.SetActive(true); // visuels pour l'instant
    }

    public void Deactivate()
    {
        IsActive = false;
        //gameObject.SetActive(false); // visuels pour l'instant
    }
    #endregion Astar

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (WayPoint point in Neighbours) //il dessine 2 fois mais t'inquietes
        {
            Gizmos.DrawLine(transform.position, point.transform.position);
        }
    }
}