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
            print("hit" +  hit.collider.gameObject.name);
            _currentPnj = hit.collider.gameObject;

            if (_currentPnj.transform.childCount > 0)
            {
                _detected = true;
                print("done");
                _currentPnj.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        else if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _detectionRange) && _currentPnj != null)
        {
            if (_currentPnj.transform.childCount <= 0) return;

            _detected = false;
            _currentPnj.transform.GetChild(0).gameObject.SetActive(false);
            _currentPnj = null;
        }
    }
}
