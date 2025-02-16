using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Gobelin : Entity
{
    GraphMaker _graphMaker;


    public override async void StartTurn()
    {
        base.StartTurn();

        _graphMaker = GraphMaker.Instance;

        Flood();

        WayPoint targetPlayerPoint;

        List<WayPoint> playerPoints = new List<WayPoint>();

        List<WayPoint> allTargetPoints = new List<WayPoint>();

        Dictionary<WayPoint, WayPoint> targetPointsDict = new Dictionary<WayPoint, WayPoint>();

        foreach (PlayerEntity player in CombatManager.Instance.PlayerEntities)
        {
            playerPoints.Add(player.CurrentPoint);
        }

        targetPlayerPoint = playerPoints.FindClosest(transform.position);

        Spell choosenSpell = Data.Spells.PickRandom();

        //target points en clef et selected points en value
        targetPointsDict = choosenSpell.ComputeTargetableWaypoints(targetPlayerPoint);

        foreach(WayPoint targetpoint in targetPointsDict.Keys)
        {
            allTargetPoints.Add(targetpoint);
        }

        WayPoint choosenTargetPoint = allTargetPoints.FindClosest(transform.position);
        print(choosenTargetPoint.transform.position);

        bool targetReached = await MoveToward(choosenTargetPoint); // le point le plus proche de lancé de sortt

        if (targetReached)
        {
            //trouver le Waypoint sur lequel tirer

            WayPoint selected = targetPointsDict[choosenTargetPoint];

            Vector3Int selfPointPos = _graphMaker.PointDict.GetKeyFromValue(CurrentPoint);
            Vector3Int targetPointPos = _graphMaker.PointDict.GetKeyFromValue(targetPlayerPoint);
            Vector3Int selectedpointPos = _graphMaker.PointDict.GetKeyFromValue(selected);

            WayPoint pointToSelect = _graphMaker.PointDict[selfPointPos + (targetPointPos - selectedpointPos)];


            choosenSpell.StartSpellPreview(pointToSelect , true);
            choosenSpell.Execute(pointToSelect);
        }
    }

   async Task<bool> MoveToward(WayPoint targetPoint)
    {
        print("move toward");

        if (Walkables.Contains(targetPoint))
        {
            print("target in range !");
            await TryMoveTo(targetPoint);
            return true;
        }
        print("target not in range yet ! getting closer...");
        await TryMoveTo(Walkables.FindClosest(targetPoint.transform.position));
        return false;
    }





    //idées en vrac :
    //class enemy entity ->
    //Bool MoveTowards(Waypoint targetpoint) -> bouge vers le point le plus proche du targetpoint renvoie true si le targetpoint est atteint
    //FindSpellPoints(Spell spell) ->  essaie de lancer le sort depuis la cible, stocke les points touchables, te déplace vers le point ( si le point est atteint lance le sort vers la case adéquate) HARDCORE
    //dans spell : List<Waypoint> ComputeTargetableWaypoints -> fait StartSelectionpreview puis StartargetPreview sur chaque selected -> renvoie la liste de targetPoints
    //rajouter un paramètre pour dire si on veut que les previews soient auto ou non
    //rajouter un paramètre Waypoint sur la startselection avec l'origin. pour potentiellement pouvoir lancer des spells depuis d'autres endroits que sois meme
}
