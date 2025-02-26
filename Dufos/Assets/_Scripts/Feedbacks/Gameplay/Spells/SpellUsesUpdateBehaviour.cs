using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUsesUpdateBehaviour : MonoBehaviour
{
    [SerializeField]
    private Button _otherDeselectionButton;
    private TextMeshProUGUI _usesDisplay;
    private Button _spellButton;

    private void Awake()
    {
        _usesDisplay = GetComponent<TextMeshProUGUI>();
        _spellButton = GetComponentInParent<Button>();
    }

    private void Update()
    {
        if (int.Parse(_usesDisplay.text) > 0 && !_otherDeselectionButton.gameObject.activeInHierarchy)
        {
            _spellButton.interactable = true;
        }

        else if (_otherDeselectionButton.gameObject.activeInHierarchy)
        {
            _spellButton.interactable = false;
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
