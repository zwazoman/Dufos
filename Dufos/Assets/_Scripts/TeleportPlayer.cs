using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private void Start()
    {
        Player.transform.position = GameManager.Instance.PlayerPosition;
    }
}
