using System;
using System.Diagnostics;
using System.Threading.Tasks;

[Serializable]
public class VisualTest1 : SpellVisuals
{
    private bool _endSpell;

    public override async Task ShowVisuals(WayPoint target)
    {
        while (!_endSpell)
        {
            _endSpell = true;
            await Task.Yield();
        }
    }
}
