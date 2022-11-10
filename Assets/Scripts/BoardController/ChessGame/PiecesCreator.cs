using System;
using System.Collections.Generic;
using UnityEngine;

public class PiecesCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _piecesPrefabs;
    [SerializeField] private Material _blackMaterial;
    [SerializeField] private Material _whiteMaterial;
    
    private Dictionary<string, GameObject> _nameToPieceDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var piece in _piecesPrefabs)
        {
            _nameToPieceDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
        }
    }

    public GameObject CreatePiece(Type type,Transform parent)
    {
        GameObject prefab = _nameToPieceDict[type.ToString()];
        if (prefab)
        {
            GameObject newPiece = Instantiate(prefab, parent);
            return newPiece;
        }

        return null;
    }

    public Material GetTeamMaterial(TeamColor team)
    {
        return team == TeamColor.White ? _whiteMaterial : _blackMaterial;
    }
}