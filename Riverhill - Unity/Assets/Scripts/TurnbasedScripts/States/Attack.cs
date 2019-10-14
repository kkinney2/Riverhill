using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    //private object gameObject;
    private GameObject character;
    public Attack(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
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
