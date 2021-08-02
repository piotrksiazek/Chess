﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Chess;

public class GameController : MonoBehaviour
{

    private List<Coordinates> directions = new List<Coordinates>() {    new Coordinates(-1, 0),
                                                                        new Coordinates(1,0),
                                                                        new Coordinates(0,-1),
                                                                        new Coordinates(0,1),
                                                                        new Coordinates(1, -1),
                                                                        new Coordinates(-1,-1),
                                                                        new Coordinates(1,1),
                                                                        new Coordinates(-1,1)};

    [SerializeField]
    private List<GameObject> allPieces;

    [SerializeField]
    private GameObject selectedPiece;

    [SerializeField]
    private GameObject surroundingSquare;

    private PieceFactory pieceFactory;

    private GameObject[,] pieceMatrix = new GameObject[8, 8];
    private GameObject[,] squareMatrix = new GameObject[8, 8];


    [SerializeField]
    private isColor playerColor = isColor.White;

    [SerializeField]
    private List<Coordinates> possibleMoves;
    private List<Coordinates> allPossibleMoves;

    [SerializeField]
    private bool isCheck = false;

    private void Awake()
    {
        pieceFactory = FindObjectOfType<PieceFactory>();
        allPieces = new List<GameObject>();
        possibleMoves = new List<Coordinates>();
        allPossibleMoves = new List<Coordinates>();
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
        //SetPossibleMovesToColor(Color.white);
        if (pieceMatrix[matrixX, matrixY] != null)
            if (pieceMatrix[matrixX, matrixY].GetComponent<Piece>().Color != playerColor)
            {
                MovePieceTo(matrixX, matrixY);
                IsCheck();
            }
            else
            {
                AddSelectedPiece(matrixX, matrixY);
            }

        else
        {
            MovePieceTo(matrixX, matrixY);
            IsCheck();
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
        {
            selectedPiece = pieceMatrix[matrixX, matrixY];
            possibleMoves = piece.GetPossibleMoves();
            FilterObstacles(selectedPiece, possibleMoves);

            //possibleMoves.ForEach(move => print(selectedPiece.GetComponent<Piece>().name + "  X:" + move.X + "  Y:" + move.Y)); //debug
        }
    }

    private void HighlightPossibleMoves()
    {
        foreach (var cord in possibleMoves)
        {
            squareMatrix[cord.X, cord.Y].GetComponent<SpriteRenderer>().color = Color.red;
        }

        foreach (var cord in allPossibleMoves)
        {
            squareMatrix[cord.X, cord.Y].GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void SetPossibleMovesToColor(List<Coordinates> moves, Color color)
    {
        foreach (var cord in moves)
        {
            squareMatrix[cord.X, cord.Y].GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void MovePieceTo(int x, int y)
    {
        if (possibleMoves.Contains(new Coordinates(x, y)))
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
            ChangePlayerColor();
            possibleMoves.Clear();
            piece.HasMovedForTheFirstTime();

            GetAllPossibleMoves(); // maybe not here
        }
    }

    private void ChangePlayerColor()
    {
        if (playerColor == isColor.White)
            playerColor = isColor.Black;
        else if (playerColor == isColor.Black)
            playerColor = isColor.White;
    }

    private void GetAllPossibleMoves()
    {
        allPossibleMoves.Clear();
        foreach (GameObject pieceGo in allPieces)
        {
            if (pieceGo)
            {
                List<Coordinates> currentPossibleMoves;
                currentPossibleMoves = pieceGo.GetComponent<Piece>().GetPossibleMoves();
                FilterObstacles(pieceGo, currentPossibleMoves);
                allPossibleMoves.AddRange(currentPossibleMoves);
                //print(pieceGo.GetComponent<Piece>().name + ": " + currentPossibleMoves.Count); //degub
            }
        }
        

    }

    private void FilterObstacles(GameObject pieceToFilter, List<Coordinates> movesToFilter)
    {
        if (pieceToFilter != null)
        {
            Piece piece = pieceToFilter.GetComponent<Piece>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var currentSquarePosition = new Coordinates(piece.MatrixX + j * directions[i].X, piece.MatrixY + j * directions[i].Y);
                    if (Piece.IsInBoundaries(currentSquarePosition) && (currentSquarePosition.X != piece.MatrixX || (currentSquarePosition.Y != piece.MatrixY)))
                    {
                        
                        if (pieceMatrix[currentSquarePosition.X, currentSquarePosition.Y])
                        {
                            //Piece consideredPiece = pieceMatrix[currentSquarePosition.X, currentSquarePosition.Y].GetComponent<Piece>();
                            //if (consideredPiece.Color == piece.Color)
                            //    movesToFilter.Remove(currentSquarePosition);
                            for (int k = j + 1; k < 8; k++)
                            {
                                currentSquarePosition.X = piece.MatrixX + k * directions[i].X;
                                currentSquarePosition.Y = piece.MatrixY + k * directions[i].Y;
                                if (Piece.IsInBoundaries(currentSquarePosition) && (currentSquarePosition.X != piece.MatrixX || (currentSquarePosition.Y != piece.MatrixY)))
                                {
                                    movesToFilter.Remove(currentSquarePosition);
                                }
                            }
                        }
                    }
                }
            }
        }
        DeleteMovesIfIsTheSameColor(pieceToFilter, movesToFilter);
    }

    private void DeleteMovesIfIsTheSameColor(GameObject pieceToFilterGo, List<Coordinates> movesToFilter)
    {
        List<Coordinates> illegalMoves = new List<Coordinates>();
        Piece pieceToFilter = pieceToFilterGo.GetComponent<Piece>();
        foreach (var move in movesToFilter)
        {
            GameObject consideredPieceGo = pieceMatrix[move.X, move.Y];
            if(consideredPieceGo)
            {
                Piece consideredPiece = consideredPieceGo.GetComponent<Piece>();
                if(consideredPiece.Color == pieceToFilter.Color)
                {
                    illegalMoves.Add(new Coordinates(consideredPiece.MatrixX, consideredPiece.MatrixY));
                }
            }
        }

        illegalMoves.ForEach(illegalMove => movesToFilter.Remove(illegalMove));
    }

    private bool IsCheck()
    {
        SpriteRenderer surroundingSquareSr = surroundingSquare.GetComponent<SpriteRenderer>();
        foreach(var move in allPossibleMoves)
        {
            GameObject consideredPieceGo = pieceMatrix[move.X, move.Y];
            if (consideredPieceGo)
            {
                Piece consideredPiece = consideredPieceGo.GetComponent<Piece>();
                if (consideredPiece.PieceName == PieceName.King)
                {
                    print("check");
                    surroundingSquareSr.color = Color.red;
                    return true;
                }
            }
        }
        surroundingSquareSr.color = Color.white;
        return false;
    }
}
