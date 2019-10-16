using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : IState
{
    private BattleStateMachine battleStateMachine;
    private GameObject gameObject;

    BattleManager battleManager;

    public Special(BattleStateMachine battleStateMachine, GameObject gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    /*
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
    */

    public void Enter()
    {
        Debug.Log("Entering special state"); //success!
        battleManager = BattleManager.Instance;
        battleManager.specialSelected = false; //reset specialSelected //success!
        battleManager.actionCount++; // or = (battleManager.actionCount + 2); //inc. actionCount, by one or two to allow/avoid multi-special selections per turn //success!
        this.battleStateMachine.UpdateState();
    }

    public void Execute() //crashes when selected twice...
    {
        Debug.Log("Executing special state, **ADD FUNC.**");
        //TO DO: do special function here!

        /*
        Debug.Log("Executing Special");
        //perform special here, add functionality later on

        //increase actionCount, by just 1? SUCCESSFULLY INCREMENTS...
        battleManager.actionCount++;
        Exit();
        */

        this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject));
    }

    public void Exit()
    {
        Debug.Log("Exiting special state");
        /*
        Debug.Log("Exiting Special");
        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
        //need to exit and return to CharacterSelect...
        */
    }
}
