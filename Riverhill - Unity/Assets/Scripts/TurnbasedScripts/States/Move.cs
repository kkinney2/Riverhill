using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    CharacterState characterState;
    BattleStateMachine characterStateMachine;
    public BattleStateMachine battleStateMachine;

    IState state_CharacterAction;

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
        
        characterState.hasActiveAction = true;

        if (!characterState.characterStats.isEnemy)
        {
            for (int i = 0; i < TileManager.Instance.tilesList.Count; i++)
            {
                //TileManager.Instance.tilesList[i].GetComponent<Tile>();

                pathfinder.FindPath(TileManager.Instance.tilesList[i].transform.position);


                if (pathfinder.path.Count >= pathfinder.range.x) // If the path is longer than or equal to the min range
                {
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(true);
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);

                    if (pathfinder.path.Count > pathfinder.range.y || TileManager.Instance.tilesList[i].GetComponent<Tile>().hasCharacter == true) // If the path is shorter than the max range OR if it has a character
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
        else
        {
            pathfinder.FindPath(characterState.AI_Target.characterStats.gameObject.transform.position);
            pathfinder.MoveAlongPath();
        }
    }

    public void Execute()
    {
        //Debug.Log("Move_Execute");
        if (pathfinder.isDone == true)
        {
            Debug.Log("Move isDone");
            pathfinder.isDone = false;
            characterState.actionCount++; //inc. actionCount, by one to allow for multi-move selections per turn //success!
            Debug.Log("actionCount: " + characterState.actionCount);
            characterStateMachine.ChangeState(characterState.state_Idle);
            Debug.Log("actionCount: " + characterState.actionCount);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Move State");

        if (!characterState.characterStats.isEnemy)
        {
            for (int i = 0; i < TileManager.Instance.tilesList.Count; i++)
            {
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(false);
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);
            }
        }
        
        characterState.hasActiveAction = false;
    }
}
