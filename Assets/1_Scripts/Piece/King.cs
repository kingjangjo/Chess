using UnityEngine;

public class King : Piece
{
    protected override void DrawMoveMent()
    {
        if (CanCreateTakeMove(X, Y + 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (0 * 1.5f), -0.75f, gameObject.transform.position.z - (1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank + 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X + 1, Y + 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (1 * 1.5f), -0.75f, gameObject.transform.position.z - (1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile + 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank + 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X + 1, Y))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (1 * 1.5f), -0.75f, gameObject.transform.position.z - (0 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile + 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X + 1, Y - 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (1 * 1.5f), -0.75f, gameObject.transform.position.z - (-1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile + 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank - 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X, Y - 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (0 * 1.5f), -0.75f, gameObject.transform.position.z - (-1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank - 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X - 1, Y - 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (-1 * 1.5f), -0.75f, gameObject.transform.position.z - (-1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile - 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank - 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X - 1, Y))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (-1 * 1.5f), -0.75f, gameObject.transform.position.z - (0 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile - 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X - 1, Y + 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (-1 * 1.5f), -0.75f, gameObject.transform.position.z - (1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile - 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank + 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
    }
    public override void CalculationRawMove()
    {
        rawss.Clear();
        //raws.Clear();
        if (CanCreateTakeMove(X, Y + 1))
        {
            rawss.Add(new Vector2Int(X, Y + 1), "Take_Move");
            //raws.Add(new Vector2Int(X, Y + 1));
        }
        if (CanCreateTakeMove(X + 1, Y + 1))
        {
            rawss.Add(new Vector2Int(X + 1, Y + 1), "Take_Move");
            //raws.Add(new Vector2Int(X + 1, Y + 1));
        }
        if (CanCreateTakeMove(X + 1, Y))
        {
            rawss.Add(new Vector2Int(X + 1, Y), "Take_Move");
            //raws.Add(new Vector2Int(X + 1, Y));
        }
        if (CanCreateTakeMove(X + 1, Y - 1))
        {
            rawss.Add(new Vector2Int(X + 1, Y - 1), "Take_Move");
            //raws.Add(new Vector2Int(X + 1, Y - 1));
        }
        if (CanCreateTakeMove(X, Y - 1))
        {
            rawss.Add(new Vector2Int(X, Y - 1), "Take_Move");
            //raws.Add(new Vector2Int(X, Y - 1));
        }
        if (CanCreateTakeMove(X - 1, Y - 1))
        {
            rawss.Add(new Vector2Int(X - 1, Y - 1), "Take_Move");
            //raws.Add(new Vector2Int(X - 1, Y - 1));
        }
        if (CanCreateTakeMove(X - 1, Y))
        {
            rawss.Add(new Vector2Int(X - 1, Y), "Take_Move");
            //raws.Add(new Vector2Int(X - 1, Y));
        }
        if (CanCreateTakeMove(X - 1, Y + 1))
        {
            rawss.Add(new Vector2Int(X - 1, Y + 1), "Take_Move");
            //raws.Add(new Vector2Int(X - 1, Y + 1));
        }
        //if (CanCreateTakeMove(X - 2, Y) && IsNotMoved())
        //{
        //    rawss.Add(new Vector2Int(X - 2, Y), "Castling");
        //    //raws.Add(new Vector2Int(X - 1, Y + 1));
        //}
        //if (CanCreateTakeMove(X + 2, Y) && IsNotMoved())
        //{
        //    rawss.Add(new Vector2Int(X + 2, Y), "Castling");
        //    //raws.Add(new Vector2Int(X - 1, Y + 1));
        //}
        if (IsNotMoved())
        {
            foreach (Piece piece in BoardManager.Instance.pieceBoard)
            {
                if (piece is Rook && piece.IsNotMoved() && piece.color == color)//캐슬링 조건
                {
                    if (piece.Y == this.Y)
                    {
                        if (piece.X > this.X)
                        {
                            for (int i = this.X + 1; piece.X > i; i++)
                            {
                                if (i == piece.X - 1 && BoardManager.Instance.IsBlocked(i, Y) == Condition.Empty)
                                {
                                    rawss.Add(new Vector2Int(X + 2, Y), "Castling");
                                }
                                if (BoardManager.Instance.IsBlocked(i, Y) == Condition.Piece)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int i = piece.X + 1; this.X > i; i++)
                            {
                                if (i == this.X - 1 && BoardManager.Instance.IsBlocked(i, Y) == Condition.Empty)
                                {
                                    rawss.Add(new Vector2Int(X - 2, Y), "Castling");
                                }
                                if (BoardManager.Instance.IsBlocked(i, Y) == Condition.Piece)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (piece.X == this.X)
                    {
                        for (int i = this.Y + 1; piece.Y > i; i++)
                        {
                            if (i == piece.Y - 1 && BoardManager.Instance.IsBlocked(X, i) == Condition.Empty)
                            {
                                rawss.Add(new Vector2Int(X, Y + 2), "Castling");
                            }
                            if (BoardManager.Instance.IsBlocked(X, i) == Condition.Piece)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }   
    }
}
