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

    //keeping track of player vs. enemy turn (odd #s = player turn, even #s = enemy turn)
    public int turnCount = 0;
    public bool playerTurn = false;
    public bool enemyTurn = false;

    //allow for 2 actions per turn, keeping track of # of actions performed
    public int actionCount = 0;
    //UI button options
    public bool moveSelected = false;
    public bool attackSelected = false;
    public bool specialSelected = false;
    public bool endTurnSelected = false; //pass option

    //referring to stats
    public PlayerStats player;
    public EnemyStats enemy;

    private BattleStateMachine battleStateMachine = new BattleStateMachine();

    // Start is called before the first frame update
    void Start()
    {
        player.playerCurrentHP = player.playerBaseHP;
        enemy.enemyCurrentHP = enemy.enemyBaseHP;

        turnCount++; //inc. turn count on start, starts @ 1, player turn
        StartCoroutine(TurnCounter());

        this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject));
    }

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

            Debug.Log("Turn: " + turnCount + ", P: " + playerTurn + ", E: " + enemyTurn); //success, keep showing for now
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion

    public void Moving()
    {
        moveSelected = true;
        this.battleStateMachine.UpdateState();
        //Debug.Log("MS: " + moveSelected);
    }

    public void Attacking()
    {
        attackSelected = true;
        this.battleStateMachine.UpdateState();
        //Debug.Log("AS: " + attackSelected);
    }

    public void Special()
    {
        specialSelected = true;
        this.battleStateMachine.UpdateState();
        //Debug.Log("SS: " + specialSelected);
    }

    public void EndTurn()
    {
        endTurnSelected = true;
        //this.battleStateMachine.UpdateState(); //Unity crashes?
        //Debug.Log("ET: " + endTurnSelected);
        ResetSelected();
    }

    private void ResetSelected()
    {
        moveSelected = false;
        attackSelected = false;
        specialSelected = false;
    }
}
