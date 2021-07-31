using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;
using System.Drawing;

public abstract class Piece : MonoBehaviour
{
    [SerializeField] 
    protected Sprite sprite;

    [SerializeField]
    protected PieceName pieceName;

    [SerializeField]
    protected isColor color;
    public isColor Color { get => color; }
    
    protected bool isFirstMove = true;

    public int MatrixX, MatrixY;

    public abstract List<Coordinates> GetPossibleMoves();

    protected List<Coordinates> BoundaryFilteredAll(List<Coordinates> possibleMoves)
    {
        var illegalMoves = new List<Coordinates>();
        for (int i=0; i<possibleMoves.Count; i++)
        {
            int moveX = possibleMoves[i].X;
            int moveY = possibleMoves[i].Y;
            
            if (!IsInBoundaries(possibleMoves[i]))
            {
                illegalMoves.Add(possibleMoves[i]);
                print(possibleMoves[i].X + " : " + possibleMoves[i].Y);
            }
                
        }
        illegalMoves.ForEach(move => possibleMoves.Remove(move));
        return possibleMoves;
    }

    protected bool IsInBoundaries(Coordinates coordinates)
    {
        return ((coordinates.X < 8 && coordinates.X >= 0 && coordinates.Y < 8 && coordinates.Y >= 0));
    }

    protected List<Coordinates> CheckHorizontalAndVertical()
    {
        int moveX, moveY;
        var possibleMoves = new List<Coordinates>();
        for (int x = -7; x <= 7; x++)
        {
            moveX = MatrixX + x;
            moveY = MatrixY + x;

            var horizontalMove = new Coordinates(moveX, MatrixY);
            var verticalMove = new Coordinates(MatrixX, moveY);
            //check horizontal
            if (IsInBoundaries(horizontalMove))
            {
                possibleMoves.Add(horizontalMove);
            }
            //check vertical
            if (IsInBoundaries(verticalMove))
            {
                possibleMoves.Add(verticalMove);
            }
        }
        return possibleMoves;
    }

    protected List<Coordinates> CheckDiagonal()
    {
        int y = -7;
        var possibleMoves = new List<Coordinates>();
        for (int x = -7; x <= 7; x++)
        {
            //first diagonal
            var diagonalMovement = new Coordinates(MatrixX + x, MatrixY + y);
            if (IsInBoundaries(diagonalMovement))
            {
                possibleMoves.Add(diagonalMovement);
            }
            //second diagonal
            diagonalMovement = new Coordinates(MatrixX + x, MatrixY - y);
            if (IsInBoundaries(diagonalMovement))
            {
                possibleMoves.Add(diagonalMovement);
            }
            y++;
        }
        return possibleMoves;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
