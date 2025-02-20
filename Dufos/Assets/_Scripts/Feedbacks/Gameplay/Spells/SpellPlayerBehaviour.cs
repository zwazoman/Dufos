using TMPro;
using UnityEngine;

public class SpellPlayerBehaviour : MonoBehaviour
{
    public void PlaySpell(int index)
    {
        if (CombatManager.Instance.CurrentEntity.name.Contains("Player"))
        {
            CombatManager.Instance.CurrentEntity.Data.Spells[index].StartSelectionPreview();
        }
    }

    public void StopSpell(int index)
    {
        if (CombatManager.Instance.CurrentEntity.name.Contains("Player") && index < CombatManager.Instance.CurrentEntity.Data.Spells.Length)
        {
            CombatManager.Instance.CurrentEntity.Data.Spells[index].CancelSelectionPreview();
            CombatManager.Instance.CurrentEntity.Data.Spells[index].CancelSpellPreview();
        }
    }
}
