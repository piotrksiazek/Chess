using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PieceFactory : MonoBehaviour
{
    private const int _z = -1;
    public static GameObject Create(GameObject go, int x, int y)
    {
        return Instantiate(go, new Vector3(x, y, _z), Quaternion.identity);
    }
}
