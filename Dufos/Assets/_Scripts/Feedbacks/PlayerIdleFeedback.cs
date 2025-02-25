using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleFeedback : MonoBehaviour
{
    [SerializeField]
    private float _height;
    [SerializeField]
    private Material _colorfulMat;
    [SerializeField]
    private List<MeshRenderer> _colorfulMeshes = new();


    private void Awake()
    {
        if(_colorfulMeshes.Count > 0 && _colorfulMat != null)
        {
            foreach (var mesh in _colorfulMeshes)
            {
                mesh.material = _colorfulMat;
            }
        }

        var move = DOTween.Sequence();

        move
            .Append(transform.DOBlendableLocalMoveBy(Vector3.up * _height, 1f)).SetEase(Ease.OutSine)
            .Append(transform.DOBlendableLocalMoveBy(Vector3.down * _height, 1.5f)).SetEase(Ease.InSine);

        move.SetLoops(-1);
        move.Play();
    }
}
