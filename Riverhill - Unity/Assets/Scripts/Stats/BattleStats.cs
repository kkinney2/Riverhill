using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStats : MonoBehaviour
{
    //helps keep track of player vs. enemy turn (odd #s = player turn, even #s = enemy turn)
    public int turnCount = 0;
    //helps in allowing for 2 actions per turn, keeping track of # of actions performed
    public int actionCount = 0;

    public bool playerTurn = false;
    public bool enemyTurn = false;

    public bool moveSelected = false;
    public bool attackSelected = false;
    public bool defendSelected = false;
    public bool endTurnSelected = false; //pass

    // Start is called before the first frame update
    void Start()
    {
        turnCount++; //inc. turn count
    }

    // Update is called once per frame
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

    public void Defending()
    {
        defendSelected = true;
        Debug.Log("DS: " + defendSelected);
        //this seems to be working
    }

    public void EndTurn()
    {
        endTurnSelected = true;
        Debug.Log("ET: " + endTurnSelected);
        //this seems to be working
        //TODO: now time to add in turnCount++; and switching to enemy turn...
        turnCount++;
        //still need to leave AS state
    }
}
