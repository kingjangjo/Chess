using System.Drawing;
using UnityEngine;

public class Pawn : Piece
{
    protected override void DrawMoveMent()
    {
        if (isFirst)
        {
            for (int i = 1; i <= 2; i++)
            {
                if (BoardManager.Instance.IsBlocked(X, Y + i * color) == Condition.Empty)
                {
                    Vector3 expectationMovement = new Vector3(gameObject.transform.position.x, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                    var movePosition = PoolManager.instance.GetObject("Move");
                    movePosition.transform.position = expectationMovement;
                    movePosition.transform.SetParent(gameObject.transform);
                    movePosition.GetComponent<Move>().curFile = this.curFile;
                    movePosition.GetComponent<Move>().curRank = this.curRank + i * color;
                    movePosition.GetComponent<Move>().RePos();
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            if (BoardManager.Instance.IsBlocked(X, Y + color) == Condition.Empty)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x, -0.75f, gameObject.transform.position.z + (-1.5f * color));
                var movePosition = PoolManager.instance.GetObject("Move");
                movePosition.transform.position = expectationMovement;//이 코드 주석 달아보쉴?
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Move>().curFile = this.curFile;
                movePosition.GetComponent<Move>().curRank = this.curRank + color;
                movePosition.GetComponent<Move>().RePos();
            }
        }
        on = true;
    }
    public override void CalculationRawMove()
    {
        raws.Clear();
        if (isFirst)
        {
            for (int i = 1; i <= 2; i++)
            {
                if (BoardManager.Instance.IsBlocked(X, Y + i * color) == Condition.Empty)
                {
                    raws.Add(new Vector2Int(X, Y + i * color));
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            if (BoardManager.Instance.IsBlocked(X, Y + color) == Condition.Empty)
            {
                raws.Add(new Vector2Int(X, Y + color));
            }
        }
    }
}
