using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoMoreMovesFeedback : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _movePoints;
    [SerializeField]
    private List<TextMeshProUGUI> _spellUses;

    private Animator _anim;
    private Image _image;

    private int _emptySpells = 0;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _emptySpells = 0;
        if (!_anim.enabled && _movePoints.text.StartsWith("0"))
        {
            foreach (var use in _spellUses)
            {
                if (use.text.StartsWith("0"))
                {
                    _emptySpells++;
                    if(_emptySpells == CombatManager.Instance.CurrentEntity.Data.Spells.Length)
                    {
                        _anim.enabled = true;
                    }
                }
            }
        }
    }

    public void ResetTurnAnim()
    {
        _image.color = Color.white;
    }
}
