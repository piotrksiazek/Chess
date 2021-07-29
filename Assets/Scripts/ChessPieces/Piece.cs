using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    [SerializeField] 
    protected Sprite sprite;

    protected string pieceName;

    protected string color;
    
    protected bool isFirstMove = false;

    public Piece(Sprite sprite, string pieceName, string color)
    {
        this.sprite = sprite;
        this.pieceName = pieceName;
        this.color = color;
    }

    protected void SayHello()
    {
        print("XDDD");
    }
}
