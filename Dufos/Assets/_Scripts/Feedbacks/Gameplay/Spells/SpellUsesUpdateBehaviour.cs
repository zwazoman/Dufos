using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUsesUpdateBehaviour : MonoBehaviour
{
    private TextMeshProUGUI _usesDisplay;
    private Button _spellButton;

    private void Awake()
    {
        _usesDisplay = GetComponent<TextMeshProUGUI>();
        _spellButton = GetComponentInParent<Button>();
    }

    private void Update()
    {
        if (int.Parse(_usesDisplay.text) > 0)
        {
            _spellButton.interactable = true;
        }
    }

    public void UseSpellDisplay()
    {
        int uses = int.Parse(_usesDisplay.text);

        if (uses <= 1)
        {
            uses = 0;
            _spellButton.interactable = false;
        }

        else
        {
            uses--;
        }

        _usesDisplay.text = uses.ToString();
    }
}
