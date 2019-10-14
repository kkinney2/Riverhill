using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    //Merged into CharacterState

    /*
    private BattleStateMachine playerStateMachine = new BattleStateMachine();
    BattleManager battleManager;

    public PlayerStats player;
    
    private void Start()
    {
        //Debug.Log("PSM start");
        player.playerCurrentHP = player.playerBaseHP; //working correctly, can see stats in inspector

        battleManager = BattleManager.Instance;
    }

    private void Update()
    {
        //Debug.Log("PSM start, player turn: " + battleStats.playerTurn);

        if (battleManager.playerTurn == true) //entering if statement correctly, based on debugs below
        {
            //Debug.Log("Player turn started");
            this.playerStateMachine.ChangeState(new ActionSelect(playerStateMachine, this.gameObject)); //moves to the ActionSelect state correctly, based on debugs
            //Debug.Log("Player AS");
        }

        if (battleManager.playerTurn != false)
        {
            this.playerStateMachine.UpdateState();
        }
        battleManager.playerTurn = false;
    }
    */
}