using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField]
    private Health _playerHealth;
    [SerializeField]
    private Vector3 _offset;

    private Slider _healthBar;
    private Entity _entity;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        _playerHealth.OnTakeDamage += SliderUpdate;

        _entity = _playerHealth.gameObject.GetComponent<Entity>();
    }

    private void Start()
    {
        _healthBar.maxValue = _playerHealth.MaxHealth;
        _healthBar.value = _playerHealth.MaxHealth;
    }

    private void Update()
    {
        this.transform.position = _playerHealth.gameObject.transform.position + _offset;
    }

    public void SliderUpdate(int damage)
    {
        _healthBar.value -= damage;
        if (_healthBar.value <= 0)
        {
            _playerHealth.gameObject.SetActive(false);
            _healthBar.gameObject.SetActive(false);
            print(_playerHealth.gameObject.name + "just died !");

            if (CombatManager.Instance.Entities.Contains(_entity))
            {
                CombatManager.Instance.Entities.Remove(_entity);
            }
        }
    }
}
