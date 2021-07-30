using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allPieces;

    [SerializeField]
    private GameObject selectedPiece;

    private PieceFactory pieceFactory;

    private void Awake()
    {
       pieceFactory = FindObjectOfType<PieceFactory>();
    }

    private void Start()
    {
        pieceFactory.PopulateChessBoard();
    }
}
