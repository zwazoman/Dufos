using TMPro;
using UnityEngine;

public class HealthValueTextFeedback : MonoBehaviour
{
    private Entity _entity;
    private HealthBarBehaviour _health;
    private TextMeshProUGUI _healthDisplay;

    private void Awake()
    {
        _entity = this.gameObject.transform.parent.transform.parent.GetComponentInParent<Entity>();

        _healthDisplay = GetComponent<TextMeshProUGUI>();
        _healthDisplay.text = _entity.Data.MaxHealth.ToString() ;

        _health = GetComponentInParent<HealthBarBehaviour>();
        _health.OnHealthChanged += UpdateHealthText;
    }

    private void UpdateHealthText(float health)
    {
        _healthDisplay.text = health.ToString();
    }
}
