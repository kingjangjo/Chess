using System.Linq;
using UnityEngine;

public class Take_Move : Move_Base
{
    private void OnMouseDown()
    {
        IPiece piece = piecePosition.GetComponent<IPiece>();
        switch(BoardManager.Instance.IsBlocked(curFile, curRank))
        {
            case Condition.Piece:
                Piece target = Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == this.curFile && t.curRank == this.curRank);
                BoardManager.Instance.CatchPiece(target.gameObject);
                piece.X = curFile;
                piece.Y = curRank;
                ChangeTurn();
                PoolManager.instance.returnAll();
                break;
            case Condition.Empty:
                piece.X = curFile;
                piece.Y = curRank;
                ChangeTurn();
                PoolManager.instance.returnAll();
                break;
            case Condition.Out:
                break;
            default:
                break;
        }
        piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
    }
}