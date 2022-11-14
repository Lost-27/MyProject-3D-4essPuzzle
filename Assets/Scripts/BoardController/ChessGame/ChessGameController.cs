using System;
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

    private IGameMode _gameMode;

    // [SerializeField] private ChessUIManager _uiManager;

    private PiecesCreator _pieceCreator;

    private ChessPlayer _whitePlayer;

    private ChessPlayer _blackPlayer;
    private ChessPlayer _activePlayer;
    private GameState _state;

    public ChessPlayer ActivePlayer => _activePlayer;

    public event Action OnGameEnd;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Init();
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
        Piece newPiece = _pieceCreator.CreatePiece(type, _board.transform);
        newPiece.SetData(squareCoords, team, _board);

        _board.SetPieceOnBoard(squareCoords, newPiece);

        ChessPlayer currentPlayer = team == TeamColor.White ? _whitePlayer : _blackPlayer;
        currentPlayer.AddPiece(newPiece);
    }

    public void EndTurn()
    {

        BeginEndTurn();
    }

    public void BeginEndTurn()
    {
        _gameMode.EndTurn();
    }

    public void EndEndTurn()
    {
        GenerateAllPossiblePlayerMoves(_activePlayer);
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(_activePlayer));
        ChangeActiveTeam();
        _activePlayer.StartTurn();
    }


    private void SetDependencies()
    {
        _pieceCreator = GetComponent<PiecesCreator>();
    }

    private void CreatePlayers()
    {
        _whitePlayer = new RealPlayer(TeamColor.White, _board); // real : you
        _blackPlayer = new ChessPlayer(TeamColor.Black, _board); // enemt TODO: Factory create
    }

    private void Init()
    {
        SetDependencies();
        CreatePlayers();
        SetGameState(GameState.Init);
        // _uiManager.HideUI();
        _board.Init(this);
        CreatePiecesFromLayout(_startingBoardLayout);
        _activePlayer = _whitePlayer;
        GenerateAllPossiblePlayerMoves(_activePlayer);
        SetGameState(GameState.Play);

        EndGameCheckerFactory gameCheckerFactory = new EndGameCheckerFactory(this);
        _gameMode = gameCheckerFactory.Create(_endGameType);
        _activePlayer.StartTurn();
    }


    private void GenerateAllPossiblePlayerMoves(ChessPlayer player)
    {
        player.GenerateAllPossibleMoves();
    }


    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        for (int i = 0; i < layout.GetPiecesCount(); i++)
        {
            BoardLayout.BoardSquareSetup setup = layout.GetSquareSetupAtIndex(i);
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColor team = layout.GetSquareTeamColorAtIndex(i);

            string typeName = layout.GetSquarePieceNameAtIndex(i);
            Type type = Type.GetType(typeName);

            CreatePieceAndInitialize(setup.Position, setup.teamColor, type);
        }
    }


    private void RestartGame()
    {
        DestroyPieces();
        _board.Dispose();
        _board.Init(this);
        _whitePlayer.OnGameRestarted();
        _blackPlayer.OnGameRestarted();
        Init();
    }

    public ChessPlayer GetOpponentToPlayer(ChessPlayer player)
    {
        return player == _whitePlayer ? _blackPlayer : _whitePlayer;
    }


    private void ChangeActiveTeam()
    {
        _activePlayer = _activePlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
    }


    public void EndGame()
    {
        Debug.Log("Game End");
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