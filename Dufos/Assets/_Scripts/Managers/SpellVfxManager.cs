using DG.Tweening;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SpellVfxManager : MonoBehaviour
{
    public static SpellVfxManager Instance;

    [field : SerializeField]
    public List<GameObject> Vfxs = new();

    [SerializeField]
    private List<GameObject> _deselectionButtons = new();

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
    }

    public void PlayParticles(string vfxName, Transform transform)
    {
        foreach (var vfx in Vfxs)
        {
            if (vfx.name == vfxName)
            {
                vfx.transform.SetParent(transform, false);
                vfx.transform.localPosition = Vector3.zero;
                vfx.gameObject.SetActive(true);

                foreach (var button in _deselectionButtons)
                {
                    button.gameObject.SetActive(false);
                    DontDestroyOnLoad(button.gameObject);
                }
            }
        }
    }
}