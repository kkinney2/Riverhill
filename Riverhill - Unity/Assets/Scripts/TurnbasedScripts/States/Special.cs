using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    public Special(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;
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
        // TODO: What adds to actionCount? the action or the decision maker?
        //battleManager.actionCount++; // or = (battleManager.actionCount + 2); //inc. actionCount, by one or two to allow/avoid multi-special selections per turn //success!
        
        // Currently ignoring error until specials are being integrated
        //this.battleStateMachine.UpdateState();
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

        // Currently ignoring error until specials are being integrated
        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject));
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
