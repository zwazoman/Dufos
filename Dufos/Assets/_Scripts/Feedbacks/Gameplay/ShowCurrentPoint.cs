using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCurrentPoint : MonoBehaviour
{
    private void Start()
    {
        transform.parent.TryGetComponent(out _entity);
        _previousPoint = _entity.CurrentPoint;
    }

    private Entity _entity;
    private WayPoint _previousPoint;

    private void Update()
    {
        if (_previousPoint == null)
        {
            _previousPoint = _entity.CurrentPoint;
        }

        if (_previousPoint != null && _previousPoint != _entity.CurrentPoint)
        {
            _previousPoint.ApplyDefaultVisual();
            _previousPoint = _entity.CurrentPoint;
            _previousPoint.ApplyWalkableVisual();
        }

        _previousPoint.ApplyWalkableVisual();
    }
}
