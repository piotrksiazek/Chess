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

    protected List<Coordinates> BoundaryFiltered(List<Coordinates> possibleMoves)
    {
        var illegalMoves = new List<Coordinates>();
        for (int i=0; i<possibleMoves.Count; i++)
        {
            int moveX = possibleMoves[i].X;
            int moveY = possibleMoves[i].Y;
            
            if (!(moveX < 8 && moveX >= 0 && moveY < 8 && moveY >= 0))
            {
                illegalMoves.Add(possibleMoves[i]);
                print(possibleMoves[i].X + " : " + possibleMoves[i].Y);
            }
                
        }
        illegalMoves.ForEach(move => possibleMoves.Remove(move));
        return possibleMoves;
    }

}
