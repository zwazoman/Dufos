using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Vector3 _offset;

    private void Update()
    {
        this.transform.position = _player.transform.position + _offset;
    }
}
