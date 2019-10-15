using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : IState
{
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    //private object gameObject;
    private GameObject character;
    public Special(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }

    public void Enter()
    {
        Debug.Log("Entering Special"); //success
        battleManager = BattleManager.Instance;
        Execute(); //success
    }

    public void Execute() //crashes when selected twice...
    {
        Debug.Log("Executing Special");
        //perform special here, add functionality later on

        //increase actionCount, by just 1? SUCCESSFULLY INCREMENTS...
        battleManager.actionCount++;
        Exit();
    }

    public void Exit()
    {
        Debug.Log("Exiting Special");
        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
        //need to exit and return to CharacterSelect...
    }
}
