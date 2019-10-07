using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();

    private void Start()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();

        if (battleStats.playerTurn == true)
        {
            this.battleStateMachine.ChangeState(new ActionSelect());
        }
    }

    private void Update()
    {
        this.battleStateMachine.UpdateState();

        //may need to take some code from above?
    }
}



    /* OLD CODE --- will keep until everything is working properly (new state codes will use aspects from old code)
    
    //public PlayerStats player;

    public enum TurnState
    {
        PROCESSING, //waiting for battle-player turn to begin (5~ sec countdown)
        WAITING, //waiting / idle state
        SELECTING, //selecting option state
        ACTION, //performing action state
        DEAD //death / defeated state
    }

    //public TurnState currentState;

    //for processing display
    //private float currentCooldown = 0.0f;
    //private float maxCooldown = 5.0f;

    //Text p1TurnREADYText;

    //private bool optionSelected = false;
    private bool moveSelected = false;
    private bool attackSelected = false;
    private bool defendSelected = false;

    private bool endedTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        player.currentHP = player.baseHP;

        currentState = TurnState.PROCESSING; //waiting for battle-player turn to begin (5~ sec countdown)

        p1TurnREADYText = GameObject.Find("P1TurnREADYText").GetComponent<Text>();

        GameObject theEnemy = GameObject.Find("Alyss_BAD");
        EnemyStateMachine enemySM = theEnemy.GetComponent<EnemyStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject theEnemy = GameObject.Find("Alyss_BAD");
        EnemyStateMachine enemySM = theEnemy.GetComponent<EnemyStateMachine>();
        
        Debug.Log("P1: " + currentState);

        switch (currentState)
        {
            case (TurnState.PROCESSING):
                DisplayProcessing();
                break;
            case (TurnState.WAITING):
                //idle state
                break;
            case (TurnState.SELECTING):
                SelectingOptions();
                break;
            case (TurnState.ACTION):
                //need to perform said action
                Debug.Log("P1: Performs said action");
                p1TurnREADYText.text = "";
                currentState = TurnState.WAITING;
                enemySM.currentState = EnemyStateMachine.TurnState.PROCESSING;
                break;
            case (TurnState.DEAD):
                break;
        }
    }

    void DisplayProcessing()
    {
        currentCooldown = currentCooldown + Time.deltaTime;
        if (currentCooldown >= maxCooldown)
        {
            p1TurnREADYText.text = "READY";
            currentState = TurnState.SELECTING; //state for selecting option
        }
    }

    public void SelectingOptions()
    {
        GameObject theEnemy = GameObject.Find("Alyss_BAD");
        EnemyStateMachine enemySM = theEnemy.GetComponent<EnemyStateMachine>();

        if (moveSelected == true)
        {
            currentState = TurnState.ACTION; //state for performing action
            //need to perform said action
        }
        else if (attackSelected == true)
        {
            currentState = TurnState.ACTION; //state for performing action
            //need to perform said action
        }
        else if (defendSelected == true)
        {
            currentState = TurnState.ACTION; //state for performing action
            //need to perform said action
        }
        else if (endedTurn == true)
        {
            p1TurnREADYText.text = "";
            currentState = TurnState.WAITING; //idle state //essentially the player's turn is not active
            enemySM.currentState = EnemyStateMachine.TurnState.PROCESSING;
        }
    }

    public void SelectingMove()
    {
        if (currentState == TurnState.SELECTING)
        {
            //If MOVE UI button pressed
            Debug.Log("P1: MOVE option selected");
            moveSelected = true;
        }
    }

    public void SelectingAttack()
    {
        if (currentState == TurnState.SELECTING)
        {
            //If ATTACK UI button pressed
            Debug.Log("P1: ATTACK option selected");
            attackSelected = true;
        }
    }

    public void SelectingDefend()
    {
        if (currentState == TurnState.SELECTING)
        {
            //If DEFEND UI button pressed
            Debug.Log("P1: DEFEND option selected");
            defendSelected = true;
        }
    }

    public void EndTurn()
    {
        if (currentState == TurnState.SELECTING)
        {
            //If END TURN UI button pressed
            Debug.Log("P1: END TURN option selected");
            endedTurn = true;
        }
    }

    */
