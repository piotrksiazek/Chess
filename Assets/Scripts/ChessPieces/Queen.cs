using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Coordinates> GetPossibleMoves()
    {
        var possibleMoves = CheckHorizontalAndVertical();
        possibleMoves.AddRange(CheckDiagonal());
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
