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
        Debug.LogWarning("BlackTurnExit!");
        string whiteTurnResult = BoardManager.Instance.IsCheckmate(true);
        if (whiteTurnResult == "Checkmate" && !ChessClient.Instance.gameEnded)
            ChessClient.Instance.End(ChessClient.Instance.roomId,"BLACK");
        //TurnManager.instance.GameEnd("Black Win!");
        else if (whiteTurnResult == "Stalemate" && !ChessClient.Instance.gameEnded)
            ChessClient.Instance.End(ChessClient.Instance.roomId, "DRAW");
        //TurnManager.instance.GameEnd("Draw!");
        else if (whiteTurnResult == "NotCheckmate")
            Debug.Log("BlackTurnEnd!");
    }
}
