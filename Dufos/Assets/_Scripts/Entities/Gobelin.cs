using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Gobelin : Entity
{
    public override async void StartTurn()
    {
        base.StartTurn();

        Flood();

        PlayerEntity closestPlayer = CombatManager.Instance.PlayerEntities.FindClosest(transform.position);

        //un peu cheap mais fonctionne pour la mêlée
        foreach(WayPoint point in closestPlayer.CurrentPoint.Neighbours)
        {
            if (Walkables.Contains(point))
            {
                await TryMoveTo(point);
                foreach(WayPoint neighbour in point.Neighbours)
                {
                    if(neighbour.Content != null && neighbour.Content.layer == 7)
                    {
                        //UseSpell(0);
                    }
                }
            }
        }

        WayPoint closestPointToClosestPlayer = Walkables.FindClosest(closestPlayer.transform.position);

        await TryMoveTo(closestPointToClosestPlayer);

        // vérifier dans les cases atteignables par les sorts si il y a un joueur -> si oui taper la case en question
    }

    //idées en vrac :
    //class enemy entity ->
    //MoveTowards(Waypoint point) -> bouge vers le point le plus proche du point
    //FindSpellPoints(Spell spell) ->  essaie de lancer le sort depuis la cible, stocke les points touchables, te déplace vers le point ( si le point est atteint lance le sort vers la case adéquate) HARDCORE
}
