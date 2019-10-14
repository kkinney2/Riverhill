using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : IState
{
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    //private object gameObject;
    private GameObject character;
    public Special(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
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
