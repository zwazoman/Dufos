using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField]
    private Health _playerHealth;
    [SerializeField]
    private Vector3 _offset;

    private Slider _healthBar;
    private EnemyEntity _enemy;
    private PlayerEntity _player;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        _playerHealth.OnTakeDamage += SliderUpdate;
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
            if (_playerHealth.gameObject.TryGetComponent(out Gobelin _enemy))
            {
                CombatManager.Instance.Entities.Remove(_enemy);
                CombatManager.Instance.EnemyEntities.Remove(_enemy);
            }

            else if (_playerHealth.gameObject.TryGetComponent(out PlayerEntity _player))
            {
                CombatManager.Instance.Entities.Remove(_player);
                CombatManager.Instance.PlayerEntities.Remove(_player);
            }

            _playerHealth.gameObject.SetActive(false);
            _healthBar.gameObject.SetActive(false);
            print(_playerHealth.gameObject.name + "just died !");
        }
    }
}
