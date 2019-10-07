using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();

    public PlayerStats player;
    
    private void Start()
    {
        //Debug.Log("PSM start");
        player.playerCurrentHP = player.playerBaseHP; //working correctly, can see stats in inspector
    }

    private void Update()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();

        //Debug.Log("PSM start, player turn: " + battleStats.playerTurn); //returning false ?

        if (battleStats.playerTurn == true) //entering if statement correctly, based on debugs below
        {
            //Debug.Log("Player turn (SM) started");
            this.battleStateMachine.ChangeState(new ActionSelect()); //moves to the ActionSelect state correctly, based on debugs
            //Debug.Log("Player AS");
        }

        this.battleStateMachine.UpdateState();
    }
}