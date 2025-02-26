using TMPro;
using UnityEngine;

public class SpellPlayerBehaviour : MonoBehaviour
{
    public bool IsSelecting { get; set; }

    public void PlaySpell(int index)
    {
        IsSelecting = true;
        if (CombatManager.Instance.CurrentEntity.name.Contains("Player"))
        {
            CombatManager.Instance.CurrentEntity.Data.Spells[index].StartSelectionPreview();
        }
    }

    public void StopSpell(int index)
    {
        IsSelecting = false;
        if (CombatManager.Instance.CurrentEntity.name.Contains("Player") && index < CombatManager.Instance.CurrentEntity.Data.Spells.Length)
        {
            CombatManager.Instance.CurrentEntity.Data.Spells[index].CancelSelectionPreview();
            CombatManager.Instance.CurrentEntity.Data.Spells[index].CancelSpellPreview();
        }
    }
}
