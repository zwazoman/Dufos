using TMPro;
using UnityEngine;

public class FightsWonUpdate : MonoBehaviour
{
    private TextMeshProUGUI _fightDisplay;

    private void Awake()
    {
        _fightDisplay = GetComponent<TextMeshProUGUI>();
        _fightDisplay.text = "Combats gagnés - " + GameManager.instance.FightsWon.ToString();
    }

}
