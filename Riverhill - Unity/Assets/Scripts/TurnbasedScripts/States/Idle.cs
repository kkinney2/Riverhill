using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * Can be used to "Idle" a Character so they they don't accidently
 * use a state while switching characters or states
 */

public class Idle : IState
{
    public GameController gameController;
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    public Idle(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;
    }

    public void Enter()
    {
        Debug.Log("Entering Idle State");
        battleManager = gameController.battleManager;
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
