using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    CharacterState characterState;
    BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    CharacterPathfinding pathfinder;

    GameObject tileHighlight_Pos;
    GameObject tileHighlight_Neg;

    // TODO: Move.attack is for what?
    //public bool attack = true;
    bool isDone = false;

    public Move(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;

        pathfinder = characterState.character.gameObject.GetComponent<CharacterPathfinding>();
    }

    /*
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    private GameObject character;
    public Move(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }

    public ActorController acScript;
    */

    public void Enter()
    {
        Debug.Log("Entering move state"); //success!
        battleManager = BattleManager.Instance;
        characterState.actionCount++; //inc. actionCount, by one to allow for multi-move selections per turn //success!

        // Put here instead of cnstr to ensure grab after creation
        tileHighlight_Pos = GameSettings.Instance.tileHighlight_Positive;
        tileHighlight_Neg = GameSettings.Instance.tileHighlight_Negative;
        /* TODO: Correct Move functionality to work without coroutine?
         * OR create a helper function that does the work, like ActorController,
         * but is able to report when it has completed so the Move state can wait and 
         * then move on
         */
    }

    public void Execute()
    {
        //Debug.Log("Executing move state, **ADD FUNC.**");

        // TODO: Move Function is "Auto" Finishing until it is implemented
        //isDone = true;

        pathfinder.FindPath();
        if (pathfinder.path.Count >= pathfinder.range.x) // If the path is longer than or equal to the min range
        {
            tileHighlight_Pos.SetActive(true);
            tileHighlight_Neg.SetActive(false);

            tileHighlight_Pos.transform.position = pathfinder.path[pathfinder.path.Count - 1].transform.position;

            if (pathfinder.path.Count > pathfinder.range.y) // If the path is shorter than the max range
            {
                tileHighlight_Pos.SetActive(false);
                tileHighlight_Neg.SetActive(true);

                tileHighlight_Neg.transform.position = pathfinder.path[pathfinder.path.Count - 1].transform.position;
            }
        }
        else
        {
            tileHighlight_Pos.SetActive(false);
            tileHighlight_Neg.SetActive(false);
        }

        if (isDone)
        {
            characterStateMachine.ChangeState(characterState.state_Idle);
        }

        /*
        Debug.Log("Executing Move");
        acScript.Move();
        
        battleManager.actionCount++;
        Debug.Log("Action count:" + battleManager.actionCount); //success
        battleManager.moveSelected = false; //returns to false correctly
        */
    }

    public void Exit()
    {
        Debug.Log("Exiting Move State");

        tileHighlight_Pos.SetActive(false);
        tileHighlight_Neg.SetActive(false);

        characterState.hasActiveAction = false;
        isDone = false;
    }
}
