using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Square : MonoBehaviour
{
    public int MatrixX, MatrixY;
    private GameController gameController;
    public static event Action<int, int> SelectedPieceDelegate;
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnMouseDown()
    {
        SelectedPieceDelegate?.Invoke(MatrixX, MatrixY);
    }
}
