using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Pieces prefabs
    [SerializeField]
    private GameObject b_pawn = null,
                       w_pawn = null,
                       b_rook = null,
                       w_rook = null,
                       b_queen = null,
                       w_queen = null,
                       b_knight = null,
                       w_knight = null,
                       b_king = null,
                       w_king = null,
                       b_bishop = null,
                       w_bishop = null;

    [SerializeField]
    private List<GameObject> allPieces; 


    void Start()
    {
        PopulatePieces();
    }

    //later put that in PieceFactory
    void PopulatePieces()
    {
        for (int i = 0; i < 8; i++)
        {
            PieceFactory.Create(b_pawn, i, 1);
            PieceFactory.Create(w_pawn, i, 6);
        }
    }
}
