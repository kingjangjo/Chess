using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameObject pawn;
    public GameObject Move;
    public GameObject Take;

    private int curFile = (int)File.E;
    private int curRank = 2;
    
    bool isFirst = true;
    bool isTurnOn = false;

    private void Awake()
    {
        pawn = GetComponent<GameObject>();
    }
    public int X
    {
        get { return curFile; }
        set
        {
            curFile = value;
        }
    }
    public int Y
    {
        get { return curRank; }
        set
        {
            curRank = value;
            pawn.transform.position = new Vector3(curFile,0,curRank);
        }
    }
    private void OnMouseDown()
    {
        if (isFirst)
        {
            for (int i = 1; i <= 2; i++)
            {
                if (BoardManager.Instance.isBlocked(X,Y+i) == Condition.Empty)
                {
                    Vector3 expectationMovement = new Vector3(gameObject.transform.position.x, -0.75f, gameObject.transform.position.z - (i * 1.5f));
                    //var movePosition = Instantiate(Move, expectationMovement, Quaternion.identity);
                    var movePosition = PoolManager.instance.GetObject("Move");
                    movePosition.transform.position = expectationMovement;
                    movePosition.transform.SetParent(gameObject.transform);
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            if (BoardManager.Instance.isBlocked(X, Y + 1) == Condition.Empty)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x, -0.75f, gameObject.transform.position.z - 1.5f);
                //var movePosition = Instantiate(Move, expectationMovement, Quaternion.identity);
                var movePosition = PoolManager.instance.GetObject("Move");
                movePosition.transform.SetParent(gameObject.transform);
            }
        }
    }
}