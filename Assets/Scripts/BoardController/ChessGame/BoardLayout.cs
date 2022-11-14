using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]
public class BoardLayout : ScriptableObject
{
    [Serializable]
    public class BoardSquareSetup
    {
        [SerializeField] private Vector2Int position;
        public PieceType pieceType;
        public TeamColor teamColor;

        public Vector2Int Position => new(position.x - 1, position.y - 1);
    }

    [SerializeField] private BoardSquareSetup[] _boardSquares;

    public int GetPiecesCount()
    {
        return _boardSquares.Length;
    }
    
    public BoardSquareSetup GetSquareSetupAtIndex(int value)
    {
        if (_boardSquares.Length <= value)
        {
            Debug.LogError("Index of piece is out range");
            return null;
        }

        return _boardSquares[value];
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