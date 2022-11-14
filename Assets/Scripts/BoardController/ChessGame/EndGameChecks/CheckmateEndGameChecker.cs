using System.Linq;

public class CheckmateEndGameChecker : IEndGameChecker
{
    private readonly ChessGameController _controller;

    public CheckmateEndGameChecker(ChessGameController controller)
    {
        _controller = controller;
    }

    public bool IsFinished()
    {
        ChessPlayer activePlayer = _controller.ActivePlayer;

        Piece[] kingAttackingPieces = activePlayer.GetPieceAttackingOppositePieceOfType<King>();
        if (kingAttackingPieces.Length > 0)
        {
            ChessPlayer oppositePlayer = _controller.GetOpponentToPlayer(activePlayer);
            Piece attackedKing = oppositePlayer.GetPiecesOfType<King>().FirstOrDefault();
            oppositePlayer.RemoveMovesEnablingAttackOnPieceOfType<King>(activePlayer, attackedKing);

            int availableKingMoves = attackedKing.availableMoves.Count;
            if (availableKingMoves == 0)
            {
                bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<King>(activePlayer);
                if (!canCoverKing)
                    return true;
            }
        }

        return false;
    }
}