using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PieceFactory : MonoBehaviour
{
    private const float _upperLeftCornerX = -3.5f;
    private const float _upperRightCornerY = -_upperLeftCornerX;

    private const int _z = -1;
    public static GameObject Create(GameObject go, int x, int y)
    {
        Vector3 translatedUnits = TranslateMatrixUnitsToWorldUnits(x, y);
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
    public static Vector3 TranslateMatrixUnitsToWorldUnits(int matrixX, int matrixY)
    {
        float worldX = _upperLeftCornerX + matrixX;
        float worldY = _upperRightCornerY - matrixY;
        return new Vector3(worldX, worldY, _z);
    }
}
