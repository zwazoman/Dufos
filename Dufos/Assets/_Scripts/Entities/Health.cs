using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnTakeDamage;
    public event Action<int> OnTakeHeal;

    public event Action OnDie;

    public int MaxHealth;

    float _currentHealth;

    public void ApplyDamage(int damage)
    {
        OnTakeDamage?.Invoke(damage);

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            OnDie?.Invoke();
        }
    }

    public void AppyHeal(int heal)
    {
        OnTakeHeal?.Invoke(heal);

        _currentHealth += heal;
        if(_currentHealth >= MaxHealth)
        {
            _currentHealth = MaxHealth;
        }
    }
}
