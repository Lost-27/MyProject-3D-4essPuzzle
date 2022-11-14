using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SquareSelectorCreator))]
public class Board : MonoBehaviour
{
    public const int BOARD_SIZE = 8;

    [SerializeField] private Transform _bottomLeftSquareTransform;
    [SerializeField] private float _squareSize;

    private Piece[,] _grid;
    private Piece _selectedPiece;
    private ChessGameController _chessController;
    private SquareSelectorCreator _squareSelector;


    private void Awake()
    {
        _squareSelector = GetComponent<SquareSelectorCreator>();
        CreateGrid();
    }

    public void SetDependencies(ChessGameController chessController)
    {
        _chessController = chessController;
    }

    public Vector3 CalculatePositionFromCoords(Vector2Int coords)
    {
        Vector3 leftSquareLocalPos = transform.InverseTransformPoint(_bottomLeftSquareTransform.position);
        return leftSquareLocalPos + new Vector3(coords.x * _squareSize, 0f, coords.y * _squareSize);
    }


    public void OnSquareSelected(Vector3 inputPosition)
    {
        if (!_chessController.IsGameInProgress())
            return;

        Vector2Int coords = CalculateCoordsFromPosition(inputPosition);
        Piece piece = GetPieceOnSquare(coords);
        if (_selectedPiece)
        {
            if (piece != null && _selectedPiece == piece)
                DeselectPiece();
            else if (piece != null && _selectedPiece != piece && _chessController.IsTeamTurnActive(piece.team))
                SelectPiece(piece);
            else if (_selectedPiece.CanMoveTo(coords))
                OnSelectedPieceMoved(coords, _selectedPiece);
        }
        else
        {
            if (piece != null && _chessController.IsTeamTurnActive(piece.team))
                SelectPiece(piece);
        }
    }


    public Piece GetPieceOnSquare(Vector2Int coords)
    {
        if (CheckIfCoordinatesAreOnBoard(coords))
            return _grid[coords.x, coords.y];
        return null;
    }

    public void SetPieceOnBoard(Vector2Int coords, Piece piece)
    {
        if (CheckIfCoordinatesAreOnBoard(coords))
            _grid[coords.x, coords.y] = piece;
    }

    public bool CheckIfCoordinatesAreOnBoard(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0 || coords.x >= BOARD_SIZE || coords.y >= BOARD_SIZE)
            return false;
        return true;
    }


    public bool HasPiece(Piece piece)
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                if (_grid[i, j] == piece)
                    return true;
            }
        }

        return false;
    }

    public void UpdateBoardOnPieceMove(Vector2Int newCoords, Vector2Int oldCoords, Piece newPiece, Piece oldPiece)
    {
        _grid[oldCoords.x, oldCoords.y] = oldPiece;
        _grid[newCoords.x, newCoords.y] = newPiece;
    }

    public void PromotePiece(Piece piece)
    {
        TakePiece(piece);
        _chessController.CreatePieceAndInitialize(piece.occupiedSquare, piece.team, typeof(Queen));
    }

    public void OnGameRestarted()
    {
        _selectedPiece = null;
        CreateGrid();
    }

    private void CreateGrid()
    {
        _grid = new Piece[BOARD_SIZE, BOARD_SIZE];
    }

    private Vector2Int CalculateCoordsFromPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).x / _squareSize) +
                BOARD_SIZE / 2;
        int y = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).z / _squareSize) +
                BOARD_SIZE / 2;
        return new Vector2Int(x, y);
    }

    private void SelectPiece(Piece piece)
    {
        _chessController.RemoveMovesEnablingAttackOnPieceOfType<King>(piece);
        _selectedPiece = piece;
        List<Vector2Int> selection = _selectedPiece.AvailableMoves;
        ShowSelectionSquares(selection);
    }

    private void ShowSelectionSquares(List<Vector2Int> selection)
    {
        Dictionary<Vector3, bool> squaresData = new Dictionary<Vector3, bool>();
        for (int i = 0; i < selection.Count; i++)
        {
            Vector3 position = CalculatePositionFromCoords(selection[i]);
            bool isSquareFree = GetPieceOnSquare(selection[i]) == null;
            squaresData.Add(position, isSquareFree);
        }

        _squareSelector.ShowSelection(squaresData, transform);
    }

    private void DeselectPiece()
    {
        _selectedPiece = null;
        _squareSelector.ClearSelection();
    }

    private void OnSelectedPieceMoved(Vector2Int coords, Piece piece)
    {
        TryToTakeOppositePiece(coords);
        UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
        
        _selectedPiece.MovePiece(coords, EndTurn);
        DeselectPiece();
    }

    private void TryToTakeOppositePiece(Vector2Int coords)
    {
        Piece piece = GetPieceOnSquare(coords);
        if (piece && !_selectedPiece.IsFromSameTeam(piece)) // **
        {
            TakePiece(piece);
        }
    }

    private void TakePiece(Piece piece)
    {
        if (piece)
        {
            _grid[piece.occupiedSquare.x, piece.occupiedSquare.y] = null;
            _chessController.OnPieceRemoved(piece);
            Destroy(piece.gameObject);
        }
    }

    private void EndTurn()
    {
        _chessController.EndTurn();
    }
}