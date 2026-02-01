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
    public void Exit() { Debug.Log("WhiteTurnEnd!"); }
}
