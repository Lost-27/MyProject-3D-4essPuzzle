using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    private readonly Vector2Int[] _offsets =
    {
        new(2, 1),
        new(2, -1),
        new(1, 2),
        new(1, -2),
        new(-2, 1),
        new(-2, -1),
        new(-1, 2),
        new(-1, -2),
    };

    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();

        for (int i = 0; i < _offsets.Length; i++)
        {
            Vector2Int nextCoords = occupiedSquare + _offsets[i];
            Piece piece = board.GetPieceOnSquare(nextCoords);
            if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                continue;
            if (piece == null || !piece.IsFromSameTeam(this))
                TryToAddMove(nextCoords);
        }

        return availableMoves;
    }
}