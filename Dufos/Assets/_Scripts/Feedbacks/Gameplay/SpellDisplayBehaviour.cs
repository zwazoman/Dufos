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

    private Entity _previousEntity;

    private void Update()
    {
        if (_previousEntity == null || _previousEntity != CombatManager.Instance.CurrentEntity)
        {
            _previousEntity = CombatManager.Instance.CurrentEntity;

            foreach (var spell in _spellImages)
            {
                spell.gameObject.SetActive(_previousEntity.name.Contains("Player"));
                _turnAndMoveDisplay.gameObject.SetActive(_previousEntity.name.Contains("Player"));

                if (spell.gameObject.activeInHierarchy)
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

                    _turnAndMoveDisplay.gameObject.SetActive(true);

                    if (_spellImages.IndexOf(spell) < CombatManager.Instance.CurrentEntity.Data.Spells.Length)
                    {
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
}
