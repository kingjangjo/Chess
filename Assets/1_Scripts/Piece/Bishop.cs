using System.Linq;
using UnityEngine;

public class Bishop : Piece
{
    protected override void DrawMoveMent()
    {
        for (int i = 1; ; i++)
        {
            if (BoardManager.Instance.IsBlocked(X + i * color, Y + i * color) == Condition.Empty)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile + i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
            }
            else if (BoardManager.Instance.IsBlocked(X + i * color, Y + i * color) == Condition.Piece && Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == X + i * color && t.curRank == Y + i * color).color != color)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile + i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
                break;
            }
            else
            {
                break;
            }
        }
        for (int i = -1; ; i--)
        {
            if (BoardManager.Instance.IsBlocked(X + i * color, Y + i * color) == Condition.Empty)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile + i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
            }
            else if (BoardManager.Instance.IsBlocked(X + i * color, Y + i * color) == Condition.Piece && Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == X + i * color && t.curRank == Y + i * color).color != color)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x - (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile + i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
                break;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; ; i++)
        {
            if (BoardManager.Instance.IsBlocked(X - i * color, Y + i * color) == Condition.Empty)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x + (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile - i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
            }
            else if (BoardManager.Instance.IsBlocked(X - i * color, Y + i * color) == Condition.Piece && Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == X - i * color && t.curRank == Y + i * color).color != color)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x + (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile - i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
                break;
            }
            else
            {
                break;
            }
        }
        for (int i = -1; ; i--)
        {
            if (BoardManager.Instance.IsBlocked(X - i * color, Y + i * color) == Condition.Empty)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x + (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile - i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
            }
            else if (BoardManager.Instance.IsBlocked(X - i * color, Y + i * color) == Condition.Piece && Object.FindObjectsByType<Piece>(FindObjectsSortMode.None).FirstOrDefault(t => t.curFile == X - i * color && t.curRank == Y + i * color).color != color)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x + (i * 1.5f) * color, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Take_Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Take_Move>().curFile = this.curFile - i * color;
                movePosition.GetComponent<Take_Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Take_Move>().RePos();
                break;
            }
            else
            {
                break;
            }
        }
    }
    public override void CalculationRawMove()
    {
        raws.Clear();
        for (int i = 1; ; i++)
        {
            var (condition, piece) = BoardManager.Instance.IsBlocked(new Vector2Int(X + i * color, Y + i * color));

            if (condition == Condition.Empty)
                raws.Add(new Vector2Int(X + i * color, Y + i * color));
            else if (condition == Condition.Piece && piece.color != color)
            {
                raws.Add(new Vector2Int(X + i * color, Y + i * color));
                Debug.Log("Enemy Piece Detected");
                break;
            }
            else
                break;

        }
        for (int i = 1; ; i++)
        {
            var (condition, piece) = BoardManager.Instance.IsBlocked(new Vector2Int(X - i * color, Y + i * color));

            if (condition == Condition.Empty)
                raws.Add(new Vector2Int(X - i * color, Y + i * color));
            else if (condition == Condition.Piece && piece.color != color)
            {
                raws.Add(new Vector2Int(X - i * color, Y + i * color));
                break;
            }
            else
                break;
        }
        for (int i = -1; ; i--)
        {
            var (condition, piece) = BoardManager.Instance.IsBlocked(new Vector2Int(X + i * color, Y + i * color));

            if (condition == Condition.Empty)
                raws.Add(new Vector2Int(X + i * color, Y + i * color));
            else if (condition == Condition.Piece && piece.color != color)
            {
                raws.Add(new Vector2Int(X + i * color, Y + i * color));
                break;
            }
            else 
                break;
        }
        for (int i = -1; ; i--)
        {
            var (condition, piece) = BoardManager.Instance.IsBlocked(new Vector2Int(X - i * color, Y + i * color));

            if (condition == Condition.Empty)
                raws.Add(new Vector2Int(X - i * color, Y + i * color));
            else if (condition == Condition.Piece && piece.color != color)
            {
                raws.Add(new Vector2Int(X - i * color, Y + i * color));
                break;
            }
            else 
                break;
        }
    }
}
