using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(Move))]
public class Entity : MonoBehaviour
{
    [HideInInspector] public bool CanInteract = false; //transférer

    [HideInInspector] public WayPoint CurrentPoint;

    [SerializeField] public EntityData Data;

    [HideInInspector] public Health EntityHealth;
    [HideInInspector] public int MovePoints;

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
        CombatManager.Instance.entities.Add(this);

        Vector3Int roundedPos = transform.position.SnapOnGrid();
        transform.position = roundedPos;

        CurrentPoint = GraphMaker.Instance.PointDict[roundedPos];
        CurrentPoint.StepOn(gameObject);
    }

    private void Update()
    {
        // remplacer par l'ui

        if (!CanInteract) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("ESPACE");
            Data.Spells[0].StartSelectionPreview();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Data.Spells[0].StopSelectionPreview();
        }

    }

    public void StartTurn() //transférer (passer en virtual)
    {
        print("start turn : " + gameObject.name);
        CanInteract = true;
        //montrer les cases accessibles par le click / pour les déplacements
    }

    public void EndTurn() //transférer (passer en virtual)
    {
        CanInteract = false;

        MovePoints = Data.MaxMovePoints;
    }

    public async Task TryMoveTo(WayPoint targetPoint)
    {
        if (!CanInteract) return;

        Stack<WayPoint> path = FindBestPath(CurrentPoint, targetPoint);
        int pathlength = path.Count;

        if(pathlength > MovePoints)
        {
            print("plus de pm !");
            return;
        }

        CanInteract = false;

        foreach(WayPoint tile in path)
        {
            tile.ApplyWalkableVisual();
        }

        for (int i = 0; i < pathlength; i++)
        {
            CurrentPoint.StepOff();

            WayPoint steppedOnPoint = path.Pop();

            await _move.StartMoving(steppedOnPoint.transform.position);

            CurrentPoint = steppedOnPoint;
            steppedOnPoint.StepOn(gameObject);

            steppedOnPoint.ApplyDefaultVisual();

            MovePoints--;
        }
        CanInteract = true;
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
