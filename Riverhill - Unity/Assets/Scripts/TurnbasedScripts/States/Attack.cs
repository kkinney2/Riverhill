using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    CharacterPathfinding pathfinder;

    bool isDone = false;

    public Attack(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;

        pathfinder = characterState.character.gameObject.GetComponent<CharacterPathfinding>();
    }

    /*
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    //private object gameObject;
    private GameObject character;
    public Attack(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }
    */

    public void Enter()
    {
        Debug.Log("Entering attack state"); //success!
        battleManager = BattleManager.Instance;
        
        characterState.hasActiveAction = true;
    }

    public void Execute()
    {
        Debug.Log("Executing attack state, **ADD FUNC.**");
        //TODO: do attacking function here!

        if (isDone)
        {
            characterState.actionCount = (characterState.actionCount + 2); //inc. actionCount, by two to avoid multi-attack selections per turn //success!
            characterStateMachine.ChangeState(characterState.state_Idle);
        }

        /*
        Debug.Log("Executing Attack");
        //perform attack here, add functionality later on

        //increase actionCount, maybe by 2 to prevent acting again after attacking?
        battleManager.actionCount = (battleManager.actionCount) + 2;
        Exit();
        */


        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject));
    }

    public void Exit()
    {
        Debug.Log("Exiting Attack State");
        characterState.hasActiveAction = false;
        isDone = false;
    }
}
