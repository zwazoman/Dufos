using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject _player;
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, 15, _player.transform.position.z);   
    }
}
