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
        Debug.Log("Executing AI state, **ADD FUNC.**");
        //AI DOES STUFF HERE...

        float minDistance = float.MaxValue;
        bool hasTarget = false;

        // Find Player
        for (int i = 0; i < playerCharacterStates.Count; i++)
        {
            // Is the character in range?
            pathfinder.FindPath(playerCharacterStates[i].character.transform.position);

            if (pathfinder.path.Count == 1)
            {
                characterState.AI_Target = playerCharacterStates[i];
                hasTarget = true;
                characterStateMachine.ChangeState(characterState.state_Attack);
                break;
            }
            else
            {
                // Store the closest player character
                if (pathfinder.path.Count < minDistance)
                {
                    minDistance = pathfinder.path.Count;
                    characterState.AI_Target = playerCharacterStates[i];
                }
            }
            #region old AI
            /*
            float distance = (Mathf.Abs(Vector3.Distance(characterState.characterStats.gameObject.transform.position, playerCharacterStates[i].characterStats.gameObject.transform.position)));
            if (proximityRange > distance)
            {
                // They are in range, so attack
                characterState.AI_Target = playerCharacterStates[i];
                hasTarget = true;
                characterStateMachine.ChangeState(characterState.state_Attack);
                break;
            }
            else
            {
                // They aren't in range, store the closest and keep searching
                if (distance < minDistance)
                {
                    minDistance = distance;
                    characterState.AI_Target = playerCharacterStates[i];
                }
            }
            */
            #endregion
        }

        // If Player is not in AttackRange: Move towards nearest Player
        if (!hasTarget)
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
                playerCharacterStates.Add(battleManager.characterStates[i]);
            }
        }
    }
}
