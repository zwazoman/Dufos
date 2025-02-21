using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    [SerializeField]
    private Health _playerHealth;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private EntityOrderDisplay _order;
    [SerializeField]
    private ScreenShakeBehaviour _screenShake;
    private Entity _entity;

    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        _entity = _playerHealth.gameObject.GetComponent<Entity>();
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
        transform.LookAt(Camera.main.ScreenToWorldPoint(transform.forward));
        this.transform.Rotate(0, 180, 0);
    }

    public void SliderUpdate(int damage)
    {
        _healthBar.DOValue(_healthBar.value - damage, 0.15f).SetDelay(0.75f).onComplete += () =>
        {
            OnHealthChanged(_healthBar.value);
            _screenShake.Shake();

            if (_healthBar.value <= 0 && _playerHealth.gameObject.activeInHierarchy)
            {

                if (_entity == CombatManager.Instance.CurrentEntity)
                {
                    _order.UpdateOrder();
                    CombatManager.Instance.NextTurn();
                }

                _playerHealth.gameObject.SetActive(false);
                _healthBar.gameObject.SetActive(false);

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
            }
        };
    }
}
