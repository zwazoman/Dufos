using UnityEngine;

public class Gobelin : EnemyEntity
{
    [SerializeField]
    private EntityOrderDisplay _order;
    public override async void StartTurn()
    {
        base.StartTurn();

        Flood();

        //Spell choosenSpell = ChooseRandomSpell();

        WayPoint targetPlayerPoint = FindClosestPlayerPoint();

        Spell choosenSpell = ChooseSpellWithRange(this, targetPlayerPoint);

        await TryUseSpell(choosenSpell,targetPlayerPoint);

        _order.UpdateOrder();

        EndTurn();
    }
}
