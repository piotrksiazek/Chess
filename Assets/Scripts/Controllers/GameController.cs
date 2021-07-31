﻿using System.Collections;
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
        Square.SelectedPieceDelegate += ClickHandler;
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
    }

    private void ClickHandler(int matrixX, int matrixY)
    {
        SetPossibleMovesToColor(Color.white);
        if (pieceMatrix[matrixX, matrixY] != null)
            if (pieceMatrix[matrixX, matrixY].GetComponent<Piece>().Color != playerColor)
            {
                MovePieceTo(matrixX, matrixY);
            }
            else
            {
                AddSelectedPiece(matrixX, matrixY);
            }
            
        else
        {
            MovePieceTo(matrixX, matrixY);
        }
    }

    private void AddSelectedPiece(int matrixX, int matrixY)
    {
        GameObject pieceGo = pieceMatrix[matrixX, matrixY];
        Piece piece = null;
        if (pieceGo != null)
        {
            piece = pieceGo.transform.GetComponent<Piece>();
        }

        if (piece != null && piece.Color == playerColor)
            //if (piece != null)
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

    private void SetPossibleMovesToColor(Color color)
    {
        foreach (var cord in possibleMoves)
        {
            squareMatrix[cord.X, cord.Y].GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void MovePieceTo(int x, int y)
    {
        if(possibleMoves.Contains(new Coordinates(x, y)))
        {
            GameObject possibleEnemyGo = pieceMatrix[x, y];
            if (possibleEnemyGo != null)
            {
                Piece possibleEnemy = possibleEnemyGo.GetComponent<Piece>();
                if (possibleEnemy.Color != playerColor)
                    possibleEnemy.Die();
            }
            //phisically move
            selectedPiece.transform.position = PieceFactory.TranslateMatrixUnitsToWorldUnits(x, y);

            //set matrix
            Piece piece = selectedPiece.GetComponent<Piece>();
            pieceMatrix[x, y] = selectedPiece;
            pieceMatrix[piece.MatrixX, piece.MatrixY] = null;

            //set new properties of a piece
            piece.MatrixX = x;
            piece.MatrixY = y;

            //after all that ...
            changePlayerColor();
            possibleMoves.Clear();
        }
    }

    void changePlayerColor()
    {
        if (playerColor == isColor.White)
            playerColor = isColor.Black;
        else if (playerColor == isColor.Black)
            playerColor = isColor.White;
    }
}
