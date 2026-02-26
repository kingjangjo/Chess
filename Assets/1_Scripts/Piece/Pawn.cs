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
        //raws.Clear();
        rawss.Clear();
        if (isFirst)
        {
            for (int i = 1; i <= 2; i++)
            {
                if (BoardManager.Instance.IsBlocked(X, Y + i * color) == Condition.Empty)
                {
                    rawss.Add(new Vector2Int(X, Y + i * color), "Move");
                    //raws.Add(new Vector2Int(X, Y + i * color));
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
                if (white)
                {
                    if (Y == 7)
                    {
                        rawss.Add(new Vector2Int(X, Y + color), "Promotion");
                    }
                    else
                    {
                        rawss.Add(new Vector2Int(X, Y + color), "Move");
                        //raws.Add(new Vector2Int(X, Y + color));
                    }
                }
                else
                {
                    if (Y == 2)
                    {
                        rawss.Add(new Vector2Int(X, Y + color), "Promotion");
                    }
                    else
                    {
                        rawss.Add(new Vector2Int(X, Y + color), "Move");
                        //raws.Add(new Vector2Int(X, Y + color));
                    }
                }
            }
        }
        if (CanCreateTakeMove(X + color, Y + color) && BoardManager.Instance.IsBlocked(X + color, Y + color) == Condition.Piece)
        {
            if (white)
            {
                if(Y == 7)
                {
                    //raws.Add(new Vector2Int(X + 2, Y + 1));
                    rawss.Add(new Vector2Int(X + color, Y + color), "TakePromotion");
                }
                else
                {
                    //raws.Add(new Vector2Int(X + 2, Y + 1));
                    rawss.Add(new Vector2Int(X + color, Y + color), "Take");
                }
            }
            else
            {
                if (Y == 2)
                {
                    //raws.Add(new Vector2Int(X + 2, Y + 1));
                    rawss.Add(new Vector2Int(X + color, Y + color), "TakePromotion");
                }
                else
                {
                    //raws.Add(new Vector2Int(X + 2, Y + 1));
                    rawss.Add(new Vector2Int(X + color, Y + color), "Take");
                }
            }
        }
        if (CanCreateTakeMove(X - color, Y + color) && BoardManager.Instance.IsBlocked(X - color, Y + color) == Condition.Piece)
        {
            if (white)
            {
                if (Y == 7)
                {
                    //raws.Add(new Vector2Int(X + 2, Y - 1));
                    rawss.Add(new Vector2Int(X - color, Y + color), "TakePromotion");
                }
                else
                {
                    //raws.Add(new Vector2Int(X + 2, Y - 1));
                    rawss.Add(new Vector2Int(X - color, Y + color), "Take");
                }
            }
            else
            {
                if (Y == 2)
                {
                    //raws.Add(new Vector2Int(X + 2, Y - 1));
                    rawss.Add(new Vector2Int(X - color, Y + color), "TakePromotion");
                }
                else
                {
                    //raws.Add(new Vector2Int(X + 2, Y - 1));
                    rawss.Add(new Vector2Int(X - color, Y + color), "Take");
                }
            }
        }
    }
}
