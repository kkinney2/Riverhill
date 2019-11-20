using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelect : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    CharacterPathfinding pathfinder;

    //Could possibly reorganize this all later & move it into Attack state?
    public GameObject attackButton;
    //For now we're going to check if attack is possible before button is even selected... work on disabling button if attack not possible yet
    // TODO: Move attack ranges to characterStats so they are accessible from inspector
    public Vector2 meleeAttackRange = new Vector2(1, 1);
    public bool enemyInMAttackRange = false;
    public Vector2 rangedAttackRange = new Vector2(1, 3);
    public bool enemyInRAttackRange = false;

    public GameObject player;
    public GameObject enemy;

    public ActionSelect(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;

        pathfinder = characterState.pathfinder;
    }

    public void Enter()
    {
        attackButton = GameObject.FindGameObjectWithTag("P1AttackButton"); //get Attack button (part of UI prefab)

        //attempting tracking enemy position and determining when attack is possible
        //there's probably a better way to do this/cleaner way to code it... whoops :-)
        //player = GameObject.FindGameObjectWithTag("Player"); //get player
        //enemy = GameObject.FindGameObjectWithTag("Enemy"); //get enemy
        //Debug.Log("Enemy pos: " + enemy.transform.position); //tracking enemy pos., using this info for finding when attack is possible

        Debug.Log("Entering ActionSelect state");
        Debug.Log("actionCount: " + characterState.actionCount);
        battleManager = BattleManager.Instance;
        this.characterStateMachine.UpdateState();
        /*        
        acScript = character.GetComponent<ActorController>();
        */
    }

    public void Execute()
    {
        //Debug.Log("Executing ActionSelect state");

        #region IsAttackPossible?
        //First---Check if enemy is in melee attack range & adjust bool accordingly
        //May be a better way to do this...
        /*
         * if ("Enemy" tag found in neighboring tiles, within meleeAttackRange x = 1 & y = 1) {
         *  enemyInMAttackRange = true;
         * }
         * 
         * else {
         *  enemyInMAttackRange = false;
         * }
         * 
        */

        //rn, it approves attack when they are on the same space
        //we want it to approve if enemy is within range of one tile away
        /*
        if (enemy.transform.position == player.transform.position) 
        {
            enemyInMAttackRange = true;
        }
        
        else 
        {
            enemyInMAttackRange = false;
        }
        */

        for (int i = 0; i < battleManager.characterStates_Enemy.Count; i++)
        {
            pathfinder.FindPath(battleManager.characterStates_Enemy[i].character.transform.position);

            if (pathfinder.path.Count == 1) // TODO: Hardcoded attack range
            {
                enemyInMAttackRange = true; // SIDENOTE: When I first read this, I read it as "enemy in MAH attack range" xD
            }
            else
            {
                enemyInMAttackRange = false;
            }
        }

        //If so-enable Attack button/make interactable
        if (enemyInMAttackRange == true) {
            attackButton.GetComponent<Button>().interactable = true;
        }

        //If not-disable Attack button/make not-interactable
        if (enemyInMAttackRange == false) {
            attackButton.GetComponent<Button>().interactable = false;
        }
        #endregion

        if (characterState.moveSelected == true && characterState.actionCount <= GameSettings.Instance.MaxActionCount) // (&& actionCount <= 2)
        {
            Debug.Log("MoveSelected, to Move state");
            characterStateMachine.ChangeState(characterState.state_Move);
        }

        if (characterState.attackSelected == true && characterState.actionCount <= GameSettings.Instance.MaxActionCount) // (&& actionCount <= 2)
        {
            Debug.Log("AttackSelected, to Attack state");
            //go to Attack state
            characterStateMachine.ChangeState(characterState.state_Attack);
        }

        // TODO: Implement Special
        if (characterState.specialSelected == true && characterState.actionCount <= GameSettings.Instance.MaxActionCount) // (&& actionCount <= 2)
        {
            Debug.Log("SpecialSelected, to Special state");
            //go to Special state
            //this.characterStateMachine.ChangeState(new Special(characterStateMachine, this.gameObject));
        }

        if (characterState.endTurnSelected == true)
        {
            Debug.Log("EndTurn Selected");
            //End Turn
            characterStateMachine.ChangeState(characterState.state_Idle);
            battleManager.nextCharacter = true;
        }

    }

    public void Exit()
    {
        Debug.Log("Exiting ActionSelect state");
        ResetSelected();
    }

    private void ResetSelected()
    {
        characterState.moveSelected = false;
        characterState.attackSelected = false;
        characterState.specialSelected = false;
        characterState.endTurnSelected = false;
    }
}
