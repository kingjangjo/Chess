using UnityEngine;

public class Pawn : MonoBehaviour,IPiece
{
    public GameObject pawn;
    public GameObject Move;
    public GameObject Take;

    public int curFile = (int)File.E;
    public int curRank = 2;
    public int beforeFile = 0;
    public int beforeRank = 0;
    
    bool isFirst = true;
    bool isTurnOn = false;
    bool on = false;
    public bool white = true;
    private int color = 1;

    private void Awake()
    {
        pawn = GetComponent<GameObject>();
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
    private void OnMouseDown()
    {
        if (!on)
        {
            if (isFirst)
            {
                for (int i = 1; i <= 2; i++)
                {
                    if (BoardManager.Instance.IsBlocked(X, Y + i * color) == Condition.Empty)
                    {
                        Vector3 expectationMovement = new Vector3(gameObject.transform.position.x, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                        //var movePosition = Instantiate(Move, expectationMovement, Quaternion.identity);
                        var movePosition = PoolManager.instance.GetObject("Move");
                        movePosition.transform.position = expectationMovement;
                        movePosition.transform.SetParent(gameObject.transform);
                        movePosition.GetComponent<Move>().curFile = this.curFile;
                        movePosition.GetComponent<Move>().curRank = this.curRank + i * color;
                        movePosition.GetComponent<Move>().rePos();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                if (BoardManager.Instance.IsBlocked(X, Y + color) == Condition.Empty)
                {
                    Vector3 expectationMovement = new Vector3(gameObject.transform.position.x, -0.75f, gameObject.transform.position.z  +(-1.5f * color));
                    //var movePosition = Instantiate(Move, expectationMovement, Quaternion.identity);
                    var movePosition = PoolManager.instance.GetObject("Move");
                    movePosition.transform.SetParent(gameObject.transform);
                    movePosition.GetComponent<Move>().curFile = this.curFile;
                    movePosition.GetComponent<Move>().curRank = this.curRank + color;
                    movePosition.GetComponent<Move>().rePos();
                }
            }
            on = true;
        }
        else
        {
            PoolManager.instance.returnAll();
            on = false;
        }
    }
}