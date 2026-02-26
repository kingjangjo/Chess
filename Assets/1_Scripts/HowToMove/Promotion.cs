using UnityEngine;

public class Promotion : Move_Base
{
    private void OnMouseDown()
    {
        IPiece piece = piecePosition.GetComponent<IPiece>();
        piece.X = curFile;
        piece.Y = curRank;
        piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
        PromotionManager.instance.Promote(this.gameObject.transform.parent.gameObject);
        PoolManager.instance.returnAll();
    }
}
