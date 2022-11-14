using System;
using System.Collections.Generic;
using UnityEngine;

public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _piecesPrefabs;
    [SerializeField] private Piece[] _piecesPrefabsNew;
    [SerializeField] private Material _blackMaterial;
    [SerializeField] private Material _whiteMaterial;
    
    private Dictionary<string, GameObject> _nameToPieceDict = new Dictionary<string, GameObject>();
    private Dictionary<int, Piece> _piecesByHashCode = new Dictionary<int, Piece>();

    private void Awake()
    {
        foreach (var piece in _piecesPrefabs)
        {
            _nameToPieceDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
        }
        
        foreach (var piece in _piecesPrefabsNew)
        {
            _piecesByHashCode.Add(piece.GetType().GetHashCode(), piece);
        }
    }

    // public GameObject CreatePiece(Type type,Transform parent)
    // {
    //     GameObject prefab = _nameToPieceDict[type.ToString()];
    //     if (prefab)
    //     {
    //         GameObject newPiece = Instantiate(prefab, parent);
    //         return newPiece;
    //     }
    //
    //     return null;
    // }
    
    public Piece CreatePiece(Type type, TeamColor team, /*ModelType */ Transform parent)
    {
        Piece prefab = _piecesByHashCode[type.GetHashCode()];
        if (!prefab)
            return null;
        
        Piece newPiece = Instantiate(prefab, parent);
        newPiece.SetMaterial(GetTeamMaterial(team));
        return newPiece;
    }

    private Material GetTeamMaterial(TeamColor team) =>
        team == TeamColor.White ? _whiteMaterial : _blackMaterial;
}