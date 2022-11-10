using DG.Tweening;
using UnityEngine;

public class Door4 : MonoBehaviour
{
    [SerializeField] private ChessGameController _chessGameController;
    [SerializeField] private float _duration;
    [SerializeField] private float _scaleMultiplier;

    private void OnEnable()
    {
        _chessGameController.OnGameEnd += Open;
    }

    private void Open()
    {
        transform.DORotate(new Vector3(0, -360, 0), _duration, RotateMode.LocalAxisAdd).SetLoops(-1)
            .SetEase(Ease.Linear);
        transform.DOScale(transform.localScale / _scaleMultiplier, _duration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}