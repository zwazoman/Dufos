using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    Camera _cam;
    Vector3 _mousePosition;
    [SerializeField] LayerMask _mask;
    NavMeshAgent _agent;

    private void Awake()
    {
        TryGetComponent(out _agent);
    }

    private void Start()
    {
        _cam = Camera.main;
        
        _mousePosition.z = _cam.transform.position.z;
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        if(Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(_mousePosition); 

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow, _mask);
                _agent.SetDestination(hit.point);
            }    
        }
    }
}
