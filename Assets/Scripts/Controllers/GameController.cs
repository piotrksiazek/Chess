using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Chess;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allPieces;

    [SerializeField]
    private GameObject selectedPiece;

    private PieceFactory pieceFactory;

    private GameObject[,] pieceMatrix = new GameObject[8,8];
    private GameObject[,] squareMatrix = new GameObject[8,8];


    [SerializeField]
    private isColor playerColor = isColor.White;

    [SerializeField]
    private List<Coordinates> possibleMoves;

    private void Awake()
    {
        pieceFactory = FindObjectOfType<PieceFactory>();
        allPieces = new List<GameObject>();
        possibleMoves = new List<Coordinates>();
        Square.SelectedPieceDelegate += AddSelectedPiece;
    }

    private void Start()
    {
        allPieces = pieceFactory.PopulatePieces();
        squareMatrix = pieceFactory.PopulateSquares();
        foreach (var pieceGo in allPieces)
        {
            Piece piece = pieceGo.GetComponent<Piece>();
            pieceMatrix[piece.MatrixX, piece.MatrixY] = pieceGo;
        }
    }

    private void Update()
    {
        HighlightPossibleMoves();
        PrintMoves();
    }

    private void AddSelectedPiece(int matrixX, int matrixY)
    {
        GameObject pieceGo = pieceMatrix[matrixX, matrixY];
        Piece piece = null;
        if (pieceGo != null)
        {
            piece = pieceGo.transform.GetComponent<Piece>();
        }

        //if (piece != null && piece.Color == playerColor)
        if (piece != null)
        {
            selectedPiece = pieceMatrix[matrixX, matrixY];
            possibleMoves = piece.GetPossibleMoves();
        }
    }

    private void HighlightPossibleMoves()
    {
        foreach(var cord in possibleMoves)
        {
            squareMatrix[cord.X, cord.Y].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    //delete
    private void PrintMoves()
    {
        foreach(var cord in possibleMoves)
        {
            
        }
    }
}
