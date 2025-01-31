using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(Move))]
public class Entity : MonoBehaviour
{
    public WayPoint CurrentPoint;

    [SerializeField] EntityData _data;

    public Health EntityHealth;

    Move _move;


    private void Awake()
    {
        if (_data != null)
        {
            EntityHealth.MaxHealth = _data.MaxHealth;
        }
        //update UI

        TryGetComponent(out EntityHealth);
        TryGetComponent(out _move);
    }

    private void Start()
    {
        Vector3Int roundedPos = transform.position.SnapOnGrid();
        transform.position = roundedPos;

        CurrentPoint = GraphMaker.Instance.PointDict[roundedPos];
        CurrentPoint.Content = gameObject;
    }

    public async Task MoveTo(WayPoint targetPoint)
    {
        Stack<WayPoint> path = FindBestPath(CurrentPoint, targetPoint);
        int pathlength = path.Count;
        for(int i = 0; i < pathlength; i++)
        {
            CurrentPoint.Content = null;

            WayPoint steppedOnPoint = path.Pop();

            await _move.StartMoving(steppedOnPoint.transform.position);

            CurrentPoint = steppedOnPoint;
            steppedOnPoint.Content = gameObject;

        }
    }

    public Stack<WayPoint> FindBestPath(WayPoint startPoint, WayPoint endPoint)
    {
        List<WayPoint> openWayPoints = new List<WayPoint>();
        List<WayPoint> closedWayPoints = new List<WayPoint>();

        Stack<WayPoint> shorterPath = new Stack<WayPoint>();

        startPoint.TravelThrough(ref openWayPoints, ref closedWayPoints, ref shorterPath, endPoint, startPoint);

        foreach (WayPoint point in openWayPoints) point.ResetState();
        foreach (WayPoint point in closedWayPoints) point.ResetState();
        return shorterPath;
    }
}
