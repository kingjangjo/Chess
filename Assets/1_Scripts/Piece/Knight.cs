using System.Linq;
using UnityEngine;

public class Knight : Piece
{
    protected override void DrawMoveMent()
    {
        if (CanCreateTakeMove(X + 2, Y + 1))
        {
            Debug.Log("Knight Move Created");
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (2 * 1.5f), -0.75f, gameObject.transform.position.z - (1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile + 2;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank + 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X + 2, Y - 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (2 * 1.5f), -0.75f, gameObject.transform.position.z - (-1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile + 2;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank - 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X + 1, Y - 2))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (1 * 1.5f), -0.75f, gameObject.transform.position.z - (-2 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile + 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank - 2;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X - 1, Y - 2))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (-1 * 1.5f), -0.75f, gameObject.transform.position.z - (-2 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile - 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank - 2;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X - 2, Y - 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (-2 * 1.5f), -0.75f, gameObject.transform.position.z - (-1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile - 2;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank - 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X - 2, Y + 1))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (-2 * 1.5f), -0.75f, gameObject.transform.position.z - (1 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile - 2;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank + 1;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X - 1, Y + 2))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (-1 * 1.5f), -0.75f, gameObject.transform.position.z - (2 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile - 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank + 2;
            movePosition.GetComponent<Take_Move>().RePos();
        }
        if (CanCreateTakeMove(X + 1, Y + 2))
        {
            Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (1 * 1.5f), -0.75f, gameObject.transform.position.z - (2 * 1.5f));
            var movePosition = PoolManager.instance.GetObject("Take_Move");
            movePosition.transform.position = expectationMovement;
            movePosition.transform.SetParent(gameObject.transform);
            movePosition.GetComponent<Take_Move>().curFile = this.curFile + 1;
            movePosition.GetComponent<Take_Move>().curRank = this.curRank + 2;
            movePosition.GetComponent<Take_Move>().RePos();
        }
    }
    public override void CalculationRawMove()
    {
        rawss.Clear();
        //raws.Clear();
        if (CanCreateTakeMove(X + 2, Y + 1))
        {
            //raws.Add(new Vector2Int(X + 2, Y + 1));
            rawss.Add(new Vector2Int(X + 2, Y + 1), "Take_Move");
        }
        if (CanCreateTakeMove(X + 2, Y - 1))
        {
            //raws.Add(new Vector2Int(X + 2, Y - 1));
            rawss.Add(new Vector2Int(X + 2, Y - 1), "Take_Move");
        }
        if (CanCreateTakeMove(X + 1, Y - 2))
        {
            rawss.Add(new Vector2Int(X + 1, Y - 2),"Take_Move");
            //raws.Add(new Vector2Int(X + 1, Y - 2));
        }
        if (CanCreateTakeMove(X - 1, Y - 2))
        {
            rawss.Add(new Vector2Int(X - 1, Y - 2), "Take_Move");
            //raws.Add(new Vector2Int(X - 1, Y - 2));
        }
        if (CanCreateTakeMove(X - 2, Y - 1))
        {
            rawss.Add(new Vector2Int(X - 2, Y - 1), "Take_Move");
            //raws.Add(new Vector2Int(X - 2, Y - 1));
        }
        if (CanCreateTakeMove(X - 2, Y + 1))
        {
            rawss.Add(new Vector2Int(X - 2, Y + 1), "Take_Move");
            //raws.Add(new Vector2Int(X - 2, Y + 1));
        }
        if (CanCreateTakeMove(X - 1, Y + 2))
        {
            rawss.Add(new Vector2Int(X - 1, Y + 2), "Take_Move");
            //raws.Add(new Vector2Int(X - 1, Y + 2));
        }
        if (CanCreateTakeMove(X + 1, Y + 2))
        {
            rawss.Add(new Vector2Int(X + 1, Y + 2), "Take_Move");
            //raws.Add(new Vector2Int(X + 1, Y + 2));
        }
    }
}
