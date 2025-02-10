using UnityEngine;

public class DialogueDetectionBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _detectionRange;
    private bool _detected;
    private GameObject _currentPnj;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, _detectionRange) && !_detected)
        {
            _detected = true;
            _currentPnj = hit.collider.gameObject;
            _currentPnj.transform.GetChild(0).gameObject.SetActive(true);
        }

        else if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1) && _currentPnj != null)
        {
            _detected = false;
            _currentPnj.transform.GetChild(0).gameObject.SetActive(false);
            _currentPnj = null;
        }
    }
}
