using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    private BattleStateMachine battleStateMachine;
    private GameObject gameObject;

    BattleManager battleManager;

    public Move(BattleStateMachine battleStateMachine, GameObject gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    /*
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    private GameObject character;
    public Move(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }

    public ActorController acScript;
    */

    public void Enter()
    {
        Debug.Log("Entering move state"); //success!
        battleManager = BattleManager.Instance;
        battleManager.moveSelected = false; //reset moveSelected //success!
        battleManager.actionCount++; //inc. actionCount, by one to allow for multi-move selections per turn //success!
        this.battleStateMachine.UpdateState();
    }

    public void Execute()
    {
        Debug.Log("Executing move state");
        //TO DO: do moving function here!

        /*
        Debug.Log("Executing Move");
        acScript.Move();
        
        battleManager.actionCount++;
        Debug.Log("Action count:" + battleManager.actionCount); //success
        battleManager.moveSelected = false; //returns to false correctly
        */

        this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject));
    }

    public void Exit()
    {
        Debug.Log("Exiting move state");
        /*
        Debug.Log("Exiting Move");
        */
    }
}
