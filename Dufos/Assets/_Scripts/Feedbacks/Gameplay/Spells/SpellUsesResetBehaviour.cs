using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellUsesResetBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _spellUsesDisplay = new();

    public void ResetUses()
    {
        foreach(var spell in _spellUsesDisplay)
        {
            spell.text = CombatManager.Instance.CurrentEntity.Data.Spells[_spellUsesDisplay.IndexOf(spell)].Data.Uses.ToString();
        }
    }
}
