using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityOrderDisplay : MonoBehaviour
{
    [SerializeField]
    private List<Image> _orderDisplay = new();
    private List<Entity> _entitiesCopy = new();

    private void Update()
    {
        if (CombatManager.Instance.Entities.Count > 0 && _entitiesCopy != CombatManager.Instance.Entities)
        {
            _entitiesCopy.Clear();
            _entitiesCopy = CombatManager.Instance.Entities;
            for (int i = 0; i < _orderDisplay.Count; i++)
            {
                _orderDisplay[i].sprite = CombatManager.Instance.Entities[i].Data.EntitySpriteUI;
            }
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
                _orderDisplay[i].sprite = _entitiesCopy[i].Data.EntitySpriteUI;
            }

            else
            {
                _orderDisplay[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
