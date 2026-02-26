using UnityEngine;

public class Move_Base : MonoBehaviour
{
    internal Transform piecePosition;

    public int curFile = 0;
    public int curRank = 0;
    private void Start()
    {
        //gameObject.transform.parent.GetComponent<Piece>().raws.Add(new Vector2Int(curFile, curRank));
    }
    public void RePos()
    {
        piecePosition = transform.parent;
        AfterRePos();
    }
    public virtual void AfterRePos()
    {

    }
    internal void ChangeTurn()
    {
        if (TurnManager.instance.currentState.ToString() == "WhiteTurnState")
            TurnManager.instance.ChangeState(new BlackTurnState());
        else
            TurnManager.instance.ChangeState(new WhiteTurnState());
    }
}
