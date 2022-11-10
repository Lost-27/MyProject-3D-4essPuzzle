using System;
using UnityEngine;

[RequireComponent(typeof(Board))]
public class BoardInputHandler : MonoBehaviour, IInputHandler
{
    private Board _board;

    private void Awake()
    {
        _board = GetComponent<Board>();
    }


    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        _board.OnSquareSelected(inputPosition);
    }
}