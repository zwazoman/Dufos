using Cinemachine;
using UnityEngine;

public class ScreenShakeBehaviour : MonoBehaviour
{
    private CinemachineImpulseSource _source;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake()
    {
        _source.GenerateImpulse();
    }
}
