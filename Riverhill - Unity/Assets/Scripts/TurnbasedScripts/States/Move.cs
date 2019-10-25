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

    public Move(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;

        pathfinder = characterState.character.gameObject.GetComponent<CharacterPathfinding>();
    }

    public void Enter()
    {
        Debug.Log("Entering move state"); //success!
        battleManager = BattleManager.Instance;
        characterState.actionCount++; //inc. actionCount, by one to allow for multi-move selections per turn //success!

        for (int i = 0; i < TileManager.Instance.tilesList.Count; i++)
        {
            //TileManager.Instance.tilesList[i].GetComponent<Tile>();

            pathfinder.FindPath(TileManager.Instance.tilesList[i].transform.position);


            if (pathfinder.path.Count >= pathfinder.range.x) // If the path is longer than or equal to the min range
            {
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(true);
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);

                if (pathfinder.path.Count > pathfinder.range.y) // If the path is shorter than the max range
                {
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(false);
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(true);
                }
            }
            else
            {
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(false);
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);
            }
        }

        pathfinder.Move();
    }

    public void Execute()
    {
        if (pathfinder.isDone)
        {
            pathfinder.isDone = false;
            characterStateMachine.ChangeState(characterState.state_Idle);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Move State");

        for (int i = 0; i < TileManager.Instance.tilesList.Count; i++)
        {
            TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(false);
            TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);
        }
        characterState.hasActiveAction = false;
    }
}
