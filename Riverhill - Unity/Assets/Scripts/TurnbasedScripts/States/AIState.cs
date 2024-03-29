﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIState : IState
{
    public GameController gameController;
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    CharacterPathfinding pathfinder;

    List<CharacterState> playerCharacterStates = new List<CharacterState>();

    float proximityRange = 5;
    GameObject post;

    public AIState(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;

        pathfinder = characterState.pathfinder;
    }

    public void Enter()
    {
        Debug.Log("Entering AI state");
        battleManager = gameController.battleManager;
        proximityRange = characterState.characterStats.agroRange;

        // Update Player Characters
        UpdatePlayerCharacters();

    }

    public void Execute()
    {
        Debug.Log("Executing AI state");
        //AI DOES STUFF HERE...

        float minDistance = float.MaxValue;
        bool hasAttackTarget = false;
        bool hasMoveTarget = false;

        // Find Player and Analyze Options
        for (int i = 0; i < playerCharacterStates.Count; i++)
        {
            // Is the character in range?
            pathfinder.FindPath(playerCharacterStates[i].character.transform.position, "raw");

            // If they are within attack range
            if (pathfinder.path.Count <= characterState.characterStats.meleeAttackRange.y)
            {
                Debug.Log("Found play in attack range");

                // and there is no current target
                if (!hasAttackTarget)
                {
                    // Store the target
                    characterState.AI_Target = playerCharacterStates[i];
                    hasAttackTarget = true;
                }
                // if there is a current target
                else
                {
                    // If the current loop's target is less than my current target's health
                    if (characterState.AI_Target.characterStats.CurrentHP > playerCharacterStates[i].characterStats.CurrentHP)
                    {
                        // make the lesser health my target
                        Debug.Log("Changing Target");
                        characterState.AI_Target = playerCharacterStates[i];
                    }
                }

                // Continue to analyze other options
                continue;
            }
        }

        // If unable to find attack target, search to move
        if (!hasAttackTarget)
        {
            Debug.Log("Was unable to attack, searching for move target");
            for (int i = 0; i < playerCharacterStates.Count; i++)
            {
                pathfinder.FindPath(playerCharacterStates[i].character.transform.position, "movement");

                // If they are closer and if they are within proximity for agro
                if (pathfinder.path.Count <= minDistance && pathfinder.path.Count <= proximityRange)
                {

                    // If its the same distance
                    if (pathfinder.path.Count == minDistance && hasMoveTarget)
                    {
                        Debug.Log("They are equally close...");
                        // Compare the two
                        // If the current loop's target is less than my current target's health
                        if (characterState.AI_Target.characterStats.CurrentHP > playerCharacterStates[i].characterStats.CurrentHP)
                        {
                            Debug.Log("...but this one is weaker");
                            // make the lesser health my target
                            characterState.AI_Target = playerCharacterStates[i];
                            hasMoveTarget = true;
                            // And continue searching
                            continue;
                        }
                    }
                    // Make them the new min

                    Debug.Log("This one is closer");
                    minDistance = pathfinder.path.Count;
                    characterState.AI_Target = playerCharacterStates[i];
                    hasMoveTarget = true;
                }
            }
        }

        // Attack
        if (hasAttackTarget)
        {
            characterStateMachine.ChangeState(characterState.state_Attack);
        }

        // If Player is not in AttackRange: Move towards nearest Player
        if (hasMoveTarget)
        {
            Debug.Log("Moving Closer to Target: " + characterState.AI_Target.characterStats.Name);
            characterStateMachine.ChangeState(characterState.state_Move);
        }
        else if (!hasMoveTarget && !hasAttackTarget)
        {
            if (characterState.character.transform.position != characterState.characterStats.startPos)
            {
                Debug.Log("Has no target, returning to post");
                pathfinder.FindPath(characterState.characterStats.startPos, "movement");

                // Will then use that path to return to starting pos
                characterStateMachine.ChangeState(characterState.state_Move);
            }
            else
            {
                // No action to perform
                Debug.Log("Nothing to Do");

                // End Turn
                characterState.actionCount = battleManager.gameController.gameSettings.MaxActionCount;
            }
            
        }

        if (characterState.actionCount >= battleManager.gameController.gameSettings.MaxActionCount)
        {
            characterStateMachine.ChangeState(characterState.state_Idle);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting AI state");
        /*
        //need to leave state... revert back to the previous CharacterState...
        Debug.Log("Exiting AIState");
        */
    }

    void UpdatePlayerCharacters()
    {
        for (int i = 0; i < battleManager.characterStates.Count; i++)
        {
            if (!battleManager.characterStats[i].isEnemy && battleManager.characterStats[i].CurrentHP > 0)
            {
                if (!playerCharacterStates.Contains(battleManager.characterStates[i]))
                {
                    playerCharacterStates.Add(battleManager.characterStates[i]);
                }
            }
            else
            {
                playerCharacterStates.Remove(battleManager.characterStates[i]);
                //playerCharacterStates.RemoveAll(battleManager.characterStates[i]);
            }
        }
    }
}
