using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Can be used to "Idle" a Character so they they don't accidently
 * use a state while switching characters or states
 */

public class Idle : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    public Idle(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;
    }

    public void Enter()
    {
        Debug.Log("Entering Idle State");
        battleManager = BattleManager.Instance;
    }

    public void Execute()
    {
        //Debug.Log("Executing Idle State");
        if (characterState.actionCount >= battleManager.gameController.gameSettings.MaxActionCount)
        {
            battleManager.nextCharacter = true;

            // Exiting/switching character states already does this
            //characterState.actionCount = 0;
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
