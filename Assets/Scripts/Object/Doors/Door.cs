using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
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
        transform.DOScale(transform.localScale / _scaleMultiplier, _duration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}