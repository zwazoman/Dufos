using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(Move))]
public class Entity : MonoBehaviour
{
    public WayPoint CurrentPoint;

    [SerializeField] public EntityData Data;

    public Health EntityHealth;
    public int MovePoints;

    Move _move;

    private void Awake()
    {
        TryGetComponent(out EntityHealth);
        TryGetComponent(out _move);

        if (Data != null)
        {
            EntityHealth.MaxHealth = Data.MaxHealth;

            MovePoints = Data.MaxMovePoints;

            foreach(Spell spell in Data.Spells)
            {
                spell.Caster = this;
            }
        }
        //update UI
    }

    private void Start()
    {
        Vector3Int roundedPos = transform.position.SnapOnGrid();
        transform.position = roundedPos;

        CurrentPoint = GraphMaker.Instance.PointDict[roundedPos];
        CurrentPoint.StepOn(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("ESPACE");
            Data.Spells[0].StartSelectionPreview();
        }
    }

    public void StartTurn()
    {
        //montrer les cases accessibles par le click / pour les déplacements
        //leur ajouter un component "clickable" ? qu'on met que sur les cases et qui détecte le click dessus ?
        //ou les faire passer dans un état de détection particulier <-
    }

    public void EndTurn()
    {
        //reset tout et unlick event onclick
        MovePoints = Data.MaxMovePoints;
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

        //retirer les controles

        for (int i = 0; i < pathlength; i++)
        {
            CurrentPoint.StepOff();

            WayPoint steppedOnPoint = path.Pop();

            await _move.StartMoving(steppedOnPoint.transform.position);

            CurrentPoint = steppedOnPoint;
            steppedOnPoint.StepOn(gameObject);

            MovePoints--;
        }
        // remettre les controles
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
