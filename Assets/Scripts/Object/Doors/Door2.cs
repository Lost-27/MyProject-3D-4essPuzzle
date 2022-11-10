using DG.Tweening;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    [SerializeField] private ChessGameController _chessGameController;
    [SerializeField] private GameObject board;
    [SerializeField] private float duration;

    [SerializeField] private int vibrato;
    [SerializeField] private float elasticity = 1F;
    private Vector3 punch = new Vector3(0, 20, 20);

    private void OnEnable()
    {
        _chessGameController.OnGameEnd += Open;
    }

    public void Open()
    {
        board.transform.DOPunchRotation(punch, duration, vibrato, elasticity).OnComplete(() =>
        {
            board.SetActive(false);
        });
    }
}
