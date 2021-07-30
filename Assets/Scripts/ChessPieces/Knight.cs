using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Coordinates> GetPossibleMoves()
    {
        var possibleMoves = new List<Coordinates>();

        possibleMoves.Add(new Coordinates(MatrixX - 1, MatrixY - 2));
        possibleMoves.Add(new Coordinates(MatrixX + 1, MatrixY - 2));
        possibleMoves.Add(new Coordinates(MatrixX + 2, MatrixY - 1));
        possibleMoves.Add(new Coordinates(MatrixX + 2, MatrixY + 1));
        possibleMoves.Add(new Coordinates(MatrixX + 1, MatrixY + 2));
        possibleMoves.Add(new Coordinates(MatrixX - 1, MatrixY + 2));
        possibleMoves.Add(new Coordinates(MatrixX - 2, MatrixY + 1));
        possibleMoves.Add(new Coordinates(MatrixX - 2, MatrixY - 1));

        return BoundaryFilteredAll(possibleMoves);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
