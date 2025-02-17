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

        EndTurn();
    }
}
