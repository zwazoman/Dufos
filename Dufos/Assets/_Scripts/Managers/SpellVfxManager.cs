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

                foreach (var button in _deselectionButtons)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }

        await Task.Delay(Mathf.RoundToInt(_parts[Vfxs.IndexOf(current)].duration * 1000));
    }
}