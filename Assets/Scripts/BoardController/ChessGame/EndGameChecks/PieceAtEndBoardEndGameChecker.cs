public class PieceAtEndBoardEndGameChecker : IEndGameChecker
{
    
    private readonly Piece[] _pieces;

    public PieceAtEndBoardEndGameChecker(Piece[] pieces)
    {
        _pieces = pieces;
    }
    public bool IsFinished()
    {
        foreach (var piece in _pieces)
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