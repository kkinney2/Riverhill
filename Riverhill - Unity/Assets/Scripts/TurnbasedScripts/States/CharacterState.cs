using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : IState
{
    public BattleStateMachine battleStateMachine;
    public GameObject character;
    public CharacterStats characterStats;

    public CharacterState AI_Target;

    BattleManager battleManager;

    IState state_CharacterAction;

    public Idle state_Idle;
    public Move state_Move;
    public Attack state_Attack;

    public bool hasActiveAction = false;
    public int actionCount = 0;

    //UI button options
    public bool moveSelected = false;
    public bool attackSelected = false;
    public bool specialSelected = false;
    public bool endTurnSelected = false; //essentially a pass option

    public Tile localTile;


    //public CharacterState(GameObject a_Character, BattleStateMachine a_BattleStateMachine)
    public CharacterState(GameObject a_Character)
    {
        battleManager = BattleManager.Instance;

        /* Each Character will have their own BattleStateMachine, this way
         * they can individually keep track of their actions taken and 
         * only affect themselves. After they are done the BattleManager will
         * switch characters and it can repeat.
         */
        character = a_Character;
        if (character.GetComponent<CharacterStats>() != null)
        {
            characterStats = character.GetComponent<CharacterStats>();
        }
        else
        {
            Debug.Log("Character " + character.name + "is either missing or failed to find its CharacterStats");
        }

        battleStateMachine = new BattleStateMachine();

        if (!characterStats.isEnemy)
        {
            state_CharacterAction = new ActionSelect(this, battleStateMachine);
        }
        else
        {
            state_CharacterAction = new AIState(this, battleStateMachine);
        }

        state_Idle = new Idle(this, battleStateMachine);
        state_Attack = new Attack(this, battleStateMachine);
        state_Move = new Move(this, battleStateMachine);
    }

    public void Enter()
    {
        Debug.Log("Entering CharacterState state");
    }

    public void Execute()
    {
        if ((actionCount >= GameSettings.Instance.MaxActionCount && !hasActiveAction) || endTurnSelected == true)
        {
            //Exit();
            this.battleStateMachine.ChangeState(state_Idle);
            battleManager.nextCharacter = true;
        }

        if (actionCount <= GameSettings.Instance.MaxActionCount || hasActiveAction)
        {
            if (hasActiveAction)
            {
                //Debug.Log("Updating Action");
                battleStateMachine.UpdateState();
            }
            else
            {
                if (!characterStats.isEnemy)
                {
                    Debug.Log("Sending to ActionSelect");
                }
                else Debug.Log("Sending to AIState");

                this.battleStateMachine.ChangeState(state_CharacterAction);
                hasActiveAction = true;
            }
        }

        #region Old Execute
        /*
        Debug.Log("Executing CharacterState state");

        if (battleManager.playerTurn == true && battleManager.enemyTurn == false && battleManager.actionCount < 2)
        {
            Debug.Log("Player turn started-->ActionSelect"); //success
            this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.gameObject));
        }

        if (battleManager.playerTurn == false && battleManager.enemyTurn == true && battleManager.actionCount < 2)
        {
            Debug.Log("Enemy turn started-->AIState"); //success
            this.battleStateMachine.ChangeState(new AIState(battleStateMachine, this.gameObject));
        }

        if (battleManager.actionCount >= 2)
        {
            battleManager.turnCount++; //successful inc.
            Debug.Log("turnCount: " + battleManager.turnCount); //success!

            //Doesn't reference IEnum TurnCounter here? At least not yet...
            if (battleManager.turnCount % 2 == 1) //odd number, it's the playerTurn
            {
                battleManager.playerTurn = true;
                battleManager.enemyTurn = false;
            }

            if (battleManager.turnCount % 2 == 0) //even number, it's the enemyTurn
            {
                battleManager.playerTurn = false;
                battleManager.enemyTurn = true;
            }

            Debug.Log("PT: " + battleManager.playerTurn + ", ET: " + battleManager.enemyTurn); //PT: TRUE, ET: FALSE, NOT REGISTERING TURNCOUNT CHANGE YET...
            //this.battleStateMachine.UpdateState(); //causes Unity to crash

            battleManager.actionCount = 0; //success!

            if (battleManager.playerTurn == true && battleManager.enemyTurn == false && battleManager.actionCount < 2)
            {
                Debug.Log("Player turn started-->ActionSelect"); //success
                this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.gameObject));
            }

            if (battleManager.playerTurn == false && battleManager.enemyTurn == true && battleManager.actionCount < 2)
            {
                Debug.Log("Enemy turn started-->AIState"); //success
                this.battleStateMachine.ChangeState(new AIState(battleStateMachine, this.gameObject));
            }
            
            
        }

    /*
    if (battleManager.playerTurn != false)
    {
        this.battleStateMachine.UpdateState();
    }
    battleManager.playerTurn = false;

    if (battleManager.enemyTurn != false)
    {
        this.battleStateMachine.UpdateState();
    }
    battleManager.enemyTurn = false;
    */
        #endregion
    }

    public void Exit()
    {
        Debug.Log("Exiting CharacterState state"); //success

        actionCount = 0;
        hasActiveAction = false;
        AI_Target = null;
    }
}
#region Original Code
/*
public class CharacterState : IState
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();
    private GameObject gameObject;

    BattleManager battleManager;

    public CharacterState(BattleStateMachine battleStateMachine, GameObject gameObject)
    {
        this.battleStateMachine = battleStateMachine;
        this.gameObject = gameObject;
    }

    /*
    private GameObject character;
    public CharacterState(BattleStateMachine battleStateMachine, GameObject a_Character)
    {
       this.battleStateMachine = battleStateMachine;
       this.character = a_Character;
    }
    //

    public void Enter()
    {
        Debug.Log("Entering CharacterState state");
        //Debug.Log("CS: " + battleStateMachine.currentState); //success!
        //Debug.Log("PS: " + battleStateMachine.previousState); //success!
        battleManager = BattleManager.Instance;
        this.battleStateMachine.UpdateState();
    }

    public void Execute()
    {
        Debug.Log("Executing CharacterState state");
        if (battleManager.playerTurn == true && battleManager.enemyTurn == false && battleManager.actionCount < 2)
        {
            Debug.Log("Player turn started-->ActionSelect"); //success
            this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.gameObject));
        }

        if (battleManager.playerTurn == false && battleManager.enemyTurn == true && battleManager.actionCount < 2)
        {
            Debug.Log("Enemy turn started-->AIState"); //success
            this.battleStateMachine.ChangeState(new AIState(battleStateMachine, this.gameObject));
        }

        if (battleManager.actionCount >= 2)
        {
            battleManager.turnCount++; //successful inc.
            Debug.Log("turnCount: " + battleManager.turnCount); //success!

            //Doesn't reference IEnum TurnCounter here? At least not yet...
            if (battleManager.turnCount % 2 == 1) //odd number, it's the playerTurn
            {
                battleManager.playerTurn = true;
                battleManager.enemyTurn = false;
            }

            if (battleManager.turnCount % 2 == 0) //even number, it's the enemyTurn
            {
                battleManager.playerTurn = false;
                battleManager.enemyTurn = true;
            }

            Debug.Log("PT: " + battleManager.playerTurn + ", ET: " + battleManager.enemyTurn); //PT: TRUE, ET: FALSE, NOT REGISTERING TURNCOUNT CHANGE YET...
            //this.battleStateMachine.UpdateState(); //causes Unity to crash

            battleManager.actionCount = 0; //success!

            if (battleManager.playerTurn == true && battleManager.enemyTurn == false && battleManager.actionCount < 2)
            {
                Debug.Log("Player turn started-->ActionSelect"); //success
                this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.gameObject));
            }

            if (battleManager.playerTurn == false && battleManager.enemyTurn == true && battleManager.actionCount < 2)
            {
                Debug.Log("Enemy turn started-->AIState"); //success
                this.battleStateMachine.ChangeState(new AIState(battleStateMachine, this.gameObject));
            }
        }

        /*
        if (battleManager.playerTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }
        battleManager.playerTurn = false;

        if (battleManager.enemyTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }
        battleManager.enemyTurn = false;
        //
    }

    public void Exit()
    {
        Debug.Log("Exiting CharacterState state"); //success
    }
}
*/
#endregion