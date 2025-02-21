using UnityEngine;

public class Gobelin : EnemyEntity
{
    [SerializeField]
    private EntityOrderDisplay _order;
    public override async void StartTurn()
    {
        base.StartTurn();

        Flood();

        Spell choosenSpell = ChooseRandomSpell();

        await TryUseSpell(choosenSpell);

        _order.UpdateOrder();

        EndTurn();
    }
}
