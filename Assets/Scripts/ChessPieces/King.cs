using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Coordinates> GetPossibleMoves()
    {
        var possibleMoves = new List<Coordinates>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                var movement = new Coordinates(MatrixX + x, MatrixY + y);
                if (IsInBoundaries(movement))
                {
                    possibleMoves.Add(movement);
                }
            }
        }
        return possibleMoves;
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
