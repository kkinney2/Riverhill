using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    private BattleStateMachine battleStateMachine;
    private object gameObject;

    public Attack(BattleStateMachine battleStateMachine, object gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    public void Enter()
    {
        Debug.Log("Entering Attack");
    }

    public void Execute()
    {
        Debug.Log("Executing Attack");
    }

    public void Exit()
    {
        Debug.Log("Exiting Attack");
    }
}
