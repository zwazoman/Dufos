using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnTakeDamage;
    public event Action<int> OnTakeHeal;

    public event Action OnDie;

    [HideInInspector] public int MaxHealth;

    float _currentHealth;

    public void ApplyDamage(int damage)
    {
        OnTakeDamage?.Invoke(damage);

        AudioManager.Instance.PlaySFXClip(Sounds.Damage, 0.3f);

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            AudioManager.Instance.PlaySFXClip(Sounds.Death, 0.2f);

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
