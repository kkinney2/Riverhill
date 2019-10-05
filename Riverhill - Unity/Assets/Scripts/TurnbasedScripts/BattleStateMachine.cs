using System.Collections;
using System.Collections.Generic; //using list
using UnityEngine;
using UnityEngine.UI; //using UI

public class BattleStateMachine : MonoBehaviour
{

    //this is our general state machine (will switch b/w states)
    //now we can mess around with different creating different states
    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState)
    {
        if (this.currentState != null)
        {
            this.currentState.Exit();
        }

        this.previousState = this.currentState;
        this.currentState = newState;
    }

    public void UpdateState()
    {
        var runningState = this.currentState;

        if (runningState != null)
        {
            this.currentState.Execute();
        }
    }

    public void RevertToPreviousState()
    {
        this.currentState.Exit();
        this.currentState = this.previousState;
        this.previousState.Enter();
    }

}

    //this is our general state machine (will switch b/w states)
    //now we can mess around with different creating different states

    /* OLD CODE --- will keep until everything is working properly (new state codes will use aspects from old code)
    
    public PlayerStats player;
    public EnemyStats enemy;

    private bool isPlayerTurn = true; //starts on player's turn, player turn active
    private bool isEnemyTurn = false; //enemy turn inactive

    public enum PlayerTurnState
    {
        PROCESSING, //5 sec countdown, then activate player turn
        WAITING, //idle state
        SELECTING, //selecting option state
        ACTION, //performing action state
        DEAD //death / defeated state
    }

    public enum EnemyTurnState
    {
        PROCESSING, //5 sec countdown, then activate player turn
        WAITING, //idle state
        //SELECTING, //selecting option state, no need for enemy to have this state? cause enemy AI will choose their action... see below state...
        CHOOSEACTION, //very basic, will set up better AI later...
        ACTION, //performing action state
        DEAD //death / defeated state
    }

    public enum TurnState
    {
        PROCESSING, //5 sec countdown, then activate player turn
        WAITING, //idle state
        SELECTING, //selecting option state
        ACTION, //performing action state
        DEAD //death / defeated state
    }

    public enum PerformAction
    {
        WAITING,
        TAKEACTION,
        PERFORMACTION
     }

    private bool isPlayerTurn = true; //starts on player's turn, player turn active
    //public PlayerTurnState playerCurrentState;

    private bool isEnemyTurn = false; //enemy turn inactive
    //public EnemyTurnState enemyCurrentState;

    //public TurnState currentState;
    //public PerformAction performStates;

    //TurnHandler used for enemy/AI turn/actions
    public List<TurnHandler> performList = new List<TurnHandler>();
    public List<GameObject> playersInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();

    //for processing time
    private float currentCooldown = 0.0f;
    private float maxCooldown = 5.0f;

    //for enemy thinking time
    private float currentThoughtTime = 0.0f;
    private float maxThoughtTime = 5.0f;

    Text p1TurnREADYText;
    Text p2TurnREADYText;

    // Start is called before the first frame update
    void Start()
    {
        performStates = PerformAction.WAITING;

        if(isPlayerTurn == true)
        {
            playerCurrentState = PlayerTurnState.PROCESSING; //5 sec countdown, then activate player turn
        }

        if (isEnemyTurn == true)
        {
            enemyCurrentState = EnemyTurnState.PROCESSING; //5 sec countdown, then activate player turn
        }
        
        playersInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player")); //finding all players + adding to list
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); //finding all enemies + adding to list
    }

    // Update is called once per frame
    void Update()
    {
        switch (performStates)
        {
            case (PerformAction.WAITING):
                break;
            case (PerformAction.TAKEACTION):
                break;
            case (PerformAction.PERFORMACTION):
                break;
        }
    }

    public void GetActions(TurnHandler input)
    {
        performList.Add(input); 
    }

    */
