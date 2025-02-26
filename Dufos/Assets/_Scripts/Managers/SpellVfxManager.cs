using Cinemachine;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpellVfxManager : MonoBehaviour
{
    public static SpellVfxManager Instance;

    [field : SerializeField]
    public List<GameObject> Vfxs = new();

    [SerializeField]
    private List<GameObject> _deselectionButtons = new();

    private List<ParticleSystem.MainModule> _parts = new();

    private ParticleSystem _part;
    private CinemachineVirtualCamera _cam;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            Instance = this;
        }

        foreach (var go in Vfxs)
        {
            if(go.TryGetComponent<ParticleSystem>(out _part))
            {
                _parts.Add(_part.main);
            }
        }

        _cam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public async Task PlayParticles(string vfxName, Transform transform)
    {
        GameObject current = null;

        foreach (var vfx in Vfxs)
        {
            if (vfx.name == vfxName)
            {
                current = vfx;
                vfx.transform.SetParent(transform, false);
                vfx.transform.localPosition = Vector3.zero;
                vfx.gameObject.SetActive(true);
                _cam.LookAt = current.transform;

                DOTween.To(() => _cam.m_Lens.FieldOfView, x => _cam.m_Lens.FieldOfView = x, 50f, 1f).SetEase(Ease.OutQuad) ;

                foreach (var button in _deselectionButtons)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }

        if (current != null && Vfxs.IndexOf(current) < _parts.Count)
        {
            await Task.Delay(Mathf.RoundToInt(_parts[Vfxs.IndexOf(current)].duration * 1000));
        }

    }
}