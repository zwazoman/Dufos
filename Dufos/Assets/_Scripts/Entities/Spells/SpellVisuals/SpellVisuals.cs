using System.Threading.Tasks;

public class SpellVisuals
{
    public virtual async Task ShowVisuals(WayPoint target)
    {

    }

    public async Task RockProjectile(WayPoint target)
    {
        SpellVfxManager.Instance.PlayParticles("RockProjectile", target.transform);
    }

    public async Task CatchingFire(WayPoint target)
    {
        SpellVfxManager.Instance.PlayParticles("CatchingFire", target.transform);
    }

    public async Task ThunderStrike(WayPoint target)
    {
        SpellVfxManager.Instance.PlayParticles("ThunderStrike", target.transform);
    }

    public async Task BloodMalediction(WayPoint target)
    {
        SpellVfxManager.Instance.PlayParticles("BloodMalediction", target.transform);
    }

    public async Task SpikeLines(WayPoint target)
    {
        SpellVfxManager.Instance.PlayParticles("SpikeLines", target.transform);
    }

    public async Task IceFall(WayPoint target)
    {
        SpellVfxManager.Instance.PlayParticles("IceFall", target.transform);
    }
}
