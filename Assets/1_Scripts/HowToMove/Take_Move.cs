using UnityEngine;

public class Take_Move : MonoBehaviour
{
    private Transform piecePosition;

    public int curFile = 0;
    public int curRank = 0;
    public void RePos()
    {
        piecePosition = transform.parent;
    }
    private void OnMouseDown()
    {
        Debug.Log("Click");
        piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
        IPiece piece = piecePosition.GetComponent<IPiece>();
        switch(BoardManager.Instance.IsBlocked(curFile, curRank))
        {
            case Condition.Piece:
                break;
            case Condition.Empty:
                piece.X = curFile;
                piece.Y = curRank;
                PoolManager.instance.returnAll();
                break;
            case Condition.Out:
                Debug.Log("OutOfRange");
                break;
            default:
                break;
        }
    }
}