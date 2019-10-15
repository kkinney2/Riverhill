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
        Debug.Log("Entering Attack"); //success
        //battleManager = BattleManager.Instance; //causes Unity to crash?
        Execute(); //success
    }

    public void Execute()
    {
        Debug.Log("Executing Attack");
        //perform attack here, add functionality later on

        //increase actionCount, maybe by 2 to prevent acting again after attacking?
        battleManager.actionCount = (battleManager.actionCount) + 2;
        Exit();
    }

    public void Exit()
    {
        Debug.Log("Exiting Attack");
        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
    }
}
