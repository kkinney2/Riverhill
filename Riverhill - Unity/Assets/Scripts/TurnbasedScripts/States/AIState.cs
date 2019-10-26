using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    List<CharacterState> playerCharacterStates = new List<CharacterState>();

    //TODO: AI attack proximity is hardcoded
    float proximityRange = 5;

    public AIState(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;
    }

    public void Enter()
    {
        Debug.Log("Entering AI state");
        battleManager = BattleManager.Instance;

        // Grab Player Characters
        for (int i = 0; i < battleManager.characterStates.Count; i++)
        {
            if (!battleManager.characterStats[i].isEnemy)
            {
                playerCharacterStates.Add(battleManager.characterStates[i]);
            }
        }

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
        }

        // If Player is not in AttackRange: Move towards nearest Player
        if (!hasTarget)
        {
            Debug.Log("AI could not find close enough target. Moving Closer");
            characterStateMachine.ChangeState(characterState.state_Move);
        }

        if (characterState.actionCount >= GameSettings.Instance.MaxActionCount)
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
}
