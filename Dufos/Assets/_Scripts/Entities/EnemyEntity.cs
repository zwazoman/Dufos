using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyEntity : Entity
{

    protected override void Start()
    {
        base.Start();
        CombatManager.Instance.EnemyEntities.Add(this);
    }

    public override async void EndTurn()
    {
        await Task.Delay(1000);
        base.EndTurn();
    }

    protected Spell ChooseSpell(int spellIndex)
    {
        return Data.Spells[spellIndex];
    }

    protected Spell ChooseRandomSpell()
    {
        return Data.Spells.PickRandom();
    }

    protected Spell ChooseSpellWithRange(WayPoint targetPoint)
    {
        return default;
    }

    protected WayPoint FindClosestPlayerPoint()
    {
        List<WayPoint> points = new List<WayPoint>();

        foreach (PlayerEntity player in CombatManager.Instance.PlayerEntities)
        {
            points.Add(player.CurrentPoint);
        }

        return points.FindClosest(transform.position);
    }

    /// <summary>
    /// preview le spell choosenSpell depuis le waypoint de la cible pour trouver le waypoint le plus proche depuis lequel tirer. Une fois cela fait : tire le sort depuis la cible
    /// </summary>
    /// <param name="choosenSpell"></param>
    /// <returns></returns>
    protected async Task<bool> TryUseSpell(Spell choosenSpell)
    {
        WayPoint targetPlayerPoint;
        targetPlayerPoint = FindClosestPlayerPoint();

        Dictionary<WayPoint, WayPoint> targetPointsDict = new Dictionary<WayPoint, WayPoint>();
        targetPointsDict = choosenSpell.ComputeTargetableWaypoints(targetPlayerPoint);

        List<WayPoint> allTargetPoints = new List<WayPoint>();
        foreach (WayPoint targetpoint in targetPointsDict.Keys)
        {
            allTargetPoints.Add(targetpoint);
        }

        WayPoint choosenTargetPoint = allTargetPoints.FindClosest(transform.position);
        print(choosenTargetPoint.transform.position);

        bool targetReached = await MoveToward(choosenTargetPoint); // le point le plus proche de lancé de sort

        await Task.Delay(1000);

        if (targetReached)
        {
            WayPoint selected = targetPointsDict[choosenTargetPoint];

            Vector3Int selfPointPos = graphMaker.PointDict.GetKeyFromValue(CurrentPoint);
            Vector3Int targetPointPos = graphMaker.PointDict.GetKeyFromValue(targetPlayerPoint);
            Vector3Int selectedpointPos = graphMaker.PointDict.GetKeyFromValue(selected);

            WayPoint pointToSelect = graphMaker.PointDict[selfPointPos + (targetPointPos - selectedpointPos)];

            choosenSpell.StartSpellPreview(pointToSelect);

            await Task.Delay(1000);

            await choosenSpell.Execute(pointToSelect);

            await Task.Delay(1000);

            choosenSpell.CancelSpellPreview();

            return true;
        }
        return false;
    }

    /// <summary>
    /// déplace l'entité vers la case la plus proche de la target
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <returns></returns>
    async Task<bool> MoveToward(WayPoint targetPoint)
    {
        if (Walkables.Contains(targetPoint))
        {
            await TryMoveTo(targetPoint);
            return true;
        }
        await TryMoveTo(Walkables.FindClosest(targetPoint.transform.position));
        return false;
    }

}
