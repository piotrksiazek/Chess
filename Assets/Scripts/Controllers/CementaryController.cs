using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

public class CementaryController : MonoBehaviour
{
    [SerializeField]
    private GameObject squarePrefab;

    [SerializeField]
    private int rowsX, rowsY;

    [SerializeField]
    private isColor color;
    public Color Color {get ;}

    private GameObject[,] cementaryMatrix;
    private List<GameObject> cementaryList;

    [SerializeField]
    private int howFarFromChessboardX, howFarFromChessboardY;

    //squares counting from 0,0 to get to the edge, each side
    private int panRight = 8; 
    private int panLeft = -3;

    private int closestFreeGraveIndex = 0;

    private void Start()
    {
        cementaryMatrix = new GameObject[rowsX, rowsY];
        PopulateCementary();
    }

    private void PopulateCementary()
    {
        int moveFactorX = panRight + howFarFromChessboardX;
        if (color == isColor.Black)
            moveFactorX = panLeft - howFarFromChessboardX;

        int index = 0;
        for (int y = 0; y < rowsY; y++)
        {
            for (int x = 0; x < rowsX; x++)
            {
                GameObject squareGo = Instantiate(squarePrefab, PieceFactory.TranslateMatrixUnitsToWorldUnits(x + moveFactorX, y + howFarFromChessboardY), Quaternion.identity);
                Square square = squareGo.GetComponent<Square>();
                square.MatrixX = x;
                square.MatrixY = y;

                cementaryMatrix[x, y] = squareGo;
                cementaryList[index++] = squareGo;

                squareGo.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    private int FindNextFreeGraveIndex()
    {
        if(closestFreeGraveIndex < cementaryList.Count)
        {
            return closestFreeGraveIndex + 1;
        }
        return 0;
    }
}
