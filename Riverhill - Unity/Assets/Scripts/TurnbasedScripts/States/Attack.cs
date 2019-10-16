using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    private BattleStateMachine battleStateMachine;
    private GameObject gameObject;

    BattleManager battleManager;

    public Attack(BattleStateMachine battleStateMachine, GameObject gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    /*
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
    */

    public void Enter()
    {
        Debug.Log("Entering attack state"); //success!
        battleManager = BattleManager.Instance;
        battleManager.attackSelected = false; //reset attackSelected //success!
        battleManager.actionCount = (battleManager.actionCount + 2); //inc. actionCount, by two to avoid multi-attack selections per turn //success!
        this.battleStateMachine.UpdateState();
    }

    public void Execute()
    {
        Debug.Log("Executing attack state");
        //TO DO: do attacking function here!

        /*
        Debug.Log("Executing Attack");
        //perform attack here, add functionality later on

        //increase actionCount, maybe by 2 to prevent acting again after attacking?
        battleManager.actionCount = (battleManager.actionCount) + 2;
        Exit();
        */

        this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject));
    }

    public void Exit()
    {
        Debug.Log("Exiting attack state");
        /*
        Debug.Log("Exiting Attack");
        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
        */
    }
}
