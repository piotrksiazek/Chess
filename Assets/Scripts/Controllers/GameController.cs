using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allPieces;

    [SerializeField]
    private GameObject selectedPiece;

    private PieceFactory pieceFactory;

    [SerializeField]
    private GameObject[,] boardMatrix = new GameObject[8,8];

    private void Awake()
    {
        pieceFactory = FindObjectOfType<PieceFactory>();
        allPieces = new List<GameObject>();
        Square.SelectedPieceDelegate += addSelectedPiece;
    }

    private void Start()
    {
        allPieces = pieceFactory.PopulateChessBoard();
        foreach (var pieceGo in allPieces)
        {
            Piece piece = pieceGo.GetComponent<Piece>();
            boardMatrix[piece.MatrixX, piece.MatrixY] = pieceGo;
        }
    }

    private void addSelectedPiece(int matrixX, int matrixY)
    {
        selectedPiece = boardMatrix[matrixY, matrixX];
    }
}
