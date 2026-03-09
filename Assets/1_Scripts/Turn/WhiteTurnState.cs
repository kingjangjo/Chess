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
        Debug.LogWarning("WhiteTurnExit!");
        string whiteTurnResult = BoardManager.Instance.IsCheckmate(false);
        if (whiteTurnResult == "Checkmate" && !ChessClient.Instance.gameEnded)
            ChessClient.Instance.End(ChessClient.Instance.roomId, "WHITE");
        else if (whiteTurnResult == "Stalemate" && !ChessClient.Instance.gameEnded)
            ChessClient.Instance.End(ChessClient.Instance.roomId, "DRAW");
        else if (whiteTurnResult == "NotCheckmate")
            Debug.Log("WhiteTurnEnd!");
    }
}
