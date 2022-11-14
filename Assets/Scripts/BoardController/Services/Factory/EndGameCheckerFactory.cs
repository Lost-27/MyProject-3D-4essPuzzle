public class EndGameCheckerFactory
{
    private readonly ChessGameController _controller;

    public EndGameCheckerFactory(ChessGameController controller)
    {
        _controller = controller;
    }

    public IGameMode Create(EndGameType type)
    {
        switch (type)
        {
            case EndGameType.Checkmate:
                return new CheckmateGameMode(_controller);
            case EndGameType.PieceAtEndBoard:
                return new PieceAtBoardGameMode(_controller);
            default:
                return null;
        }
    }
}