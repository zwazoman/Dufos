using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellDisplayBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Image> _spellImages;
    [SerializeField]
    private List<Button> _spellButtons;
    [SerializeField]
    private List<TextMeshProUGUI> _spellUsesDisplay;
    [SerializeField]
    private GameObject _turnAndMoveDisplay;

    private void Update()
    {
        if (CombatManager.Instance.CurrentEntity.name.Contains("Enemy"))
        {
            foreach(var spell in _spellImages)
            {
                spell.gameObject.SetActive(false);
                _turnAndMoveDisplay.gameObject.SetActive(false);
            }
        }

        else
        {
            foreach (var spell in _spellImages)
            {
                int i = _spellImages.Count - 1;
                for (; i >= CombatManager.Instance.CurrentEntity.Data.Spells.Length; i--)
                {
                    _spellImages[i].gameObject.SetActive(false);
                }

                for (; i >= 0; i--)
                {
                    _spellImages[i].gameObject.SetActive(true);
                }

                if (spell.gameObject.activeInHierarchy && spell.sprite != CombatManager.Instance.CurrentEntity.Data.Spells[_spellImages.IndexOf(spell)].Data.UISprite)
                {
                    _turnAndMoveDisplay.gameObject.SetActive(true);
                    spell.sprite = CombatManager.Instance.CurrentEntity.Data.Spells[_spellImages.IndexOf(spell)].Data.UISprite;
                    foreach (var spellUse in _spellUsesDisplay)
                    {
                        spellUse.text = CombatManager.Instance.CurrentEntity.Data.Spells[_spellImages.IndexOf(spell)].Data.Uses.ToString();
                    }
                }
            }
        }

    }
}
