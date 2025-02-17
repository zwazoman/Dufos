using TMPro;
using UnityEngine;

public class PlayerCombatInfosDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pmDisplay;

    private void Update()
    {
        Display();
    }

    public void Display()
    {
        if(_pmDisplay.text != CombatManager.Instance.CurrentEntity.MovePoints.ToString() + " Move Points")
        {
            _pmDisplay.text = CombatManager.Instance.CurrentEntity.MovePoints.ToString() + " Move Points";
        }
    }

}
