using System.Linq;

public class CheckmateGameMode : IGameMode
{
    private readonly ChessGameController _controller;

    public CheckmateGameMode(ChessGameController controller)
    {
        _controller = controller;
    }

    public void Init()
    {
        // initizle
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

            int availableKingMoves = attackedKing.AvailableMoves.Count;
            if (availableKingMoves == 0)
            {
                bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<King>(activePlayer);
                if (!canCoverKing)
                    return true;
            }
        }

        return false;
    }

    public void EndTurn()
    {
        if (IsFinished())
        {
            _controller.EndGame();
        }
        else
        {
            // _controller.Restart();
        }
    }
}