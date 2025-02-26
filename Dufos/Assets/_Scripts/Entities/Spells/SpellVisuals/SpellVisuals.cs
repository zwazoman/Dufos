using System.Threading.Tasks;

public class SpellVisuals
{

    public async Task RockProjectile(WayPoint target)
    {
        await SpellVfxManager.Instance.PlayParticles("RockProjectile", target.transform);
    }

    public async Task CatchingFire(WayPoint target)
    {
        await SpellVfxManager.Instance.PlayParticles("CatchingFire", target.transform);
    }

    public async Task ThunderStrike(WayPoint target)
    {
        await SpellVfxManager.Instance.PlayParticles("ThunderStrike", target.transform);
    }

    public async Task BloodMalediction(WayPoint target)
    {
        await SpellVfxManager.Instance.PlayParticles("BloodMalediction", target.transform);
    }

    public async Task SpikeLines(WayPoint target)
    {
        await SpellVfxManager.Instance.PlayParticles("SpikeLines", target.transform);
    }

    public async Task IceFall(WayPoint target)
    {
        await SpellVfxManager.Instance.PlayParticles("IceFall", target.transform);
    }
}
