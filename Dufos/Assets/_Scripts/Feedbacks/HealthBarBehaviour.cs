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
        _playerHealth.OnTakeDamage += SliderUpdate;
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
