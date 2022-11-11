public interface ICheckEndGame
{
    bool CheckIfGameIsFinished();
}

public class Checkmate : ICheckEndGame
{
    public bool CheckIfGameIsFinished()
    {
        // Piece[] kingAttackingPieces = _activePlayer.GetPieceAttackingOppositePieceOfType<King>();
        // if (kingAttackingPieces.Length > 0)
        // {
        //     ChessPlayer oppositePlayer = GetOpponentToPlayer(_activePlayer);
        //     Piece attackedKing = oppositePlayer.GetPiecesOfType<King>().FirstOrDefault();
        //     oppositePlayer.RemoveMovesEnablingAttackOnPieceOfType<King>(_activePlayer, attackedKing);
        //
        //     int availableKingMoves = attackedKing.availableMoves.Count;
        //     if (availableKingMoves == 0)
        //     {
        //         bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<King>(_activePlayer);
        //         if (!canCoverKing)
        //             return true;
        //     }
        // }
        //
        // return false;
        throw new System.NotImplementedException();
    }
}

public class PieceAtEndBoard : ICheckEndGame
{
    public bool CheckIfGameIsFinished()
    {
        throw new System.NotImplementedException();
    }
}
