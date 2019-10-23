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
        if (characterState.moveSelected == true /*&& characterState.actionCount < 2*/) // Don't need to check action count bc character state already does this
        {
            Debug.Log("MoveSelected, to Move state");
            //go to Move state
            characterStateMachine.ChangeState(characterState.state_Move);
            //battleManager.moveSelected = false; maybe do this in Enter() of Move state?
            //battleManager.actionCount++; maybe do this in Enter() of Move state?
        }

        if (characterState.attackSelected == true)
        {
            Debug.Log("AttackSelected, to Attack state");
            //go to Attack state
            characterStateMachine.ChangeState(characterState.state_Attack);
            //battleManager.attackSelected = false; maybe do this in Enter() of Attack state?
            //battleManager.actionCount++; maybe do this in Enter() of Attack state?
        }

        // TODO: Implement Special
        if (characterState.specialSelected == true)
        {
            Debug.Log("SpecialSelected, to Special state");
            //go to Special state
            //this.characterStateMachine.ChangeState(new Special(characterStateMachine, this.gameObject));
            //battleManager.specialSelected = false; maybe do this in Enter() of Special state?
            //battleManager.actionCount++; maybe do this in Enter() of Special state?
        }

        if (characterState.endTurnSelected == true)
        {
            Debug.Log("EndTurn Selected");
            //End Turn
            battleManager.nextCharacter = true;

            //battleManager.specialSelected = false; maybe do this in Enter() of Special state?
            //battleManager.actionCount++; maybe do this in Enter() of Special state?
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
        /*
        Debug.Log("Exiting ActionSelect");
        this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
        */
    }
}
