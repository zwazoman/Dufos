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
