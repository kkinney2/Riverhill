using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{

    private BattleStateMachine battleStateMachine = new BattleStateMachine();

}

    /* OLD CODE --- will keep until everything is working properly (new state codes will use aspects from old code)
    
    //public EnemyStats enemy;

    public enum TurnState
    {
        PROCESSING, //waiting for battle-player turn to begin (5~ sec countdown)
        WAITING, //waiting / idle state
        //SELECTING, //selecting option state, no need for this state cause enemy AI will choose action... see below state
        CHOOSEACTION, //will set up better AI later...
        ACTION, //performing action state
        DEAD //death / defeated state
    }

    //public TurnState currentState;

    //for processing display
    //private float currentCooldown = 0.0f;
    //private float maxCooldown = 5.0f;

    //for enemy thinking time
    //private float currentThoughtTime = 0.0f;
    //private float maxThoughtTime = 5.0f;

    //Text p2TurnREADYText;

    //private BattleStateMachine battleSM; //connecting to Battle State Machine

    // Start is called before the first frame update
    void Start()
    {
        enemy.currentHP = enemy.baseHP;

        currentState = TurnState.WAITING; //waiting, idle state... enemy turn is not active until player ends their turn

        p2TurnREADYText = GameObject.Find("P2TurnREADYText").GetComponent<Text>();

        battleSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>(); //connecting to Battle State Machine

        GameObject thePlayer = GameObject.Find("Alyss");
        PlayerStateMachine playerSM = thePlayer.GetComponent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject thePlayer = GameObject.Find("Alyss");
        PlayerStateMachine playerSM = thePlayer.GetComponent<PlayerStateMachine>();

        Debug.Log("P2: " + currentState);

        switch (currentState)
        {
            case (TurnState.PROCESSING):
                DisplayProcessing();
                break;
            case (TurnState.WAITING):
                //idle state
                break;
            //case (TurnState.SELECTING):
                //break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                //DisplayThinking();
                currentState = TurnState.ACTION; //need to perform said action
                break;
            case (TurnState.ACTION):
                //need to perform said action
                Debug.Log("P2: Performs said action");
                p2TurnREADYText.text = "";
                currentState = TurnState.WAITING;
                //playerSM.currentState = PlayerStateMachine.TurnState.PROCESSING; //glitches right now

                //ENDING TURN BY CHOOSING AN ACTION AND ENDING TURN VIA UI BUTTON... both are having trouble switching to opposite states... issue with ChooseAction(); and/or BattleManager?

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
            p2TurnREADYText.text = "READY";
            currentState = TurnState.CHOOSEACTION; //choose action state, improve AI later on...
        }
    }

    void DisplayThinking()
    {
        currentThoughtTime = currentThoughtTime + Time.deltaTime;
        if (currentThoughtTime >= maxThoughtTime)
        {
            currentState = TurnState.ACTION;
        }
    }

    void ChooseAction()
    {
        //this function puts a certain action (in this case an attack) in a list, taking note of who is attacking and who is the target...
        TurnHandler myAttack = new TurnHandler();
        myAttack.Attacker = enemy.name;
        myAttack.attackerGO = this.gameObject;
        myAttack.attacksTarget = battleSM.playersInBattle[Random.Range(0, battleSM.playersInBattle.Count)]; //array-length, list-count
        battleSM.GetActions(myAttack); //above code for choosing an action, randomly attacking players
    }

    */
