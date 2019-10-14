using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    /* Use if ever only one instance of battle stats
     * Can be referenced using BattleStats.Instance.*** 
     * 
     * A good reference is in TileManager for the singleton and ActorController for the application
     * 
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

    //helps keep track of player vs. enemy turn (odd #s = player turn, even #s = enemy turn)
    public int turnCount = 0;

    //helps in allowing for 2 actions per turn, keeping track of # of actions performed
    public int actionCount = 0;

    public bool playerTurn = false;
    public bool enemyTurn = false;

    public bool moveSelected = false;
    public bool attackSelected = false;
    public bool specialSelected = false;
    public bool endTurnSelected = false; //pass

    // Start is called before the first frame update
    void Start()
    {
        turnCount++; //inc. turn count

        StartCoroutine(TurnCounter());
    }

    #region TurnCounter
    /*
    void Update()
    {
        //move stuff from start down here?
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

        //Debug.Log("Turn: " + turnCount);
        //Debug.Log("PT: " + playerTurn + ", ET: " + enemyTurn);
        Debug.Log("Turn: " + turnCount + ", P: " + playerTurn + ", E: " + enemyTurn);
    }
    */

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

            //Debug.Log("Turn: " + turnCount);
            //Debug.Log("PT: " + playerTurn + ", ET: " + enemyTurn);
            Debug.Log("Turn: " + turnCount + ", P: " + playerTurn + ", E: " + enemyTurn);

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion

    public void Moving()
    {
        moveSelected = true;
        Debug.Log("MS: " + moveSelected);
        //this seems to be working
    }

    public void Attacking()
    {
        attackSelected = true;
        Debug.Log("AS: " + attackSelected);
        //this seems to be working
    }

    public void Special()
    {
        specialSelected = true;
        Debug.Log("DS: " + specialSelected);
        //this seems to be working
    }

    public void EndTurn()
    {
        endTurnSelected = true;
        ResetSelected();
        Debug.Log("ET: " + endTurnSelected);

        //this seems to be working
        //TODO: now time to add in turnCount++; and switching to enemy turn...
        turnCount++;
        //still need to leave AS state
    }

    private void ResetSelected()
    {
        moveSelected = false;
        attackSelected = false;
        specialSelected = false;
    }
}
