using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

public abstract class Piece : MonoBehaviour
{
    [SerializeField] 
    protected Sprite sprite;

    [SerializeField]
    protected PieceName pieceName;

    [SerializeField]
    protected isColor color;
    
    protected bool isFirstMove = false;

    protected void SayHello()
    {
        print("XDDD");
    }
}
