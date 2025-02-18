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

    private void Update()
    {
        foreach(var spell in _spellImages)
        {
            int i = _spellImages.Count - 1;
            for (; i >= CombatManager.Instance.CurrentEntity.Data.Spells.Length; i--)
            {
                _spellImages[i].gameObject.SetActive(false);
            }

            for(;i >= 0; i--)
            {
                _spellImages[i].gameObject.SetActive(true);
            }

            if(spell.gameObject.activeInHierarchy && spell.sprite != CombatManager.Instance.CurrentEntity.Data.Spells[_spellImages.IndexOf(spell)].Data.UISprite)
            {
                spell.sprite = CombatManager.Instance.CurrentEntity.Data.Spells[_spellImages.IndexOf(spell)].Data.UISprite;
                foreach(var spellUse in _spellUsesDisplay)
                {
                    spellUse.text = CombatManager.Instance.CurrentEntity.Data.Spells[_spellImages.IndexOf(spell)].Data.Uses.ToString();
                }
            }
        }
    }
}
