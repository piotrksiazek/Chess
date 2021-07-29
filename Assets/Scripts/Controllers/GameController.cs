using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Pieces prefabs
    [SerializeField]
    private GameObject b_pawn, w_pawn, b_rook, w_rook, b_queen, w_queen, b_knight, w_knight, b_king, w_king, b_bishop, w_bishop;

    [SerializeField]
    private List<GameObject> allPieces; 


    void Start()
    {
        PieceFactory.Create(b_pawn, 0, 0);
        PieceFactory.Create(b_pawn, 1, 0);
        PieceFactory.Create(b_pawn, 1, 1);
    }

    //Adds all piece game objects to scene
    //void PopulatePieces()
    //{
    //    for(int i=0; i<32; i++)
    //    {
    //        allPieces.Add(PieceFactory.Create(b_pawn, i, i));
    //    }
    //}
}
