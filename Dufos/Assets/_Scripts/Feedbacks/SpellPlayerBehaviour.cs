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
}
