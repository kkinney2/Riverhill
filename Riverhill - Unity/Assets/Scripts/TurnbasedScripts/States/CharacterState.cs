using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : IState
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();
    BattleManager battleManager;

    private GameObject character;
    public CharacterState(BattleStateMachine battleStateMachine, GameObject a_Character)
    {
        this.battleStateMachine = battleStateMachine;
        this.character = a_Character;
    }

    public void Enter()
    {
        Debug.Log("Entering CharacterState");
        battleManager = BattleManager.Instance;
        Execute();
    }

    public void Execute()
    {
        Debug.Log("Executing CharacterState");

        if (battleManager.playerTurn == true && battleManager.enemyTurn == false)
        {
            Debug.Log("Player turn started-->ActionSelect");
            this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.character));
        }

        if (battleManager.playerTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }
        battleManager.playerTurn = false;

        if (battleManager.playerTurn == false && battleManager.enemyTurn == true)
        {
            Debug.Log("Enemy turn started-->AIState");
            this.battleStateMachine.ChangeState(new AIState(battleStateMachine, this.character));
        }

        if (battleManager.enemyTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }
        battleManager.enemyTurn = false;
    }

    public void Exit()
    {
        Debug.Log("Exiting CharacterState");
    }
}
