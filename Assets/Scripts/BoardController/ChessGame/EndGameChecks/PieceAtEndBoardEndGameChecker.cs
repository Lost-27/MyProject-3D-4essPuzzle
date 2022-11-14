public class PieceAtEndBoardEndGameChecker : IEndGameChecker
{
    private readonly ChessGameController _controller;

    public PieceAtEndBoardEndGameChecker(ChessGameController controller)
    {
        _controller = controller;
    }

    public bool IsFinished()
    {
        foreach (var piece in _controller.ActivePlayer.ActivePieces)
        {
            int endOfBoardYCoord = piece.team == TeamColor.White ? Board.BOARD_SIZE - 1 : 0;
            if (piece.occupiedSquare.y == endOfBoardYCoord)
            {
                return true;
            }
        }

        return false;
    }
}