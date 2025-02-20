using UnityEngine;

public class VfxDisablingBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _inGameUI;

    private void OnEnable()
    {
        _inGameUI.SetActive(false);
    }

    private void OnDisable()
    {
        _inGameUI.SetActive(true);
    }
}
