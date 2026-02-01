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
    public void Exit() { Debug.Log("BlcakTurnEnd!"); }
}
