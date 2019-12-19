using System.Collections;
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

    //TODO: AI attack proximity is hardcoded
    float proximityRange = 5;

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

        // Update Player Characters
        UpdatePlayerCharacters();

    }

    public void Execute()
    {
        Debug.Log("Executing AI state");
        //AI DOES STUFF HERE...

        float minDistance = float.MaxValue;
        bool hasTarget = false;

        // Find Player and Analyze Options
        for (int i = 0; i < playerCharacterStates.Count; i++)
        {
            // Is the character in range?
            pathfinder.FindPath(playerCharacterStates[i].character.transform.position);

            // If they are within attack range
            if (pathfinder.path.Count <= characterState.characterStats.meleeAttackRange.y)
            {
                // and there is no current target
                if (!hasTarget)
                {
                    // Store the target
                    characterState.AI_Target = playerCharacterStates[i];
                    hasTarget = true;
                }
                // if there is a current target
                else
                {
                    // If the current loop's target is less than my current target's health
                    if (characterState.AI_Target.characterStats.CurrentHP > playerCharacterStates[i].characterStats.CurrentHP)
                    {
                        // make the lesser health my target
                        characterState.AI_Target = playerCharacterStates[i];
                    }
                }

                // Continue to analyze other options
                continue;
            }
        }

        // If unable to find attack target, search to move
        if (!hasTarget)
        {
            for (int i = 0; i < playerCharacterStates.Count; i++)
            {
                if (pathfinder.path.Count <= minDistance)
                {
                    // If its the same distance
                    if (pathfinder.path.Count == minDistance)
                    {
                        // Compare the two
                        // If the current loop's target is less than my current target's health
                        if (characterState.AI_Target.characterStats.CurrentHP > playerCharacterStates[i].characterStats.CurrentHP)
                        {
                            // make the lesser health my target
                            characterState.AI_Target = playerCharacterStates[i];

                            // And continue searching
                            continue;
                        }
                    }
                    // Make them the new min
                    minDistance = pathfinder.path.Count;
                    characterState.AI_Target = playerCharacterStates[i];
                }
            }
        }
        
        // Attack
        if (hasTarget)
        {
            characterStateMachine.ChangeState(characterState.state_Attack);
        }
        // If Player is not in AttackRange: Move towards nearest Player
        else
        {
            Debug.Log("AI could not find close enough target. Moving Closer");
            characterStateMachine.ChangeState(characterState.state_Move);
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
