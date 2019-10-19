using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    public AIState(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;
    }

    /*
    BattleStateMachine owner;
    BattleManager battleManager;
    GameObject character;

    public ActorController acScript;

    public AIState(BattleStateMachine newOwner, GameObject a_Character)
    {
       this.owner = newOwner;
       character = a_Character;
    }
    */

    public void Enter()
    {
        Debug.Log("Entering AI state");
        battleManager = BattleManager.Instance;

        //AIState controls actions, not an action itself
        //battleManager.actionCount++; //inc. actionCount, by one to allow for multi-action selections per turn //success! //may have to move depending on what option enemy AI picks
        //this.battleStateMachine.UpdateState();

        /*
        Debug.Log("Entering AIState");
        battleManager = BattleManager.Instance;
        acScript = character.GetComponent<ActorController>();
        */
    }

    public void Execute()
    {
        Debug.Log("Executing AI state, **ADD FUNC.**");
        //AI DOES STUFF HERE...

        // Find Player
        // TODO: Needs Reference to Player


        // If Player is in AttackRange: Attack

        // If Player is not in AttackRange: Move towards nearest Player


        if (characterState.actionCount >= GameSettings.Instance.MaxActionCount)
        {
            characterStateMachine.ChangeState(characterState.state_Idle);
        }
        #region Original Player Code
        /*
        Debug.Log("Executing AIState"); //success

        if (battleManager.moveSelected == true && battleManager.actionCount < 2) //stops at actionCount of 2 (allows for 2 option picks per turn)
        {
            Debug.Log("Move selected"); //success
            //do move here... (add in functionality later)
            //if (acScript.enabled == false)
            //{
                //acScript.enabled = true;
            //}
            //tried enable/disable of entire ActorController script, no luck

            //acScript.Move();

            //owner.ChangeState(new IState());

            battleManager.actionCount++;
            Debug.Log("Action count:" + battleManager.actionCount); //success
            battleManager.moveSelected = false; //returns to false correctly
        }

        if (battleManager.attackSelected == true && battleManager.actionCount < 2) //same as above rule
        {
            Debug.Log("Attack selected"); //success
            //do attack here... (add in functionality later)
            battleManager.actionCount++;
            Debug.Log("Action count:" + battleManager.actionCount); //success
            battleManager.attackSelected = false; //returns to false correctly
        }

        if (battleManager.specialSelected == true && battleManager.actionCount < 2) //same as above rule
        {
            Debug.Log("Special selected"); //success
            //do special option here... (add in functionality later)
            battleManager.actionCount++;
            Debug.Log("Action count:" + battleManager.actionCount); //success
            battleManager.specialSelected = false; //returns to false correctly
        }

         //if(actionCount >= 2 || endTurnSelected == true) ??
         //{
         //Exit();
         //}
        */
        #endregion
    }

    public void Exit()
    {
        Debug.Log("Exiting AI state");
        /*
        //need to leave state... revert back to the previous CharacterState...
        Debug.Log("Exiting AIState");
        */
    }
}
