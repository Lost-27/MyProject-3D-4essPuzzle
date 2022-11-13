using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PiecesCreator))]
public class ChessGameController : MonoBehaviour
{
    private enum GameState
    {
        Init,
        Play,
        Finished
    }

    [SerializeField] private BoardLayout _startingBoardLayout;
    [SerializeField] private Board _board;
    [SerializeField] private EndGameType _endGameType;

    private IEndGameChecker _gameChecker;

    [SerializeField] private Piece[] _piece;
    // [SerializeField] private ChessUIManager _uiManager;

    private PiecesCreator _pieceCreator;
    private ChessPlayer _whitePlayer;
    private ChessPlayer _blackPlayer;
    public ChessPlayer _activePlayer;
    private GameState _state;

    public event Action OnGameEnd;

    private void Awake()
    {
        SetDependencies();
        CreatePlayers();
    }

    private void Start()
    {
        StartNewGame();
    }

    public bool IsTeamTurnActive(TeamColor team)
    {
        return _activePlayer.Team == team;
    }

    public bool IsGameInProgress()
    {
        return _state == GameState.Play;
    }

    public void RemoveMovesEnablingAttackOnPieceOfType<T>(Piece piece) where T : Piece
    {
        _activePlayer.RemoveMovesEnablingAttackOnPieceOfType<T>(GetOpponentToPlayer(_activePlayer), piece);
    }

    public void OnPieceRemoved(Piece piece)
    {
        ChessPlayer pieceOwner = (piece.team == TeamColor.White) ? _whitePlayer : _blackPlayer;
        pieceOwner.RemovePiece(piece);
        Destroy(piece.gameObject); //*********  ?
    }

    public void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColor team, Type type)
    {
        Piece newPiece = _pieceCreator.CreatePiece(type, _board.transform).GetComponent<Piece>();
        newPiece.SetData(squareCoords, team, _board);

        Material teamMaterial = _pieceCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);

        _board.SetPieceOnBoard(squareCoords, newPiece);

        ChessPlayer currentPlayer = team == TeamColor.White ? _whitePlayer : _blackPlayer;
        currentPlayer.AddPiece(newPiece);
    }

    public void EndTurn()
    {
        GenerateAllPossiblePlayerMoves(_activePlayer);
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(_activePlayer));

        if (_gameChecker.IsFinished())
        {
            EndGame();
        }
        
        else
        {
            // RestartGame();
            ChangeActiveTeam();
        }
    }


    private void SetDependencies()
    {
        _pieceCreator = GetComponent<PiecesCreator>();
    }

    private void CreatePlayers()
    {
        _whitePlayer = new ChessPlayer(TeamColor.White, _board);
        _blackPlayer = new ChessPlayer(TeamColor.Black, _board);
    }

    private void StartNewGame()
    {
        SetGameState(GameState.Init);
        // EndGameCheckerFactory _gameCheckerFactory = new EndGameCheckerFactory(this,_piece);
        // _gameChecker =  _gameCheckerFactory.Create(_endGameType);
        // _uiManager.HideUI();
        _board.SetDependencies(this);
        CreatePiecesFromLayout(_startingBoardLayout);
        _activePlayer = _whitePlayer;
        GenerateAllPossiblePlayerMoves(_activePlayer);
        SetGameState(GameState.Play);
        
        _piece = FindObjectsOfType<Piece>();
        EndGameCheckerFactory _gameCheckerFactory = new EndGameCheckerFactory(this,_piece);
        _gameChecker =  _gameCheckerFactory.Create(_endGameType);
    }


    private void GenerateAllPossiblePlayerMoves(ChessPlayer player)
    {
        player.GenerateAllPossibleMoves();
    }


    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        for (int i = 0; i < layout.GetPiecesCount(); i++)
        {
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColor team = layout.GetSquareTeamColorAtIndex(i);

            string typeName = layout.GetSquarePieceNameAtIndex(i);
            Type type = Type.GetType(typeName);

            CreatePieceAndInitialize(squareCoords, team, type);
        }
    }


    private void RestartGame()
    {
        DestroyPieces();
        _board.OnGameRestarted();
        _whitePlayer.OnGameRestarted();
        _blackPlayer.OnGameRestarted();
        StartNewGame();
    }

    public ChessPlayer GetOpponentToPlayer(ChessPlayer player)
    {
        return player == _whitePlayer ? _blackPlayer : _whitePlayer;
    }

    private void ChangeActiveTeam()
    {
        _activePlayer = _activePlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
    }


    private void EndGame()
    {
        Debug.Log("Game Ended");
        SetGameState(GameState.Finished);

        OnGameEnd?.Invoke();
        // _uiManager.OnGameFinished(_activePlayer.Team.ToString());
        DestroyPieces();
    }

    private void SetGameState(GameState state)
    {
        _state = state;
    }


    private void DestroyPieces()
    {
        _whitePlayer.ActivePieces.ForEach(p => Destroy(p.gameObject));
        _blackPlayer.ActivePieces.ForEach(p => Destroy(p.gameObject));
    }
}