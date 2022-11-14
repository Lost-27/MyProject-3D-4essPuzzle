using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialSetter2D))]
[RequireComponent(typeof(IObjectTweener))]
public abstract class Piece : MonoBehaviour
{
    [SerializeField] private MaterialSetter2D materialSetter2D;
    public Board board { protected get; set; }
    public Vector2Int occupiedSquare { get; set; }
    public TeamColor team { get; set; }
    public bool hasMoved { get; private set; }
    
    public List<Vector2Int> AvailableMoves;

    private IObjectTweener _tweener;

    public abstract List<Vector2Int> SelectAvailableSquares();

    private void Awake()
    {
        AvailableMoves = new List<Vector2Int>();
        _tweener = GetComponent<IObjectTweener>();
        materialSetter2D = GetComponent<MaterialSetter2D>();
        hasMoved = false;
    }

    public void SetMaterial(Material selectedMaterial)
    {
        materialSetter2D.SetSingleMaterial(selectedMaterial);
    }

    public bool IsFromSameTeam(Piece piece)
    {
        return team == piece.team;
    }

    public bool CanMoveTo(Vector2Int coords)
    {
        return AvailableMoves.Contains(coords);
    }

    public virtual void MovePiece(Vector2Int coords, Action competedCallback = null)
    {
        Vector3 targetPosition = board.CalculatePositionFromCoords(coords);
        
        occupiedSquare = coords;
        hasMoved = true;
        
        
        _tweener.MoveTo(transform, targetPosition, competedCallback);
    }


    protected void TryToAddMove(Vector2Int coords)
    {
        AvailableMoves.Add(coords);
    }

    public void SetData(Vector2Int coords, TeamColor team, Board board)
    {
        this.team = team;
        occupiedSquare = coords;
        this.board = board;
        transform.localPosition = board.CalculatePositionFromCoords(coords);
    }

    public bool IsAttackingPieceOfType<T>() where T : Piece
    {
        foreach (var square in AvailableMoves)
        {
            if (board.GetPieceOnSquare(square) is T)
                return true;
        }

        return false;
    }
}