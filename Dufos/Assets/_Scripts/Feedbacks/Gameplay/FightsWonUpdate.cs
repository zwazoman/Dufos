using TMPro;
using UnityEngine;

public class FightsWonUpdate : MonoBehaviour
{
    private TextMeshProUGUI _fightDisplay;

    private void Start()
    {
        _fightDisplay = GetComponent<TextMeshProUGUI>();
        _fightDisplay.text = "Combats gagnés - " + SavedDataCenter.Instance.Data.ClearedCampsCount.ToString();
    }

}
