#nullable enable
using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    private bool isChecked;
    Condition[,] conditionBoard = new Condition[10, 10];
    public Piece[,] pieceBoard = new Piece[10, 10];
    Condition protectCondition;
    Piece protectPiece;
    Vector2Int protectPosition;
    Condition beforeCondition;
    Piece beforePiece;
    Vector2Int beforePosition;
    private void Awake()
    {
        //싱글톤화
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        //보드세팅
        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                if (i == 0 || i == 9 || j == 0 || j == 9)
                {
                    conditionBoard[i, j] = Condition.Out;
                }
                else if (j == 1 || j == 2 || j == 7 || j == 8)
                {
                    conditionBoard[i, j] = Condition.Piece;
                }
                else
                {
                    conditionBoard[i,j] = Condition.Empty;
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
    private void Start()
    {
        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                if (j == 1 || j == 2 || j == 7 || j == 8)
                {
                    if(i != 0 && i != 9)
                    {
                        pieceBoard[i, j] = UnityEngine.Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == i && t.curRank == j);
                    }
                }
                else
                    pieceBoard[i, j] = null;
            }
        }
    }
    public Condition IsBlocked(int x, int y)
    {
        if (conditionBoard[x, y] == Condition.Empty) 
            return Condition.Empty;
        else if (conditionBoard[x, y] == Condition.Piece) 
            return Condition.Piece;
        else 
            return Condition.Out;
    }
    public (Condition condition, Piece? piece) IsBlocked(Vector2Int pos)
    {
        if (pos.x <= 0 || pos.x >= 9 || pos.y <= 0 || pos.y >= 9)
            return (Condition.Out, null);
        if (pieceBoard[pos.x, pos.y] == null)
            return (Condition.Empty, null);
        else
            return (Condition.Piece, pieceBoard[pos.x, pos.y]);
    }
    public void MovePos(Piece piece, Vector2Int to)
    {
        var from = piece.pos;
        protectPosition = to;
        beforePosition = from;
        
        protectPiece = pieceBoard[to.x, to.y];
        beforePiece = pieceBoard[from.x, from.y];
        pieceBoard[to.x, to.y] = piece;
        pieceBoard[from.x, from.y] = null;

        protectCondition = conditionBoard[to.x, to.y];
        beforeCondition = conditionBoard[from.x, from.y];
        conditionBoard[to.x, to.y] = Condition.Piece;
        conditionBoard[from.x, from.y] = Condition.Empty;
        piece.pos = to;
    }
    public void UndoMovePos(Piece piece)
    {
        piece.pos = beforePosition;
        pieceBoard[beforePosition.x, beforePosition.y] = beforePiece;
        conditionBoard[beforePosition.x, beforePosition.y] = beforeCondition;
        pieceBoard[protectPosition.x, protectPosition.y] = protectPiece;
        conditionBoard[protectPosition.x, protectPosition.y] = protectCondition;
    }
    private Vector2Int GetKingPos(bool white)
    {
        if (white)
        {
            foreach (var piece in pieceBoard)
            {
                if (piece != null && piece.white && piece is King)
                {
                    return piece.pos;
                }
            }
            Debug.LogError("White King not found!");
            return new Vector2Int(-1, -1);
        }
        else
        {
            foreach (var piece in pieceBoard)
            {
                if (piece != null && !piece.white && piece is King)
                {
                    return piece.pos;
                }
            }
            Debug.LogError("Black King not found!");
            return new Vector2Int(-1, -1);
        }
    }
    public bool IsKingInCheck(bool white)
    {
        Vector2Int kingPos = GetKingPos(white);
        foreach (var piece in pieceBoard)
        {
            if (piece != null && piece.white != white)
            {
                piece.GetComponent<Piece>().CalculationRawMove();
                var moves = piece.rawss;
                foreach (DictionaryEntry move in moves)
                {
                    if ((Vector2Int)move.Key == kingPos && (string)move.Value != "Move" && (string)move.Value != "Promotion")
                        return true;
                }
            }
        }
        return false;
    }
    public void CatchPiece(GameObject piece)
    {
        Vector2Int position = piece.GetComponent<Piece>().Pos;
        pieceBoard[position.x, position.y] = null;
        conditionBoard[position.x, position.y] = Condition.Empty;
        Destroy(piece);
    }
    public string IsCheckmate(bool white)
    {
        foreach(var piece in pieceBoard)
        {
            if (piece != null && piece.white == white)
            {
                piece.GetComponent<Piece>().CalculationRawMove();
                foreach (DictionaryEntry move in piece.rawss)
                {
                    MovePos(piece, (Vector2Int)move.Key);
                    if (!IsKingInCheck(white))
                    {
                        UndoMovePos(piece);
                        return "NotCheckmate";
                    }
                    UndoMovePos(piece);
                }
            }
        }
        if (IsKingInCheck(white))
            return "Checkmate";
        else
            return "Stalemate";
    }
}