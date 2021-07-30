using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

public class Pawn : Piece
{
    public override List<Coordinates> GetPossibleMoves()
    {
        var possibleMoves = new List<Coordinates>();
        int moveFactor = 1;
        if (color == isColor.White)
            moveFactor = -1;

        if(isFirstMove)
        {
            possibleMoves.Add( new Coordinates(MatrixX, (MatrixY) + 1 * moveFactor));
        }
        else
        {
            possibleMoves.Add(new Coordinates(MatrixX, (MatrixY) + 2 * moveFactor));
        }
        return BoundaryFiltered(possibleMoves);
    }
}
