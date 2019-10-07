using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : IState
{
    void Start()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();

        Debug.Log("ActionSelect");

        if(battleStats.moveSelected == true && battleStats.actionCount <= 2) //&& actionCount <= 2;
        {
            Debug.Log("Move selected");
            //do move
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount);
            battleStats.moveSelected = false;
        }

        if (battleStats.moveSelected == true && battleStats.actionCount <= 2) //&& actionCount <= 2;
        {
            Debug.Log("Attack selected");
            //do attack
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount);
            battleStats.attackSelected = false;
        }

        if (battleStats.moveSelected == true && battleStats.actionCount <= 2) //&& actionCount <= 2;
        {
            Debug.Log("Defend selected");
            //do defend
            battleStats.actionCount++;
            Debug.Log("Action count:" + battleStats.actionCount);
            battleStats.defendSelected = false;
        }
    }

    public void Exit()
    {
        
    }
}
