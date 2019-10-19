using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    /* Use if ever only one instance of battle manager (battle stats)
     * Can be referenced using BattleManager.Instance (BattleStats.Instance).***
     * 
     * A good reference is in TileManager for the singleton and ActorController for the application
     */
    #region Singleton
    private static BattleManager _instance;
    public static BattleManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    // Removed for scaleability
    /*
    //referring to stats
    public PlayerStats player;
    public EnemyStats enemy;
    */

    public List<CharacterStats> characterStats;
    List<CharacterState> characterStates;

    //keeping track of player vs. enemy turn (odd #s = player turn, even #s = enemy turn)
    public int turnCount = 0;

    // No longer needed - Characters are told when their turn is, not waiting for their turn
    /* 
    public bool playerTurn = false;
    public bool enemyTurn = false;
    */

    //UI button options
    public bool moveSelected = false;
    public bool attackSelected = false;
    public bool specialSelected = false;
    public bool endTurnSelected = false; //essentially a pass option

    public bool nextCharacter = false;

    // Currently no longer used, only(?) purpose was to determine turn order
    #region TurnCounter
    /*
    IEnumerator TurnCounter()
    {
        while (true)
        {
            // TODO: Rework turn order for multiple characters
            //       Maybe via array/lists?
            if (turnCount % 2 == 1) //odd number, it's the playerTurn
            {
                playerTurn = true;
                enemyTurn = false;
            }

            if (turnCount % 2 == 0) //even number, it's the enemyTurn
            {
                playerTurn = false;
                enemyTurn = true;
            }

            Debug.Log("Turn: " + turnCount + ", PT: " + playerTurn + ", ET: " + enemyTurn); //success, keep showing for now
            yield return new WaitForFixedUpdate();
        }
    }
    */
    #endregion

    private BattleStateMachine battleStateMachine = new BattleStateMachine();

    // Start is called before the first frame update
    void Start()
    {
        // Assign characters' stats
        //          Or do they come as prefabs with the stats already?
        //          and then hand that off to the BattleManager?

        // TODO: Limits number of characters
        characterStates = new CharacterState[characterStats.Length];

        turnCount++; //inc. turn count on start, starts @ 1, player turn
        //Debug.Log(turnCount);

        // See TurnCounter Function
        //StartCoroutine(TurnCounter());

        //this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine)); //success!
    }

    IEnumerator TurnSequence()
    {
        while (true)
        {
            for (int i = 0; i < characterStates.Count; i++)
            {
                battleStateMachine.ChangeState(characterStates[i]);
                yield return new WaitUntil(() => nextCharacter == true);
                nextCharacter = false;
            }
            nextCharacter = false;
            turnCount++;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    public void Moving()
    {
        //have to limit UpdateState(); cause each button would Update the same state
        //POSSIBLY: disable buttons while in state?
        moveSelected = true;
        //Debug.Log("MS: " + moveSelected); //success!
        this.battleStateMachine.UpdateState();
    }

    public void Attacking()
    {
        //have to limit UpdateState(); cause each button would Update the same state
        //see POSSIBLY...
        attackSelected = true;
        //Debug.Log("AS: " + attackSelected); //success!
        this.battleStateMachine.UpdateState();
    }

    public void Special()
    {
        //have to limit UpdateState(); cause each button would Update the same state
        //see POSSIBLY...
        specialSelected = true;
        //Debug.Log("SS: " + specialSelected); //success!
        this.battleStateMachine.UpdateState();
    }

    public void EndTurn()
    {
        endTurnSelected = true;
        Debug.Log("ET: " + endTurnSelected);
        //this.battleStateMachine.UpdateState(); //Unity crashes?
        ResetSelected();
    }

    private void ResetSelected()
    {
        moveSelected = false;
        attackSelected = false;
        specialSelected = false;
    }

    public void CreateCharacter(string a_Name)
    {
        // Find correct characterStat
        for (int i = 0; i < characterStats.Length; i++)
        {
            if (characterStats[i].Name == a_Name)
            {
                // Creates CharacterState and Assigns GameObject
                CharacterState a_CState = new CharacterState(characterStats[i].gameObject);
                characterStates. = a_CState;
                break;
            }
        }
        
    }
}

#region Original Code
/*
public class BattleManager : MonoBehaviour
{
    /* Use if ever only one instance of battle manager (battle stats)
     * Can be referenced using BattleManager.Instance (BattleStats.Instance).***
     * 
     * A good reference is in TileManager for the singleton and ActorController for the application
     //

#region Singleton
private static BattleManager _instance;
public static BattleManager Instance { get { return _instance; } }

private void Awake()
{
    if (_instance != null && _instance != this)
    {
        Destroy(this.gameObject);
    }
    else
    {
        _instance = this;
    }
}
#endregion

//referring to stats
public PlayerStats player;
public EnemyStats enemy;

//keeping track of player vs. enemy turn (odd #s = player turn, even #s = enemy turn)
public int turnCount = 0;
public bool playerTurn = false;
public bool enemyTurn = false;

//keeping track of / limiting multiple actions per turn (we're limiting to 2 for now?)
public int actionCount = 0;
//UI button options
public bool moveSelected = false;
public bool attackSelected = false;
public bool specialSelected = false;
public bool endTurnSelected = false; //essentially a pass option

#region TurnCounter
IEnumerator TurnCounter()
{
    while (true)
    {
        // TODO: Rework turn order for multiple characters
        //       Maybe via array/lists?
        if (turnCount % 2 == 1) //odd number, it's the playerTurn
        {
            playerTurn = true;
            enemyTurn = false;
        }

        if (turnCount % 2 == 0) //even number, it's the enemyTurn
        {
            playerTurn = false;
            enemyTurn = true;
        }

        Debug.Log("Turn: " + turnCount + ", PT: " + playerTurn + ", ET: " + enemyTurn); //success, keep showing for now
        yield return new WaitForFixedUpdate();
    }
}
#endregion

private BattleStateMachine battleStateMachine = new BattleStateMachine();

// Start is called before the first frame update
void Start()
{
    player.playerCurrentHP = player.playerBaseHP;
    enemy.enemyCurrentHP = enemy.enemyBaseHP;

    turnCount++; //inc. turn count on start, starts @ 1, player turn
                 //Debug.Log(turnCount);
    StartCoroutine(TurnCounter());

    this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject)); //success!
}

public void Moving()
{
    //have to limit UpdateState(); cause each button would Update the same state
    //POSSIBLY: disable buttons while in state?
    moveSelected = true;
    //Debug.Log("MS: " + moveSelected); //success!
    this.battleStateMachine.UpdateState();
}

public void Attacking()
{
    //have to limit UpdateState(); cause each button would Update the same state
    //see POSSIBLY...
    attackSelected = true;
    //Debug.Log("AS: " + attackSelected); //success!
    this.battleStateMachine.UpdateState();
}

public void Special()
{
    //have to limit UpdateState(); cause each button would Update the same state
    //see POSSIBLY...
    specialSelected = true;
    //Debug.Log("SS: " + specialSelected); //success!
    this.battleStateMachine.UpdateState();
}

public void EndTurn()
{
    endTurnSelected = true;
    Debug.Log("ET: " + endTurnSelected);
    //this.battleStateMachine.UpdateState(); //Unity crashes?
    ResetSelected();
}

private void ResetSelected()
{
    moveSelected = false;
    attackSelected = false;
    specialSelected = false;
}
}
*/
#endregion