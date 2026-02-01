using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform piecePosition;

    public int curFile = 0;
    public int curRank = 0;
    private void Start()
    {
        gameObject.transform.parent.GetComponent<Piece>().raws.Add(new Vector2Int(curFile, curRank));
    }
    public void RePos()
    {
        piecePosition = transform.parent;
    }
    private void OnMouseDown()
    {
        piecePosition.position = new Vector3(this.gameObject.transform.position.x,0,this.transform.position.z);
        IPiece piece = piecePosition.GetComponent<IPiece>();
        piece.X = curFile;
        piece.Y = curRank;
        ChangeTurn();
        PoolManager.instance.returnAll();
    }
    void ChangeTurn()
    {
        if (TurnManager.instance.currentState.ToString() == "WhiteTurnState")
            TurnManager.instance.ChangeState(new BlackTurnState());
        else
            TurnManager.instance.ChangeState(new WhiteTurnState());
    }
}
