using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Coordinates> GetPossibleMoves()
    {
        return BoundaryFilteredAll(CheckHorizontalAndVertical());
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
