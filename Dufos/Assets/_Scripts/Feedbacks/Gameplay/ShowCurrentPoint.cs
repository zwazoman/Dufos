using UnityEngine;

public class ShowCurrentPoint : MonoBehaviour
{
    [SerializeField]
    private SpellPlayerBehaviour _spellPlayer;
    private Entity _entity;
    private WayPoint _previousPoint;
    private int _previousWalkPoints;

    private void Start()
    {
        transform.parent.TryGetComponent(out _entity);
        _previousPoint = _entity.CurrentPoint;
    }

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

        if((_previousWalkPoints != _entity.MovePoints || _entity.MovePoints == _entity.Data.MaxMovePoints) && !_spellPlayer.IsSelecting)
        {
            _previousPoint.ApplyWalkableVisual();
            _previousWalkPoints = _entity.MovePoints;
        }
    }
}
