using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform piecePosition;

    public int curFile = 0;
    public int curRank = 0;
    public void rePos()
    {
        piecePosition = transform.parent;
    }
    private void OnMouseDown()
    {
        Debug.Log("Click");
        piecePosition.position = new Vector3(this.gameObject.transform.position.x,0,this.transform.position.z);
        IPiece piece = piecePosition.GetComponent<IPiece>();
        piece.X = curFile;
        piece.Y = curRank;
        PoolManager.instance.returnAll();
    }
}
