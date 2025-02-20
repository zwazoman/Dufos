using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellDeselectionBehaviour : MonoBehaviour
{
    SpellPlayerBehaviour _spellPlayer;
    [SerializeField]
    private List<TextMeshProUGUI> _spellUsesDisplay = new();

    private void Awake()
    {
        _spellPlayer = GetComponent<SpellPlayerBehaviour>();
    }

    public void OnDeselectedSpell(int index)
    {
        _spellPlayer.StopSpell(index);

        int currentUses = int.Parse(_spellUsesDisplay[index].text);
        currentUses++;
        _spellUsesDisplay[index].text = currentUses.ToString();
    }
}
