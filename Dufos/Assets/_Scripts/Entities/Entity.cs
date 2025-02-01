using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(Move))]
public class Entity : MonoBehaviour
{
    public event Action OnSelected;

    public WayPoint CurrentPoint;

    [SerializeField] EntityData _data;

    public Health EntityHealth;
    public int MovePoints;

    Move _move;




    private void Awake()
    {
        TryGetComponent(out EntityHealth);
        TryGetComponent(out _move);

        if (_data != null)
        {
            EntityHealth.MaxHealth = _data.MaxHealth;
            MovePoints = _data.MaxMovePoints;
        }
        //update UI
    }

    private void Start()
    {
        Vector3Int roundedPos = transform.position.SnapOnGrid();
        transform.position = roundedPos;

        CurrentPoint = GraphMaker.Instance.PointDict[roundedPos];
        CurrentPoint.Content = gameObject;
    }

    public void StartTurn()
    {
        //montrer les cases accessibles par le click / pour les déplacements
        //leur ajouter un component "clickable" ? qu'on met que sur les cases et qui détecte le click dessus ?
        //ou les faire passer dans un état de détection particulier <-
    }

    public void EndTurn()
    {
        //reset tout
        MovePoints = _data.MaxMovePoints;
    }

    public async Task MoveTo(WayPoint targetPoint)
    {
        Stack<WayPoint> path = FindBestPath(CurrentPoint, targetPoint);
        int pathlength = path.Count;

        if(pathlength > MovePoints)
        {
            print("plus de pm !");
            return;
        }

        for(int i = 0; i < pathlength; i++)
        {
            CurrentPoint.Content = null;

            WayPoint steppedOnPoint = path.Pop();

            await _move.StartMoving(steppedOnPoint.transform.position);

            CurrentPoint = steppedOnPoint;
            steppedOnPoint.Content = gameObject;

            MovePoints--;

        }
    }

    Stack<WayPoint> FindBestPath(WayPoint startPoint, WayPoint endPoint)
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
