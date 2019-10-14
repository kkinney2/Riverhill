using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : IState
{
    //keep track of player char vs. enemy char turn
    private bool isPlayerTurn = false; //player being controlled when true, will call for ActionSelect
    private bool isEnemyTurn = false; //enemy being controlled when true, will call for AIState

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }

    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */

}
