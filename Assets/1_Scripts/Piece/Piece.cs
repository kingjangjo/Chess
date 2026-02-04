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
    [SerializeField]
    private int beforeFile = 0;
    [SerializeField]
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
            //DrawMoveMent();
            DrawLegalMove();
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
    private void DrawLegalMove()
    {
        foreach (var move in GetLagalMoves())
        {
            Vector3 expectationMovement = new Vector3(6.75f - (move.x * 1.5f), -0.75f, 6.75f - (move.y * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Move>().curFile = move.x;
            movePosition.GetComponent<Move>().curRank = move.y;
            movePosition.GetComponent<Move>().RePos();
        }
    }
    public virtual void CalculationRawMove()
    {

    }
    protected bool CanCreateTakeMove(int x, int y)
    {
        if (x <= 0 || x >= 9 || y <= 0 || y >= 9)
        {
            return false;

        }
        if (BoardManager.Instance.IsBlocked(x, y) == Condition.Empty)
        {
            return true;
        }
        if (BoardManager.Instance.IsBlocked(x, y) == Condition.Out)
        {
            return false;
        }
        if (BoardManager.Instance.IsBlocked(x, y) == Condition.Piece)
        {
            foreach(Piece piece in BoardManager.Instance.pieceBoard)
            {
                if(piece != null)
                {
                    if (piece.color != color)
                    {
                        if (piece.curFile == x && piece.curRank == y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    List<Vector2Int> GetLagalMoves()
    {
        var legal = new List<Vector2Int>();
        CalculationRawMove();
        foreach (var move in raws)
        {
            BoardManager.Instance.MovePos(this, move);
            if(!BoardManager.Instance.IsKingInCheck(white))
                legal.Add(move);

            BoardManager.Instance.MovePos(this, new Vector2Int(curFile, curRank));
        }

        return legal;
    }
}