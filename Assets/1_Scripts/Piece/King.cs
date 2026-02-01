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
        raws.Clear();
        if (CanCreateTakeMove(X, Y + 1))
        {
            raws.Add(new Vector2Int(X, Y + 1));
        }
        if (CanCreateTakeMove(X + 1, Y + 1))
        {
            raws.Add(new Vector2Int(X + 1, Y + 1));
        }
        if (CanCreateTakeMove(X + 1, Y))
        {
            raws.Add(new Vector2Int(X + 1, Y));
        }
        if (CanCreateTakeMove(X + 1, Y - 1))
        {
            raws.Add(new Vector2Int(X + 1, Y - 1));
        }
        if (CanCreateTakeMove(X, Y - 1))
        {
            raws.Add(new Vector2Int(X, Y - 1));
        }
        if (CanCreateTakeMove(X - 1, Y - 1))
        {
            raws.Add(new Vector2Int(X - 1, Y - 1));
        }
        if (CanCreateTakeMove(X - 1, Y))
        {
            raws.Add(new Vector2Int(X - 1, Y));
        }
        if (CanCreateTakeMove(X - 1, Y + 1))
        {
            raws.Add(new Vector2Int(X - 1, Y + 1));
        }
    }
}
