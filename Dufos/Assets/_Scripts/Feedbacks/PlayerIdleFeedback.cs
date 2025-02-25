using DG.Tweening;
using UnityEngine;

public class PlayerIdleFeedback : MonoBehaviour
{
    [SerializeField]
    private float _height;

    private void Awake()
    {
        var move = DOTween.Sequence();

        move
            .Append(transform.DOBlendableLocalMoveBy(Vector3.up * _height, 1f)).SetEase(Ease.OutSine)
            .Append(transform.DOBlendableLocalMoveBy(Vector3.down * _height, 1.5f)).SetEase(Ease.InSine);

        move.SetLoops(-1);
        move.Play();
    }
}
