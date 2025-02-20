using Cinemachine;
using UnityEngine;

public class ScreenShakeBehaviour : MonoBehaviour
{
    private CinemachineImpulseSource _source;

    public void Shake()
    {
        _source.GenerateImpulse();
    }
}
