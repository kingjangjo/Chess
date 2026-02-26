using UnityEngine;

public class Castling : Move_Base
{
    public override void AfterRePos()
    {
        if (!BoardManager.Instance.IsKingInCheck(this.gameObject.transform.parent.GetComponent<Piece>().white))
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        IPiece piece = piecePosition.GetComponent<IPiece>();
        King king = this.gameObject.transform.parent.GetComponent<King>();
        if (king.X == this.curFile)
        {
            for(int i = 1; i < 9; i++)
            {
                if(BoardManager.Instance.pieceBoard[king.X, i] is Rook)
                {
                    BoardManager.Instance.pieceBoard[king.X, i].gameObject.transform.position = new Vector3(6.75f - (king.X * 1.5f), 0, 6.75f - ((king.Y + 1) * 1.5f));
                    BoardManager.Instance.pieceBoard[king.X, i].X = king.X;
                    BoardManager.Instance.pieceBoard[king.X, i].Y = king.Y + 1;
                    piece.X = curFile;
                    piece.Y = curRank;
                    ChangeTurn();
                    PoolManager.instance.returnAll();
                    piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
                }
            }
        }
        else if (king.Y == this.curRank)
        {
            if (king.X > this.curFile)
            {
                for(int i = king.X; i > 0; i--)
                {
                    if (BoardManager.Instance.pieceBoard[i, king.Y] is Rook)
                    {
                        BoardManager.Instance.pieceBoard[i, king.Y].gameObject.transform.position = new Vector3(6.75f - ((king.X - 1) * 1.5f), 0, 6.75f - (king.Y * 1.5f));
                        BoardManager.Instance.pieceBoard[i, king.Y].X = king.X - 1;
                        BoardManager.Instance.pieceBoard[i, king.Y].Y = king.Y;
                        piece.X = curFile;
                        piece.Y = curRank;
                        ChangeTurn();
                        PoolManager.instance.returnAll();
                        piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
                    }
                }
            }
            else
            {
                for (int i = king.X; i < 9; i++)
                {
                    if (BoardManager.Instance.pieceBoard[i, king.Y] is Rook)
                    {
                        BoardManager.Instance.pieceBoard[i, king.Y].gameObject.transform.position = new Vector3(6.75f - ((king.X + 1) * 1.5f), 0, 6.75f - (king.Y * 1.5f));
                        BoardManager.Instance.pieceBoard[i, king.Y].X = king.X + 1;
                        BoardManager.Instance.pieceBoard[i, king.Y].Y = king.Y;
                        piece.X = curFile;
                        piece.Y = curRank;
                        ChangeTurn();
                        PoolManager.instance.returnAll();
                        Debug.Log(BoardManager.Instance.pieceBoard[king.X + 1, king.Y]);
                        
                        piecePosition.position = new Vector3(this.gameObject.transform.position.x, 0, this.transform.position.z);
                    }
                }
            }
        }
    }
}
