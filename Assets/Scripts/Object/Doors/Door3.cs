using DG.Tweening;
using UnityEngine;

public class Door3 : MonoBehaviour
{
    [SerializeField] private ChessGameController _chessGameController;
    [SerializeField] private GameObject board;
    [SerializeField] private float duration;

    [SerializeField] private int vibrato;
    [SerializeField] private float elasticity;
    private Vector3 punch = new Vector3(1, 1, 1);

    private void OnEnable()
    {
        _chessGameController.OnGameEnd += Open;
    }

    public void Open()
    {
        board.transform.DOPunchScale(punch, duration, vibrato, elasticity).OnComplete(() =>
        {
            board.SetActive(false);
        });
    }
}
