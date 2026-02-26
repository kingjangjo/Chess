using UnityEngine;

public class BlackTurnState : IState
{
    public void Enter()
    {
        Debug.Log("BlcakTurnStart!");
    }
    public void Update()
    {

    }
    public void Exit()
    {
        string whiteTurnResult = BoardManager.Instance.IsCheckmate(true);
        if (whiteTurnResult == "Checkmate")
            TurnManager.instance.GameEnd("Black Win!");
        else if (whiteTurnResult == "Stalemate")
            TurnManager.instance.GameEnd("Draw!");
        else if (whiteTurnResult == "NotCheckmate")
            Debug.Log("BlackTurnEnd!");
    }
}
