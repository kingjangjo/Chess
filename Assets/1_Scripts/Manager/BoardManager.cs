using System;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    
    private bool isChecked;
    Condition[,] board = new Condition[10, 10];
    private void Awake()
    {
        //ΩÃ±€≈Ê»≠
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //∫∏µÂºº∆√
        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                if (i == 0 || i == 9 || j == 0 || j == 9)
                {
                    board[i, j] = Condition.Out;
                }
                else if (j == 1 || j == 2 || j == 7 || j == 8)
                {
                    board[i, j] = Condition.Piece;
                }
                else
                {
                    board[i,j] = Condition.Empty;
                }
            }
        }
        /*o o o o o o o o o o
          o p p p p p p p p o
          o p p p p p p p p o*/
        /*o e e e e e e e e o
          o e e e e e e e e o
          o e e e e e e e e o
          o e e e e e e e e o*/
        /*o p p p p p p p p o
          o p p p p p p p p o
          o o o o o o o o o o*/
    }
    public Condition IsBlocked(int x, int y)
    {
        if (board[x, y] == Condition.Empty) return Condition.Empty;
        else if (board[x, y] == Condition.Piece) return Condition.Piece;
        else return Condition.Out;
    }
    public void MovePos(int curFile, int curRank, int nextFile, int nextRank)
    {
        if (board[curFile,curRank] == Condition.Piece)
        {
            board[curFile,curRank] = Condition.Empty;
            board[nextFile,nextRank] = Condition.Piece;
        }
        else
        {
            Debug.LogWarning("What..?!?!?!");
        }
    }
}