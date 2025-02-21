using DG.Tweening;
using UnityEngine;

public class MeteorAnimationBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.transform.localPosition = Vector3.zero;
        this.gameObject.transform.DOLocalMoveY(-33f, 0.4f).onComplete += () =>
        {
            this.gameObject.SetActive(false);
        };
    }

    private void OnDisable()
    {
        this.gameObject.transform.localPosition = Vector3.zero;
    }
}
