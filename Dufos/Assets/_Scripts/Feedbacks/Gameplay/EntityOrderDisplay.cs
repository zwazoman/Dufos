using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityOrderDisplay : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _orderDisplay = new();
    private List<Entity> _entitiesCopy = new();
    private bool _firstOrder = true;

    private void Update()
    {
        if (CombatManager.Instance.Entities.Count > 0 && _entitiesCopy != CombatManager.Instance.Entities)
        {
            _entitiesCopy.Clear();
            _entitiesCopy = CombatManager.Instance.Entities;
            for (int i = 0; i < _orderDisplay.Count; i++)
            {
                _orderDisplay[i].text = CombatManager.Instance.Entities[i].name;
            }
            _firstOrder = false;
        }
    }
    public void UpdateOrder()
    {
        var temp = _entitiesCopy[0];

        _entitiesCopy.RemoveAt(0);
        _entitiesCopy.Add(temp);

        for (int i = 0; i < _orderDisplay.Count; i++)
        {
            if(_entitiesCopy.Count > i)
            {
                _orderDisplay[i].text = _entitiesCopy[i].name;
            }

            else
            {
                _orderDisplay[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
