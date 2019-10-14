using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;
    
    //private object gameObject;
    private GameObject character;
    public Move(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }

    public ActorController acScript;

    public void Enter()
    {
        Debug.Log("Entering Move");
        Execute();
    }

    public void Execute()
    {
        Debug.Log("Executing Move");
        /*
        acScript.Move();

        //owner.ChangeState(new IState());
        
        battleManager.actionCount++;
        Debug.Log("Action count:" + battleManager.actionCount); //success
        battleManager.moveSelected = false; //returns to false correctly
        */
    }

    public void Exit()
    {
        Debug.Log("Exiting Move");
    }
}
