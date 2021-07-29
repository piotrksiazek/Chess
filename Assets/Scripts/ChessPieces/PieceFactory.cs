using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PieceFactory : MonoBehaviour
{
    private const int _z = -1;
    public static GameObject Create(GameObject go, int x, int y)
    {
        Vector3 translatedUnits = TranslateMatrixUnitsToWorldUnits(x, y);
        return Instantiate(go, translatedUnits, Quaternion.identity);
    }

    public static Vector3 TranslateMatrixUnitsToWorldUnits(int matrixX, int matrixY)
    {
        float worldX = -3.5f + matrixX;
        float worldY = 3.5f - matrixY;
        return new Vector3(worldX, worldY, _z);
    }
}
