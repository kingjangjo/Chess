using UnityEngine;

public class Move : Move_Base
{
    private void OnMouseDown()
    {
        IPiece piece = piecePosition.GetComponent<IPiece>();
        piece.X = curFile;
        piece.Y = curRank;
        ChangeTurn();
        PoolManager.instance.returnAll();
        piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
    }
}
