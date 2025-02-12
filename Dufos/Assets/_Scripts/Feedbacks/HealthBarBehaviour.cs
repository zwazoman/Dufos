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

    public void SliderUpdate(int health)
    {
        _healthBar.value = health;
    }
}
