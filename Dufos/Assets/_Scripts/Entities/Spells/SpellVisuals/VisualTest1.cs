using System;
using System.Threading.Tasks;

[Serializable]
public class VisualTest1 : SpellVisual
{
    private bool _endSpell;

    public override async Task ShowVisuals(WayPoint target)
    {
        while (!_endSpell)
        {
            _endSpell = true;
            await Task.Yield();
        }

        print("spell used");
    }
}
