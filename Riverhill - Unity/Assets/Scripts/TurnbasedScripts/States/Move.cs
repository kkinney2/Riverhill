using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    private BattleStateMachine battleStateMachine;
    private object gameObject;

    public Move(BattleStateMachine battleStateMachine, object gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    public void Enter()
    {
        Debug.Log("Entering Move");
    }

    public void Execute()
    {
        Debug.Log("Executing Move");
    }

    public void Exit()
    {
        Debug.Log("Exiting Move");
    }
}
