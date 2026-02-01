using NUnit.Framework;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System.Collections.Generic;

public class Piece : MonoBehaviour,IPiece//대충 피스 베이스
{
    private GameObject piece;
    public int curFile = (int)File.E;
    public int curRank = 2;
    private int beforeFile = 0;
    private int beforeRank = 0;

    internal bool on = false;
    public bool white = true;
    internal int color = 1;
    internal bool isFirst = true;
    public Vector2Int pos;
    internal List<Vector2Int> raws = new List<Vector2Int>();
    private void Awake()
    {
        piece = GetComponent<GameObject>();
        if (white)
        {
            color = 1;
        }
        else
        {
            color = -1;
        }
        pos = new Vector2Int(curFile, curRank);
    }
    private void Start()
    {
        //calculationRawMove();
    }
    public Vector2Int Pos
    {
        get { return pos; }
        set
        {
            pos = value;
        }

    }
    public int X
    {
        get { return curFile; }
        set
        {
            beforeFile = curFile;
            curFile = value;
        }
    }
    public int Y
    {
        get { return curRank; }
        set
        {
            beforeRank = curRank;
            curRank = value;
            Debug.Log($"{(File)beforeFile}{beforeRank} to {(File)curFile}{curRank}");
            BoardManager.Instance.MovePos(beforeFile, beforeRank, X, Y);
            BoardManager.Instance.MovePos(this, new Vector2Int(X,Y));
            isFirst = false;
        }
    }
    void OnMouseDown()
    {
        if (!on && !TurnManager.instance.isSelectPiece)
        {
            if (TurnManager.instance.currentState.ToString() == "WhiteTurnState" && !white)
                return;
            if (TurnManager.instance.currentState.ToString() == "BlackTurnState" && white)
                return;
            TurnManager.instance.isSelectPiece = true;
            DrawMoveMent();
        }
        else
        {
            PoolManager.instance.returnAll();
            on = false;
            TurnManager.instance.isSelectPiece = false;
        }
    }
    protected virtual void DrawMoveMent()
    {

    }
    public virtual void CalculationRawMove()
    {

    }
    protected bool CanCreateTakeMove(int x, int y)
    {
        Debug.Log($"{x} {y}");
        if (x <= 0 || x >= 9 || y <= 0 || y >= 9)
        {
            Debug.Log("Out of bounds");
            return false;

        }
        if (BoardManager.Instance.IsBlocked(x, y) == Condition.Empty)
        {
            Debug.Log("Empty");
            return true;

        }
        if (BoardManager.Instance.IsBlocked(x, y) == Condition.Piece && Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == x && t.curRank == y).color != color)
        {
            Debug.Log("Can take");
            return true;
        }
        else
        {
            Debug.Log("Cannot move or take");
            return false;
        }
    }
}