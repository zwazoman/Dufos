using UnityEngine;

public class Gobelin : EnemyEntity
{
    [SerializeField]
    private EntityOrderDisplay _order;
    public override async void StartTurn()
    {
        base.StartTurn();

        Flood(CurrentPoint,MovePoints);

        Spell choosenSpell = ChooseRandomSpell();

        await TryUseSpell(choosenSpell);

        _order.UpdateOrder();

        EndTurn();
    }
}
