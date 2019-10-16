﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : IState
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();
    private GameObject gameObject;

    BattleManager battleManager;

    public CharacterState(BattleStateMachine battleStateMachine, GameObject gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    /*
    private GameObject character;
    public CharacterState(BattleStateMachine battleStateMachine, GameObject a_Character)
    {
       this.battleStateMachine = battleStateMachine;
       this.character = a_Character;
    }
    */

    public void Enter()
    {
        Debug.Log("Entering CharacterState state");
        //Debug.Log("CS: " + battleStateMachine.currentState); //success!
        //Debug.Log("PS: " + battleStateMachine.previousState); //success!
        battleManager = BattleManager.Instance;
        this.battleStateMachine.UpdateState();
    }

    public void Execute()
    {
        Debug.Log("Executing CharacterState state");
        if (battleManager.playerTurn == true && battleManager.enemyTurn == false)
        {
            Debug.Log("Player turn started-->ActionSelect"); //success
            this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.gameObject));
        }

        if (battleManager.playerTurn == false && battleManager.enemyTurn == true)
        {
            Debug.Log("Enemy turn started-->AIState"); //success
            this.battleStateMachine.ChangeState(new AIState(battleStateMachine, this.gameObject));
        }

        /*
        if (battleManager.playerTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }
        battleManager.playerTurn = false;

        if (battleManager.enemyTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }
        battleManager.enemyTurn = false;
        */
    }

    public void Exit()
    {
        Debug.Log("Exiting CharacterState state"); //success
    }
}
