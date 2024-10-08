﻿using System;
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

    ////Mo rong ban co neu nguoi choi danh vao ria
    //public void ExpandBoardIfNecessary(int row, int column)
    //{
    //    if (row == 1 || row == boardSize || column == 1 || column == boardSize)
    //    {
    //        boardSize++;
    //        string[,] newMatrix = new string[boardSize + 1, boardSize + 1];

    //        //Sao chep du lieu cu vao ma tran moi
    //        for (int i = 1; i <= boardSize - 1; i++)
    //        {
    //            for (int j = 1; j <= boardSize - 1; j++)
    //            {
    //                newMatrix[i, j] = matrix[i, j];
    //            }
    //        }

    //        matrix = newMatrix;
    //        gridLayout.constraintCount = boardSize;
    //        for (int i = 0; i < lst_cell.Count; i++)
    //        {
    //            Destroy(lst_cell[i]);
    //            //lst_cell.RemoveAt(i);
    //        }
    //        //CreateNewCells(); // Them o moi
    //        ReRenderBoard();
    //    }
    //}

    public void ExpandBoardIfNecessary(int row, int column)
    {
        bool expanded = false; // Biến để theo dõi xem có mở rộng hay không

        // Kiểm tra nếu người chơi đánh vào rìa bên trên
        if (row == 1)
        {
            boardSize++;
            expanded = true;
            string[,] newMatrix = new string[boardSize + 1, boardSize + 1];

            // Sao chép ma trận cũ và mở rộng thêm hàng trên
            for (int i = 1; i <= boardSize - 1; i++)
            {
                for (int j = 1; j <= boardSize - 1; j++)
                {
                    newMatrix[i + 1, j] = matrix[i, j]; // Dịch xuống 1 hàng để thêm hàng trên
                }
            }

            matrix = newMatrix;
        }

        // Kiểm tra nếu người chơi đánh vào rìa bên dưới
        if (row == boardSize)
        {
            boardSize++;
            expanded = true;
            string[,] newMatrix = new string[boardSize + 1, boardSize + 1];

            // Sao chép ma trận cũ và mở rộng thêm hàng dưới
            for (int i = 1; i <= boardSize - 1; i++)
            {
                for (int j = 1; j <= boardSize - 1; j++)
                {
                    newMatrix[i, j] = matrix[i, j]; // Giữ nguyên vị trí các hàng hiện tại
                }
            }

            matrix = newMatrix;
        }

        // Kiểm tra nếu người chơi đánh vào rìa bên trái
        if (column == 1)
        {
            boardSize++;
            expanded = true;
            string[,] newMatrix = new string[boardSize + 1, boardSize + 1];

            // Sao chép ma trận cũ và mở rộng thêm cột trái
            for (int i = 1; i <= boardSize - 1; i++)
            {
                for (int j = 1; j <= boardSize - 1; j++)
                {
                    newMatrix[i, j + 1] = matrix[i, j]; // Dịch phải 1 cột để thêm cột trái
                }
            }

            matrix = newMatrix;
        }

        // Kiểm tra nếu người chơi đánh vào rìa bên phải
        if (column == boardSize)
        {
            boardSize++;
            expanded = true;
            string[,] newMatrix = new string[boardSize + 1, boardSize + 1];

            // Sao chép ma trận cũ và mở rộng thêm cột phải
            for (int i = 1; i <= boardSize - 1; i++)
            {
                for (int j = 1; j <= boardSize - 1; j++)
                {
                    newMatrix[i, j] = matrix[i, j]; // Giữ nguyên vị trí các cột hiện tại
                }
            }

            matrix = newMatrix;
        }

        // Nếu đã mở rộng bàn cờ, cần tái tạo lại giao diện
        if (expanded)
        {
            gridLayout.constraintCount = boardSize;

            // Xóa tất cả các ô cũ
            for (int i = 0; i < lst_cell.Count; i++)
            {
                Destroy(lst_cell[i]); // Xóa từng ô trong danh sách
            }
            lst_cell.Clear(); // Xóa toàn bộ danh sách sau khi đã phá hủy các ô

            // Tạo lại toàn bộ bàn cờ sau khi mở rộng
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

    ////Tao them cac o moi khi mo rong ban co
    //public void CreateNewCells()
    //{
    //    for (int i = 1; i <= boardSize; i++)
    //    {
    //        for (int j = 1; j <= boardSize; j++)
    //        {
    //            if (matrix[i, j] == null)
    //            {
    //                GameObject cellTransform = Instantiate(cellPrefab, board);
    //                Cell cell = cellTransform.GetComponent<Cell>();
    //                cell.row = i;
    //                cell.column = j;
    //                matrix[i, j] = ""; // Khoi tao gtri la chuoi rong
    //            }
    //        }
    //    }
    //}

    public bool CanPlay(int row, int column)
    {
        //Ktra neu o trong, thi tra ve true (nguoi choi co the danh)
        //Debug.Log("CanPlay " + matrix[row, column]);
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
