using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField]
    private Health _playerHealth;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private EntityOrderDisplay _order;
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
        transform.LookAt(Camera.main.ScreenToWorldPoint(Vector3.forward));
    }

    public void SliderUpdate(int damage)
    {
        _healthBar.value -= damage;

        if (_healthBar.value <= 0 && _playerHealth.gameObject.activeInHierarchy)
        {
            if(_entity == CombatManager.Instance.CurrentEntity)
            {
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
                _order.UpdateOrder();

                CombatManager.Instance.Entities.Remove(_player);
                CombatManager.Instance.PlayerEntities.Remove(_player);
            }
        }
    }
}
