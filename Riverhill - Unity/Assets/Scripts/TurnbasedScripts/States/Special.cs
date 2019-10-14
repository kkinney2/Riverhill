using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : IState
{
    private BattleStateMachine battleStateMachine;
    private object gameObject;

    public Special(BattleStateMachine battleStateMachine, object gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    public void Enter()
    {
        Debug.Log("Entering Special");
    }

    public void Execute()
    {
        Debug.Log("Executing Special");
    }

    public void Exit()
    {
        Debug.Log("Exiting Special");
    }
}
