using UnityEngine;

public class Piece : MonoBehaviour,IPiece//대충 피스 베이스
{
    public GameObject piece;
    public GameObject move;
    public GameObject take;

    public int curFile = (int)File.E;
    public int curRank = 2;
    public int beforeFile = 0;
    public int beforeRank = 0;

    internal bool on = false;
    public bool white = true;
    internal int color = 1;
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
            Debug.Log($"{(File)X}{Y} {(File)beforeFile}{beforeFile}");
            curRank = value;
            Debug.Log($"{(File)curFile}{curRank}");
            BoardManager.Instance.MovePos(beforeFile, beforeRank, X, Y);
        }
    }
    void OnMouseDown()
    {
        if (!on && !TurnManager.instance.isSelectPiece)
        {
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
}