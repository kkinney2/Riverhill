using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    GameSettings gameSettings;

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
        Debug.Log("actionCount: " + characterState.actionCount);
        battleManager = BattleManager.Instance;
        this.characterStateMachine.UpdateState();
        /*        
        acScript = character.GetComponent<ActorController>();
        */
    }

    public void Execute()
    {
        //Debug.Log("Executing ActionSelect state");

        if (characterState.moveSelected == true && characterState.actionCount <= gameSettings.MaxActionCount) // (&& actionCount <= 2)
        {
            Debug.Log("MoveSelected, to Move state");
            characterStateMachine.ChangeState(characterState.state_Move);
        }

        if (characterState.attackSelected == true && characterState.actionCount <= gameSettings.MaxActionCount) // (&& actionCount <= 2)
        {
            Debug.Log("AttackSelected, to Attack state");
            //go to Attack state
            characterStateMachine.ChangeState(characterState.state_Attack);
        }

        // TODO: Implement Special
        if (characterState.specialSelected == true && characterState.actionCount <= gameSettings.MaxActionCount) // (&& actionCount <= 2)
        {
            Debug.Log("SpecialSelected, to Special state");
            //go to Special state
            //this.characterStateMachine.ChangeState(new Special(characterStateMachine, this.gameObject));
        }

        if (characterState.endTurnSelected == true)
        {
            Debug.Log("EndTurn Selected");
            //End Turn
            characterStateMachine.ChangeState(characterState.state_Idle);
            battleManager.nextCharacter = true;
        }

    }

    private void ResetSelected()
    {
        characterState.moveSelected = false;
        characterState.attackSelected = false;
        characterState.specialSelected = false;
        characterState.endTurnSelected = false;
    }

    public void Exit()
    {
        Debug.Log("Exiting ActionSelect state");
        ResetSelected();
    }
}
