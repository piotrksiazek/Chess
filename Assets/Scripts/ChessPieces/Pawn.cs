using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public Pawn(Sprite sprite, string pieceName, string color) : base(sprite, pieceName, color)
    {
        this.sprite = sprite;
        this.pieceName = pieceName;
        this.color = color;
    }
}
