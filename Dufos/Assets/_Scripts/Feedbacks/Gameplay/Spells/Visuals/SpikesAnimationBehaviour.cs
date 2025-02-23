using DG.Tweening;
using UnityEngine;

public class SpikesAnimationBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _height;
    [SerializeField]
    private Ease _ease;

    private void OnEnable()
    {
        this.gameObject.transform.localPosition = Vector3.zero;

        transform.DOBlendableLocalMoveBy(Vector3.up * _height, _speed).SetEase(_ease).onComplete += () =>
        {
            transform.DOBlendableLocalMoveBy(Vector3.down * _height, _speed).SetEase(_ease).onComplete += () =>
            {
                this.gameObject.SetActive(false);
            };
        };
    }

    private void OnDisable()
    {
        this.gameObject.transform.localPosition = Vector3.zero;
    }
}
