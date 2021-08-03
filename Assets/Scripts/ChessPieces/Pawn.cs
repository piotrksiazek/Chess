using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

public class Pawn : Piece
{
    private GameObject[,] pieceMatrix = new GameObject[8, 8];
    private void Start()
    {
        pieceMatrix = FindObjectOfType<GameController>().PieceMatrix;
    }
    public override List<Coordinates> GetPossibleMoves()
    {
        var possibleMoves = new List<Coordinates>();
        int moveFactor = 1;
        if (color == isColor.White)
            moveFactor = -1;

        var possibleEnemies = new List<Coordinates>();
        possibleEnemies.Add(new Coordinates(MatrixX + 1, (MatrixY) + 1 * moveFactor));
        possibleEnemies.Add(new Coordinates(MatrixX - 1, (MatrixY) + 1 * moveFactor));

        foreach(var cord in possibleEnemies)
        {
            if(IsInBoundaries(cord))
            {
                GameObject possibleEnemyGo = pieceMatrix[cord.X, cord.Y];
                if (possibleEnemyGo)
                {
                    Piece possibleEnemy = possibleEnemyGo.GetComponent<Piece>();
                    if (possibleEnemy.Color != color)
                    {
                        possibleMoves.Add(cord);
                    }
                }
            }
            
        }
        if (isFirstMove)
        {
            possibleMoves.Add(new Coordinates(MatrixX, (MatrixY) + 1 * moveFactor));
            possibleMoves.Add( new Coordinates(MatrixX, (MatrixY) + 2 * moveFactor));
        }
        else
        {
            var coordinates = new Coordinates(MatrixX, (MatrixY) + 1 * moveFactor);
            if(IsInBoundaries(coordinates))
            {
                GameObject possibleEnemyGo = pieceMatrix[coordinates.X, coordinates.Y];
                if(!possibleEnemyGo)
                {
                    possibleMoves.Add(coordinates);
                }
            }
        }
        return BoundaryFilteredAll(possibleMoves);
    }
}
