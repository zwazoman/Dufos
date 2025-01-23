using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Movement : MonoBehaviour
{
    Camera _cam;
    Vector3 _mousePosition;
    [SerializeField] LayerMask _mask;
    NavMeshAgent _agent;

    private void Start()
    {
        _cam = Camera.main;
        TryGetComponent(out _agent);
        _mousePosition.z = _cam.transform.position.z;
    }
    private void Update()
    {
        _mousePosition = Input.mousePosition;
        if(Input.GetMouseButton(0)) 
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(_mousePosition); 

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                _agent.SetDestination(hit.point);
            }    
        }
    }
}
