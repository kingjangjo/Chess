using System.Linq;
using UnityEngine;

public class Take : Move_Base
{
    public override void AfterRePos()
    {
        if (BoardManager.Instance.IsBlocked(curFile, curRank) == Condition.Piece)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        IPiece piece = piecePosition.GetComponent<IPiece>();
        Piece target = Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == this.curFile && t.curRank == this.curRank);
        BoardManager.Instance.CatchPiece(target.gameObject);
        piece.X = curFile;
        piece.Y = curRank;
        ChangeTurn();
        PoolManager.instance.returnAll();
        piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
    }
}