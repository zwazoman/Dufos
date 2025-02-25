using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class VfxDisablingBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _inGameUI;
    [SerializeField]
    private SpellPlayerBehaviour _spellPlayer;
    private CinemachineVirtualCamera _cam;

    private void Awake()
    {
        _cam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        _inGameUI.SetActive(false);
    }

    private void OnDisable()
    {
        _spellPlayer.IsSelecting = false;
        _cam.LookAt = CombatManager.Instance.CurrentEntity.gameObject.transform;
        DOTween.To(() => _cam.m_Lens.FieldOfView, x => _cam.m_Lens.FieldOfView = x, 75f, 0.5f).SetEase(Ease.InQuad);
        _inGameUI.SetActive(true);
    }
}
