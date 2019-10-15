using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : IState
{
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

    public void Enter()
    {
        Debug.Log("Enter ActionSelect");
        battleManager = BattleManager.Instance;
        acScript = character.GetComponent<ActorController>();
    }

    public void Execute()
    {
        Debug.Log("Execute ActionSelect");

        if (battleManager.moveSelected == true && battleManager.actionCount < 2) //stops at actionCount of 2 (allows for 2 option picks per turn)
        {
            Debug.Log("Move selected");
            this.battleStateMachine.ChangeState(new Move(battleStateMachine, this.character));


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
        }

        if (battleManager.attackSelected == true && battleManager.actionCount < 2) //same as above rule
        {
            Debug.Log("Attack selected");
            this.battleStateMachine.ChangeState(new Attack(battleStateMachine, this.character));


            /* MOVING TO ATTACK SCRIPT
            //do attack here... (add in functionality later)
            battleManager.actionCount++;
            Debug.Log("Action count:" + battleManager.actionCount); //success
            battleManager.attackSelected = false; //returns to false correctly
            */
        }

        if (battleManager.specialSelected == true && battleManager.actionCount < 2) //same as above rule
        {
            Debug.Log("Special selected");
            //this.battleStateMachine.ChangeState(new Special(battleStateMachine, this.character));
            //Crashes when special is selected twice...


            /* MOVING TO SPECIAL SCRIPT
            //do special option here... (add in functionality later)
            battleManager.actionCount++;
            Debug.Log("Action count:" + battleManager.actionCount); //success
            battleManager.specialSelected = false; //returns to false correctly
            */
        }

        if(battleManager.actionCount >= 2 || battleManager.endTurnSelected == true)
        {
            Exit();
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting ActionSelect");
        this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
    }
}
