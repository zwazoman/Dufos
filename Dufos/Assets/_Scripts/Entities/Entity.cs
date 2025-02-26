using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(Move))]
public class Entity : MonoBehaviour
{
    [HideInInspector] public WayPoint CurrentPoint;

    [SerializeField] public EntityData Data;

    [HideInInspector] public Health EntityHealth;
    [HideInInspector] public int MovePoints;

    public List<WayPoint> Walkables = new List<WayPoint>();
    protected GraphMaker graphMaker;

    Move _move;

    protected virtual void Awake()
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

    protected virtual void Start()
    {
        graphMaker = GraphMaker.Instance;

        CombatManager.Instance.Entities.Add(this);

        Vector3Int roundedPos = transform.position.SnapOnGrid();
        transform.position = roundedPos;
        transform.position += Vector3.up * 1.3f;

        CurrentPoint = GraphMaker.Instance.PointDict[roundedPos];
        CurrentPoint.StepOn(gameObject);
    }

    public virtual void StartTurn() //transférer (passer en virtual)
    {
        print("start turn : " + gameObject.name);
        //montrer les cases accessibles par le click / pour les déplacements
    }

    public virtual void EndTurn() //transférer (passer en virtual)
    {
        MovePoints = Data.MaxMovePoints;
        CombatManager.Instance.NextTurn();
    }

    public virtual async Task TryMoveTo(WayPoint targetPoint)
    {
        Stack<WayPoint> path = Tools.FindBestPath(CurrentPoint, targetPoint);
        int pathlength = path.Count;

        if(pathlength > MovePoints)
        {
            print("plus de pm !");
            return;
        }

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
        // remettre les controles
    }
    protected void Flood()
    {
        //reset les anciens walkkables
        Walkables.Clear();

        Walkables.Add(CurrentPoint);

        for (int i = 0; i < MovePoints; i++)
        {
            List<WayPoint> tmp = new List<WayPoint>();
            foreach (WayPoint walkable in Walkables)
            {
                foreach (WayPoint neighbour in walkable.Neighbours)
                {
                    if (Walkables.Contains(neighbour) || !neighbour.IsActive) continue;
                    tmp.Add(neighbour);
                }
            }
            Walkables.AddRange(tmp);
        }
    }
}
