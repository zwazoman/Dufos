using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Movement : MonoBehaviour
{
    [SerializeField] Camera _cam;
    Vector3 _mousePosition;

    private void Start()
    {     
        _mousePosition.z = _cam.transform.position.z;
    }
    private void Update()
    {
        _mousePosition = Input.mousePosition;
        if(Input.GetMouseButton(0)) 
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(_mousePosition); 
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow); 
            
        }
    }
}
