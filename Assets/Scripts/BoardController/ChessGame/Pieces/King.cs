using System;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    Vector2Int[] _directions =
    {
        new(-1, 1),
        new(0, 1),
        new(1, 1),
        new(-1, 0),
        new(1, 0),
        new(-1, -1),
        new(0, -1),
        new(1, -1),
    };

    private Piece _leftRook;
    private Piece _rightRook;
    
    private Vector2Int _leftCastlingMove;
    private Vector2Int _rightCastlingMove;

    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        AssignStandardMoves();
        AssignCastlingMoves();
        return availableMoves;
    }

    private void AssignStandardMoves()
    {
        float range = 1;
        foreach (var direction in _directions)
        {
            for (int i = 1; i <= range; i++)
            {
                Vector2Int nextCoords = occupiedSquare + direction * i;
                Piece piece = board.GetPieceOnSquare(nextCoords);
                if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                    break;
                if (piece == null)
                    TryToAddMove(nextCoords);
                else if (!piece.IsFromSameTeam(this))
                {
                    TryToAddMove(nextCoords);
                    break;
                }
                else if (piece.IsFromSameTeam(this))
                    break;
            }
        }
    }

    private void AssignCastlingMoves()
    {
        _leftCastlingMove = new Vector2Int(-1, -1);
        _rightCastlingMove = new Vector2Int(-1, -1);
        if (!hasMoved)
        {
            _leftRook = GetPieceInDirection<Rook>(team, Vector2Int.left);
            if (_leftRook && !_leftRook.hasMoved)
            {
                _leftCastlingMove = occupiedSquare + Vector2Int.left * 2;
                availableMoves.Add(_leftCastlingMove);
            }

            _rightRook = GetPieceInDirection<Rook>(team, Vector2Int.right);
            if (_rightRook && !_rightRook.hasMoved)
            {
                _rightCastlingMove = occupiedSquare + Vector2Int.right * 2;
                availableMoves.Add(_rightCastlingMove);
            }
        }
    }

    private Piece GetPieceInDirection<T>(TeamColor team, Vector2Int direction)
    {
        for (int i = 1; i <= Board.BOARD_SIZE; i++)
        {
            Vector2Int nextCoords = occupiedSquare + direction * i;
            Piece piece = board.GetPieceOnSquare(nextCoords);
            if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                return null;
            if (piece != null)
            {
                if (piece.team != team || !(piece is T))
                    return null;
                else if (piece.team == team && piece is T)
                    return piece;
            }
        }

        return null;
    }

    public void MovePiece(Vector2Int coords)//,Action competedCallback = null)
    {
        base.MovePiece(coords);
        if (coords == _leftCastlingMove)
        {
            board.UpdateBoardOnPieceMove(coords + Vector2Int.right, _leftRook.occupiedSquare, _leftRook, null);
            _leftRook.MovePiece(coords + Vector2Int.right);
        }
        else if (coords == _rightCastlingMove)
        {
            board.UpdateBoardOnPieceMove(coords + Vector2Int.left, _rightRook.occupiedSquare, _rightRook, null);
            _rightRook.MovePiece(coords + Vector2Int.left);
        }
    }
}