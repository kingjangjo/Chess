using System;
using UnityEngine;

public class WhiteTurnState : IState
{
    public void Enter()
    {
        Debug.Log("WhiteTurnStart!");
    }
    public void Update()
    {

    }
    public void Exit()
    {
        string whiteTurnResult = BoardManager.Instance.IsCheckmate(false);
        if (whiteTurnResult == "Checkmate")
            TurnManager.instance.GameEnd("White Win!");
        else if (whiteTurnResult == "Stalemate")
            TurnManager.instance.GameEnd("Draw!");
        else if (whiteTurnResult == "NotCheckmate")
            Debug.Log("WhiteTurnEnd!");
    }
}
