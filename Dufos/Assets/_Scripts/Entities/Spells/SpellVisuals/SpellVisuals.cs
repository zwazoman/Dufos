using System.Threading.Tasks;
using UnityEngine;

public class SpellVisuals
{
    public virtual async Task ShowVisuals(WayPoint target)
    {

    }

    public async Task RockProjectile(WayPoint target)
    {
        await Task.Yield();
        Debug.Log("<color=red>test visuel 1</color>" + target.name);
        SpellVfxManager.Instance.PlayVfx("RockProjectile", target.transform);
    }

    public async Task CatchingFire(WayPoint target)
    {
        await Task.Yield();
        //Debug.Log("<color=red>test visuel 1</color>" + targets.name);
        SpellVfxManager.Instance.PlayVfx("CatchingFire", target.transform);
    }
}
