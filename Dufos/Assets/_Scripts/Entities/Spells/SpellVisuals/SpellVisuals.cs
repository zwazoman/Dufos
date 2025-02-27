using System.Threading.Tasks;

public class SpellVisuals
{

    public async Task RockProjectile(WayPoint target)
    {
        AudioManager.Instance.PlaySFXClip(Sounds.Rocks,0.7f);
        await SpellVfxManager.Instance.PlayParticles("RockProjectile", target.transform);
    }

    public async Task CatchingFire(WayPoint target)
    {
        AudioManager.Instance.PlaySFXClip(Sounds.Fire, 0.7f);
        await SpellVfxManager.Instance.PlayParticles("CatchingFire", target.transform);
    }

    public async Task ThunderStrike(WayPoint target)
    {
        AudioManager.Instance.PlaySFXClip(Sounds.Zap, 0.7f);
        await SpellVfxManager.Instance.PlayParticles("ThunderStrike", target.transform);
    }

    public async Task BloodMalediction(WayPoint target)
    {
        AudioManager.Instance.PlaySFXClip(Sounds.Slice, 0.7f);
        await SpellVfxManager.Instance.PlayParticles("BloodMalediction", target.transform);
    }

    public async Task SpikeLines(WayPoint target)
    {
        AudioManager.Instance.PlaySFXClip(Sounds.Slice, 0.7f);
        await SpellVfxManager.Instance.PlayParticles("SpikeLines", target.transform);
    }

    public async Task IceFall(WayPoint target)
    {
        AudioManager.Instance.PlaySFXClip(Sounds.Ice, 0.7f);
        await SpellVfxManager.Instance.PlayParticles("IceFall", target.transform);
    }
}
