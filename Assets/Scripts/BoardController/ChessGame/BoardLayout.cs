using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]
public class BoardLayout : ScriptableObject
{
    [Serializable]
    private class BoardSquareSetup
    {
        public Vector2Int position;
        public PieceType pieceType;
        public TeamColor teamColor;
    }

    [SerializeField] private BoardSquareSetup[] _boardSquares;

    public int GetPiecesCount()
    {
        return _boardSquares.Length;
    }

    public Vector2Int GetSquareCoordsAtIndex(int index)
    {
        if (_boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out range");
            return new Vector2Int(-1, -1);
        }
        return new Vector2Int(_boardSquares[index].position.x - 1, _boardSquares[index].position.y - 1);
    }

    public string GetSquarePieceNameAtIndex(int index)
    {
        if (_boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out range");
            return "";
        }
        return _boardSquares[index].pieceType.ToString();
    }

    public TeamColor GetSquareTeamColorAtIndex(int index)
    {
        if (_boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out range");
            return TeamColor.Black;
        }
        return _boardSquares[index].teamColor;
    }
}