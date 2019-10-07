using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : IState
{
    BattleStateMachine owner;
    BattleStats battleStats;
    GameObject character;

    public ActorController acScript;

    public ActionSelect(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }

    /*
    void Start()
    {
        GameObject playerAlyss = GameObject.Find("Alyss");
        acScript = playerAlyss.GetComponent<ActorController>();
        acScript.enabled = false;
    }
    */
    //tried enable/disable of entire ActorController script, no luck

    public void Enter()
    {
        battleStats = BattleStats.Instance;
        acScript = character.GetComponent<ActorController>();
    }

    public void Execute()
    {
        

        Debug.Log("ActionSelect"); //success

        if (battleStats.moveSelected == true && battleStats.actionCount < 2) //stops at actionCount of 2 (allows for 2 option picks per turn)
        {
            Debug.Log("Move selected"); //success
            //do move here... (add in functionality later)
            /*
            if (acScript.enabled == false)
            {
                acScript.enabled = true;
            }
            */
            //tried enable/disable of entire ActorController script, no luck

            acScript.Move();

            /*
            owner.ChangeState(new IState());
            */



            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount); //success
            battleStats.moveSelected = false; //returns to false correctly
        }

        if (battleStats.attackSelected == true && battleStats.actionCount < 2) //same as above rule
        {
            Debug.Log("Attack selected"); //success
            //do attack here... (add in functionality later)
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount); //success
            battleStats.attackSelected = false; //returns to false correctly
        }

        if (battleStats.defendSelected == true && battleStats.actionCount < 2) //same as above rule
        {
            Debug.Log("Defend selected"); //success
            //do defend here... (add in functionality later)
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount); //success
            battleStats.defendSelected = false; //returns to false correctly
        }

        /*
        if(battleStats.actionCount >= 2) //after two selections
        {
            //this makes turnCount increase like crazy...
            battleStats.turnCount++;
            //we need to leave this ActionSelect
            Exit(); //exit said state
        }
        */
    }

    public void Exit()
    {
        //need to leave state...
    }
}
