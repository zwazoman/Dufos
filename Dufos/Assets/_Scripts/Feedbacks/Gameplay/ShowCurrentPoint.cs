using UnityEngine;

public class ShowCurrentPoint : MonoBehaviour
{
    [SerializeField]
    private Material _changingMat;
    private WayPoint _waypoint;
    private MeshRenderer _mesh;

    private void Start()
    {
        TryGetComponent(out _waypoint);
        transform.GetChild(0).TryGetComponent(out _mesh);
    }

    private void Update()
    {
        if (_mesh.material != _changingMat && !_waypoint.IsHovered && _waypoint.Content != null && (_waypoint.Content.name.Contains("Player") || _waypoint.Content.name.Contains("Enemy")))
        {
            _mesh.material = _changingMat;
        }

        if (_waypoint.Content != null && !_waypoint.Content.activeInHierarchy && !_waypoint.IsHovered)
        {
            _waypoint.ApplyDefaultVisual();
        }
    }
}
