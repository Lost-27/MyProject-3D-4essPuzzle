using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    private readonly Vector2Int[] _directions = 
    {
        new(1, 1),
        new(1, -1),
        new(-1, 1),
        new(-1,- 1),
    };
    
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        float range = Board.BOARD_SIZE;
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
        return availableMoves;
    }
}
