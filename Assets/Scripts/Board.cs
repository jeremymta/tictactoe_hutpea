using System;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject cellPrefab;

    public Transform board;
    public GridLayoutGroup gridLayout;

    public int boardSize;
    public string currentTurn = "x";
    public string[,] matrix;
    public System.Collections.Generic.List<GameObject> lst_cell;

    public void Start()
    {
        matrix = new string[boardSize + 1, boardSize + 1];
        gridLayout.constraintCount = boardSize;
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int i = 1; i <= boardSize; i++)
        {
            for(int j = 1; j <= boardSize; j++)
            {
                GameObject cellTransform = Instantiate(cellPrefab, board);
                Cell cell = cellTransform.GetComponent<Cell>();
                cell.row = i;
                cell.column = j;
                matrix[i, j] = "";
                lst_cell.Add(cellTransform);
            }
        }
    }

    //Mo rong ba co neu nguoi choi danh vao ria
    public void ExpandBoardIfNecessary(int row, int column)
    {
        if (row == 1 || row == boardSize || column == 1 || column == boardSize)
        {
            boardSize++;
            string[,] newMatrix = new string[boardSize + 1, boardSize + 1];

            //Sao chep du lieu cu vao ma tran moi
            for (int i = 1; i <= boardSize - 1; i++)
            {
                for (int j = 1; j <= boardSize - 1; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            matrix = newMatrix;
            gridLayout.constraintCount = boardSize;
            for(int i = 0; i < lst_cell.Count; i++)
            {
                Destroy(lst_cell[i]);
                //lst_cell.RemoveAt(i);
            }
            //CreateNewCells(); // Them o moi
            ReRenderBoard();
        }
    }

    public void ReRenderBoard()
    {
        for (int i = 1; i <= boardSize; i++)
        {
            for (int j = 1; j <= boardSize; j++)
            {
                if (matrix[i, j] == null)
                {
                    matrix[i, j] = "";
                }
                GameObject cellTransform = Instantiate(cellPrefab, board);
                Cell cell = cellTransform.GetComponent<Cell>();
                cell.row = i;
                cell.column = j;
                if (matrix[i, j] != "")
                {
                    Debug.Log("ReRenderBoard " + matrix[i, j] + " " + i + " " + j);
                }
                cell.ChangeImage(matrix[i, j]);
                lst_cell.Add(cellTransform);
            }
        }
    }

    //Tao them cac o moi khi mo rong ban co
    public void CreateNewCells()
    {
        for (int i = 1; i <= boardSize; i++)
        {
            for (int j = 1; j <= boardSize; j++)
            {
                if (matrix[i, j] == null)
                {
                    GameObject cellTransform = Instantiate(cellPrefab, board);
                    Cell cell = cellTransform.GetComponent<Cell>();
                    cell.row = i;
                    cell.column = j;
                    matrix[i, j] = ""; // Khoi tao gtri la chuoi rong
                }
            }
        }
    }

    public bool CanPlay(int row, int column)
    {
        //Ktra neu o trong, thi tra ve true (nguoi choi co the danh)
        Debug.Log("CanPlay " + matrix[row, column]);
        return matrix[row, column] == "";
    }

    public bool Check(int row, int column)
    {
        matrix[row, column] = currentTurn;
        bool result = false;

        //Check ham doc
        int count = 0; 
        for ( int i = row - 1; i >= 1; i--) //len tren
        {
            if (matrix[i, column] == currentTurn)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        for (int i = row + 1; i <= boardSize; i++) //xuong duoi
        {
            if (matrix[i, column] == currentTurn)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        if (count + 1 >= 5)
        {
            result = true;
        }

        //Check hang ngang
        count = 0;
        for (int i = column - 1; i >= 1; i--) //sang trai
        {
            if (matrix[row, i] == currentTurn)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        for (int i = column + 1; i <= boardSize; i++) //sang phai
        {
            if (matrix[row, i] == currentTurn)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        if (count + 1 >= 5)
        {
            result = true;
        }

        //Check hang cheo 1
        count = 0;
        for (int i = column - 1; i >= 1; i--) //cheo tren 1
        {
            if (matrix[row - (column - i), i] == currentTurn) //hang lui bao nhieu, cot lui bay nhieu
            {
                count++;
            }
            else
            {
                break;
            }
        }
        for (int i = column + 1; i <= boardSize; i++) //cheo duoi 1
        {
            if (matrix[row + (i - column), i] == currentTurn)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        if (count + 1 >= 5)
        {
            result = true;
        }

        //Hang cheo 2
        count = 0;
        for (int i = column + 1; i <= boardSize; i++) //cheo tren 2
        {
            if (matrix[row - (i - column), i] == currentTurn) //hang lui bao nhieu, cot lui bay nhieu
            {
                count++;
            }
            else
            {
                break;
            }
        }
        for (int i = column - 1; i >= 1; i--) //cheo duoi 2
        {
            if (matrix[row + (column - i), i] == currentTurn)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        if (count + 1 >= 5)
        {
            result = true;
        }

        return result;
    }
}
