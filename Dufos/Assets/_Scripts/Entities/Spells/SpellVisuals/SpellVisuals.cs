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
        SpellVfxManager.Instance.PlayParticles("RockProjectile", target.transform);
    }

    public async Task CatchingFire(WayPoint target)
    {
        await Task.Yield();
        SpellVfxManager.Instance.PlayParticles("CatchingFire", target.transform);
    }

    public async Task MeteorProjectile(WayPoint target)
    {
        await Task.Yield();
        SpellVfxManager.Instance.PlayParticles("MeteorProjectile", target.transform);
    }

    public async Task ThunderStrike(WayPoint target)
    {
        await Task.Yield();
        SpellVfxManager.Instance.PlayParticles("ThunderStrike", target.transform);
    }

    public async Task BloodMalediction(WayPoint target)
    {
        await Task.Yield();
        SpellVfxManager.Instance.PlayParticles("BloodMalediction", target.transform);
    }

    public async Task SpikeLines(WayPoint target)
    {
        await Task.Yield();
        SpellVfxManager.Instance.PlayParticles("SpikeLines", target.transform);
    }
}
