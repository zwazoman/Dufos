using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenShakeBehaviour : MonoBehaviour
{
    private CinemachineImpulseSource _source;
    [SerializeField]
    private Volume _volume;
    private VolumeProfile _profile;
    private ChromaticAberration _chromaticAberration;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
        _profile = _volume.GetComponent<Volume>().profile;

        _profile.TryGet(out _chromaticAberration);
    }

    public void Shake()
    {
        _source.GenerateImpulse();

        Sequence chromaticBounce = DOTween.Sequence();
        chromaticBounce
            .Append(DOTween.To(() => _chromaticAberration.intensity.value, x => _chromaticAberration.intensity.value = x, 0.8f, 0.25f))
            .Append(DOTween.To(() => _chromaticAberration.intensity.value, x => _chromaticAberration.intensity.value = x, 0.2f, 0.25f));
    }
}
