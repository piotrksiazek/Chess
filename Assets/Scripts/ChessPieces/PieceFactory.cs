using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PieceFactory : MonoBehaviour
{
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
    private GameObject squarePrefab = null;

    private const float _upperLeftCornerX = -3.5f;
    private const float _upperRightCornerY = -_upperLeftCornerX;

    private const int _z = -1;

    public List<GameObject> PopulateChessBoard()
    {
        List<GameObject> pieces = new List<GameObject>();
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                GameObject squareGo = Instantiate(squarePrefab, TranslateMatrixUnitsToWorldUnits(x, y), Quaternion.identity);
                Square square = squareGo.GetComponent<Square>();
                square.MatrixX = x;
                square.MatrixY = y;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            pieces.Add(Create(b_pawn, i, 1));
            pieces.Add(Create(w_pawn, i, 6));
        }

        pieces.Add(Create(b_king, 4, 0));
        pieces.Add(Create(w_king, 4, 7));

        pieces.Add(Create(b_queen, 3, 0));
        pieces.Add(Create(w_queen, 3, 7));

        pieces.Add(Create(b_rook, 0, 0));
        pieces.Add(Create(b_rook, 7, 0));
        pieces.Add(Create(w_rook, 0, 7));
        pieces.Add(Create(w_rook, 7, 7));

        pieces.Add(Create(b_bishop, 2, 0));
        pieces.Add(Create(b_bishop, 5, 0));
        pieces.Add(Create(w_bishop, 2, 7));
        pieces.Add(Create(w_bishop, 5, 7));

        pieces.Add(Create(b_knight, 1, 0));
        pieces.Add(Create(b_knight, 6, 0));
        pieces.Add(Create(w_knight, 1, 7));
        pieces.Add(Create(w_knight, 6, 7));

        return pieces;
    }

    private GameObject Create(GameObject go, int x, int y)
    {
        Vector3 translatedUnits = TranslateMatrixUnitsToWorldUnits(x, y);
        Piece piece = go.GetComponent<Piece>();
        piece.MatrixX = x;
        piece.MatrixY = y;
        return Instantiate(go, translatedUnits, Quaternion.identity);
    }

    /// <summary>
    /// On the game matrix pieces will be represented as follows
    ///[0, 0][1, 0][2, 0][3, 0][4, 0][5, 0][6, 0][7, 0]
    ///[0, 1][1, 1][2, 1][3, 1][4, 1][5, 1][6, 1][7, 1]
    ///[0, 2][1, 2][2, 2][3, 2][4, 2][5, 2][6, 2][7, 2]
    ///[0, 3][1, 3][2, 3][3, 3][4, 3][5, 3][6, 3][7, 3]
    ///[0, 4][1, 4][2, 4][3, 4][4, 4][5, 4][6, 4][7, 4]
    ///[0, 5][1, 5][2, 5][3, 5][4, 5][5, 5][6, 5][7, 5]
    ///[0, 6][1, 6][2, 6][3, 6][4, 6][5, 6][6, 6][7, 6]
    ///[0, 7][1, 7][2, 7][3, 7][4, 7][5, 7][6, 7][7, 7]
    /// </summary>
    /// <param name="matrixX">X coordinate starting from upper left corner</param>
    /// <param name="matrixY">Y coordinate starting from upper left corner</param>
    /// <returns></returns>
    private Vector3 TranslateMatrixUnitsToWorldUnits(int matrixX, int matrixY)
    {
        float worldX = _upperLeftCornerX + matrixX;
        float worldY = _upperRightCornerY - matrixY;
        return new Vector3(worldX, worldY, _z);
    }
}
