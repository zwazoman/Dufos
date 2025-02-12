using UnityEngine;

public class SpellPlayerBehaviour : MonoBehaviour
{
    public void PlaySpell(int index)
    {
        CombatManager.Instance.CurrentEntity.Data.Spells[index].StartSelectionPreview();
    }
}
