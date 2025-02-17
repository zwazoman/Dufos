using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Gobelin : EnemyEntity
{

    public override async void StartTurn()
    {
        base.StartTurn();

        Flood();

        Spell choosenSpell = ChooseRandomSpell();

        await TryUseSpell(choosenSpell);

        await Task.Delay(1000);

        EndTurn();
    }


    //idées en vrac :
    //class enemy entity ->
    //Bool MoveTowards(Waypoint targetpoint) -> bouge vers le point le plus proche du targetpoint renvoie true si le targetpoint est atteint
    //FindSpellPoints(Spell spell) ->  essaie de lancer le sort depuis la cible, stocke les points touchables, te déplace vers le point ( si le point est atteint lance le sort vers la case adéquate) HARDCORE
    //dans spell : List<Waypoint> ComputeTargetableWaypoints -> fait StartSelectionpreview puis StartargetPreview sur chaque selected -> renvoie la liste de targetPoints
    //rajouter un paramètre pour dire si on veut que les previews soient auto ou non
    //rajouter un paramètre Waypoint sur la startselection avec l'origin. pour potentiellement pouvoir lancer des spells depuis d'autres endroits que sois meme
}
