public class EndGameCheckerFactory
{
    private readonly ChessGameController _controller;

    public EndGameCheckerFactory(ChessGameController controller)
    {
        _controller = controller;
    }

    public IEndGameChecker Create(EndGameType type)
    {
        switch (type)
        {
            case EndGameType.Checkmate:
                return new CheckmateEndGameChecker(_controller);
            case EndGameType.PieceAtEndBoard:
                return new PieceAtEndBoardEndGameChecker(_controller);
            default:
                return null;
        }
    }
}