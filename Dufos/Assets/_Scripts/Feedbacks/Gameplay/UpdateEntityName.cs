using TMPro;
using UnityEngine;

public class UpdateEntityName : MonoBehaviour
{
    private Entity _entity;
    private TextMeshProUGUI _nameDisplay;

    private void Start()
    {
        _entity = this.gameObject.transform.parent.transform.parent.GetComponentInParent<Entity>();
        _nameDisplay = this.gameObject.GetComponent<TextMeshProUGUI>();

        _nameDisplay.text = _entity.Data.EntityName;
    }
}
