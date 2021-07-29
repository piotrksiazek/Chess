using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Pieces prefabs
    [SerializeField]
    private GameObject b_pawn, w_pawn, b_rook, w_rook, b_queen, w_queen, b_knight, w_knight, b_king, w_king, b_bishop, w_bishop;


    void Start()
    {
        PieceFactory.Create(b_pawn, 3, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
