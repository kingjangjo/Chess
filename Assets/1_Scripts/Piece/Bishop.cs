using UnityEngine;

public class Bishop : Piece
{
    protected override void DrawMoveMent()
    {
        for (int i = -8; ; i++)
        {
            if (BoardManager.Instance.IsBlocked(X + i, Y + i * color) == Condition.Empty)
            {
                Vector3 expectationMovement = new Vector3(gameObject.transform.position.x, -0.75f, gameObject.transform.position.z - (i * 1.5f) * color);
                var movePosition = PoolManager.instance.GetObject("Move");
                movePosition.transform.position = expectationMovement;
                movePosition.transform.SetParent(gameObject.transform);
                movePosition.GetComponent<Move>().curFile = this.curFile;
                movePosition.GetComponent<Move>().curRank = this.curRank + i * color;
                movePosition.GetComponent<Move>().RePos();
            }
            if (BoardManager.Instance.IsBlocked(X - i, Y + i * color) == Condition.Empty)
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
}
