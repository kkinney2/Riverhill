using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Special : IState
{
    public GameController gameController;
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;
    CharacterStats characterStats;

    bool isDone = false;

    public Special(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

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
        battleManager = gameController.battleManager;
        // TODO: What adds to actionCount? the action or the decision maker?
        //battleManager.actionCount++; // or = (battleManager.actionCount + 2); //inc. actionCount, by one or two to allow/avoid multi-special selections per turn //success!

        // Currently ignoring error until specials are being integrated
        //this.battleStateMachine.UpdateState();

        /*
        foreach (GameObject ally in GameObject.FindGameObjectsWithTag("Player"))
        {
            alliesList.Add(ally);
        }
        foreach(var ally in alliesList)
        {
            Debug.Log("ally: " + ally);
        }
        */

    }

    public void Execute() //crashes when selected twice...
    {
        Debug.Log("Executing special state, **ADD FUNC.**");

        //TO DO: do special function here!
        if (battleManager.player.name.Contains("Alyss"))
        {
            Debug.Log("Healing Aura");
            //Do Healing Aura

            characterState.characterStats.charSounds.clip = characterState.characterStats.charHeal;
            characterState.characterStats.charSounds.Play();

            foreach (var ally in battleManager.alliesList)
            {
                //characterState.characterStats.CurrentHP = (characterState.characterStats.CurrentHP + characterState.characterStats.healingAuraHP);
                ally.GetComponent<CharacterStats>().IsHealing();
                ally.GetComponent<CharacterStats>().CurrentHP = (ally.GetComponent<CharacterStats>().CurrentHP + ally.GetComponent<CharacterStats>().healingAuraHP);
                //make sure it doesn't add past base health;
                if (ally.GetComponent<CharacterStats>().CurrentHP > ally.GetComponent<CharacterStats>().BaseHP)
                {
                    ally.GetComponent<CharacterStats>().CurrentHP = ally.GetComponent<CharacterStats>().BaseHP;
                }
            }
            isDone = true;
        }

        if (battleManager.player.name.Contains("Dayana"))
        {
            Debug.Log("Knockback");
            //Do Knockback
            foreach (var enemy in battleManager.enemiesList)
            {
                enemy.GetComponent<CharacterStats>().CurrentHP = (enemy.GetComponent<CharacterStats>().CurrentHP - enemy.GetComponent<CharacterStats>().knockbackAD);
            }
            isDone = true;
        }


        if (battleManager.player.name.Contains("Nelson"))
        {
            Debug.Log("Long Range");
            //Do Long Range
            isDone = true;
        }

        /*
        Debug.Log("Executing Special");
        //perform special here, add functionality later on

        //increase actionCount, by just 1? SUCCESSFULLY INCREMENTS...
        battleManager.actionCount++;
        Exit();
        */

        // Currently ignoring error until specials are being integrated
        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject));

        if (isDone)
        {
            Debug.Log("Special isDone");
            //characterState.actionCount++;
            characterState.actionCount = (characterState.actionCount + battleManager.gameController.gameSettings.MaxActionCount); //inc. actionCount, by max to avoid multi-special selections per turn //success!
            Debug.Log("ActionCount = " + characterState.actionCount);
            characterStateMachine.ChangeState(characterState.state_Idle);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting special state");
        isDone = false;
        /*
        Debug.Log("Exiting Special");
        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.character));
        //need to exit and return to CharacterSelect...
        */
    }
}
