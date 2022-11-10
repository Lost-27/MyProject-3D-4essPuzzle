using System.Collections;
using System.Collections.Generic;
using Object.FirtsDoor;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    [SerializeField] private ChessGameController _chessGameController;
    private Left _left;
    private Right _right;
    [SerializeField] private GameObject Left;
    [SerializeField] private GameObject Right;

    private void Start()
    {
        _left = Left.GetComponent<Left>();
        _right = Right.GetComponent<Right>();
    }
    
    private void OnEnable()
    {
        _chessGameController.OnGameEnd += Open;
    }

    public void Open()
    {
        _left.LeftOpen();
        _right.RightOpen();
    }
}
