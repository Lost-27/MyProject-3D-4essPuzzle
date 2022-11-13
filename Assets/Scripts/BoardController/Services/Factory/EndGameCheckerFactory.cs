public class EndGameCheckerFactory
{
    private readonly ChessGameController _controller;
    private readonly Piece[] _piece;
    
    public EndGameCheckerFactory(ChessGameController controller, Piece[] piece)
    {
        _controller = controller;
        _piece = piece;
    }
    
    public IEndGameChecker Create(EndGameType type)
    {
        switch (type)
        {
            case EndGameType.Checkmate:
                return new CheckmateEndGameChecker(_controller);
            case EndGameType.PieceAtEndBoard:
                return new PieceAtEndBoardEndGameChecker(_piece);
            default:
                return null;
        }
    }
}