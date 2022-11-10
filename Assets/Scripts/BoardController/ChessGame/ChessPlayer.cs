using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ChessPlayer
{
    public TeamColor Team { get; set; }
    public Board Board { get; set; }
    public List<Piece> ActivePieces { get; private set; }

    public ChessPlayer(TeamColor team, Board board)
    {
        ActivePieces = new List<Piece>();
        Board = board;
        Team = team;
    }

    public void AddPiece(Piece piece)
    {
        if (!ActivePieces.Contains(piece))
            ActivePieces.Add(piece);
    }

    public void RemovePiece(Piece piece)
    {
        if (ActivePieces.Contains(piece))
            ActivePieces.Remove(piece);
    }

    public void GenerateAllPossibleMoves()
    {
        foreach (var piece in ActivePieces)
        {
            if (Board.HasPiece(piece))
                piece.SelectAvailableSquares();
        }
    }

    public Piece[] GetPieceAttackingOppositePieceOfType<T>() where T : Piece
    {
        return ActivePieces.Where(p => p.IsAttackingPieceOfType<T>()).ToArray();
    }

    public Piece[] GetPiecesOfType<T>() where T : Piece
    {
        return ActivePieces.Where(p => p is T).ToArray();
    }

    public void RemoveMovesEnablingAttackOnPieceOfType<T>(ChessPlayer opponent, Piece selectedPiece) where T : Piece
    {
        List<Vector2Int> coordsToRemove = new List<Vector2Int>();

        coordsToRemove.Clear();
        foreach (var coords in selectedPiece.availableMoves)
        {
            Piece pieceOnCoords = Board.GetPieceOnSquare(coords);
            Board.UpdateBoardOnPieceMove(coords, selectedPiece.occupiedSquare, selectedPiece, null);
            opponent.GenerateAllPossibleMoves();
            if (opponent.CheckIfIsAttackingPiece<T>())
                coordsToRemove.Add(coords);
            Board.UpdateBoardOnPieceMove(selectedPiece.occupiedSquare, coords, selectedPiece, pieceOnCoords);
        }

        foreach (var coords in coordsToRemove)
        {
            selectedPiece.availableMoves.Remove(coords);
        }
    }

    private bool CheckIfIsAttackingPiece<T>() where T : Piece
    {
        foreach (var piece in ActivePieces)
        {
            if (Board.HasPiece(piece) && piece.IsAttackingPieceOfType<T>())
                return true;
        }

        return false;
    }

    public bool CanHidePieceFromAttack<T>(ChessPlayer opponent) where T : Piece
    {
        foreach (var piece in ActivePieces)
        {
            foreach (var coords in piece.availableMoves)
            {
                Piece pieceOnCoords = Board.GetPieceOnSquare(coords);
                Board.UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
                opponent.GenerateAllPossibleMoves();
                if (!opponent.CheckIfIsAttackingPiece<T>())
                {
                    Board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
                    return true;
                }

                Board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
            }
        }

        return false;
    }

    public void OnGameRestarted() // ***pub inte
    {
        ActivePieces.Clear();
    }
}