using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    private BattleStateMachine playerStateMachine = new BattleStateMachine();
    BattleStats battleStats;

    public PlayerStats player;
    
    private void Start()
    {
        //Debug.Log("PSM start");
        player.playerCurrentHP = player.playerBaseHP; //working correctly, can see stats in inspector

        battleStats = BattleStats.Instance;
    }

    private void Update()
    {

        //Debug.Log("PSM start, player turn: " + battleStats.playerTurn); //returning false ?

        if (battleStats.playerTurn == true) //entering if statement correctly, based on debugs below
        {
            //Debug.Log("Player turn (SM) started");
            this.playerStateMachine.ChangeState(new ActionSelect(playerStateMachine, this.gameObject)); //moves to the ActionSelect state correctly, based on debugs
            //Debug.Log("Player AS");


            
        }

        if (battleStats.playerTurn != false)
        {
            this.playerStateMachine.UpdateState();
        }
        

        battleStats.playerTurn = false;
    }
}