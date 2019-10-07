using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : IState
{
    public bool moveSelected = false;
    public bool attackSelected = false;
    public bool defendSelected = false;

    public void Enter()
    {
        
    }

    public void Execute()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();

        Debug.Log("ActionSelect");

        if(moveSelected == true && battleStats.actionCount <= 2) //&& actionCount <= 2;
        {
            Debug.Log("Move selected");
            //do move
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount);
            moveSelected = false;
        }

        if (moveSelected == true && battleStats.actionCount <= 2) //&& actionCount <= 2;
        {
            Debug.Log("Attack selected");
            //do attack
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount);
            attackSelected = false;
        }

        if (moveSelected == true && battleStats.actionCount <= 2) //&& actionCount <= 2;
        {
            Debug.Log("Defend selected");
            //do defend
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount);
            defendSelected = false;
        }
    }

    public void Moving()
    {
        moveSelected = true;
    }

    public void Attacking()
    {
       attackSelected = true;
    }

    public void Defending()
    {
        defendSelected = true;
    }

    public void Exit()
    {
        
    }
}
