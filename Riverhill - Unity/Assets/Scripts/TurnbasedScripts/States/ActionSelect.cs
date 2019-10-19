using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    public ActionSelect(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;
    }

    /*
    private BattleStateMachine battleStateMachine = new BattleStateMachine();
    BattleStateMachine owner;
    BattleManager battleManager;

    //private GameObject gameObject;
    private GameObject character;
    public ActionSelect(BattleStateMachine newOwner, GameObject a_Character)
    {
       this.owner = newOwner;
       this.character = a_Character;
    }

    public ActorController acScript;
    */

    public void Enter()
    {
        Debug.Log("Entering ActionSelect state");
        battleManager = BattleManager.Instance;
        this.characterStateMachine.UpdateState();
        /*        
        acScript = character.GetComponent<ActorController>();
        */
    }

    public void Execute()
    {
        Debug.Log("Executing ActionSelect state");

        // TODO: Individual Character buttons? That way they only have to reference "themselves"?
        if (battleManager.moveSelected == true && characterState.actionCount < 2)
        {
            Debug.Log("MoveSelected, to Move state");
            //go to Move state
            characterStateMachine.ChangeState(characterState.state_Move);
            //battleManager.moveSelected = false; maybe do this in Enter() of Move state?
            //battleManager.actionCount++; maybe do this in Enter() of Move state?
        }

        if (battleManager.attackSelected == true && characterState.actionCount < 2)
        {
            Debug.Log("AttackSelected, to Attack state");
            //go to Attack state
            characterStateMachine.ChangeState(characterState.state_Attack);
            //battleManager.attackSelected = false; maybe do this in Enter() of Attack state?
            //battleManager.actionCount++; maybe do this in Enter() of Attack state?
        }

        // TODO: Implement Special
        if (battleManager.specialSelected == true && characterState.actionCount < 2)
        {
            Debug.Log("SpecialSelected, to Special state");
            //go to Special state
            //this.characterStateMachine.ChangeState(new Special(characterStateMachine, this.gameObject));
            //battleManager.specialSelected = false; maybe do this in Enter() of Special state?
            //battleManager.actionCount++; maybe do this in Enter() of Special state?
        }

        /* HAVE THIS CHECK IN CHARACTERSTATE INSTEAD? YES
        if (battleManager.actionCount >= 2)
        {
            battleManager.turnCount++;
            this.battleStateMachine.UpdateState();
        }
        */

        /*
        Debug.Log("Execute ActionSelect");

        if (battleManager.moveSelected == true && battleManager.actionCount < 2) //stops at actionCount of 2 (allows for 2 option picks per turn)
        {
            Debug.Log("Move selected");
            this.battleStateMachine.ChangeState(new Move(battleStateMachine, this.character));

        */
        /* MOVING TO MOVE SCRIPT
        //do move here... (add in functionality later)

        //if (acScript.enabled == false)
        //{
        //    acScript.enabled = true;
        //}

        //tried enable/disable of entire ActorController script, no luck

        acScript.Move();

        //owner.ChangeState(new IState());

        battleManager.actionCount++;
        Debug.Log("Action count:" + battleManager.actionCount); //success
        battleManager.moveSelected = false; //returns to false correctly
        */
        /*
    }

    if (battleManager.attackSelected == true && battleManager.actionCount < 2) //same as above rule
    {
        Debug.Log("Attack selected");
        this.battleStateMachine.ChangeState(new Attack(battleStateMachine, this.character));

        */
        /* MOVING TO ATTACK SCRIPT
        //do attack here... (add in functionality later)
        battleManager.actionCount++;
        Debug.Log("Action count:" + battleManager.actionCount); //success
        battleManager.attackSelected = false; //returns to false correctly
        */
        /*
    }

    if (battleManager.specialSelected == true && battleManager.actionCount < 2) //same as above rule
    {
        Debug.Log("Special selected");
        //this.battleStateMachine.ChangeState(new Special(battleStateMachine, this.character));
        //Crashes when special is selected twice...

        */
        /* MOVING TO SPECIAL SCRIPT
        //do special option here... (add in functionality later)
        battleManager.actionCount++;
        Debug.Log("Action count:" + battleManager.actionCount); //success
        battleManager.specialSelected = false; //returns to false correctly
        */
        /*
    }

    if(battleManager.actionCount >= 2 || battleManager.endTurnSelected == true)
    {
        Exit();
    }
    */
    }

    public void Exit()
    {
        Debug.Log("Exiting ActionSelect state");
        /*
        Debug.Log("Exiting ActionSelect");
        this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
        */
    }
}
