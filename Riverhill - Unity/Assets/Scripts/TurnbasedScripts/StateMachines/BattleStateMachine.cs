using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine //: MonoBehaviour
{
    //general state machine (will switch b/w states)
    //now we can mess around w/ creating different states
    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState) //exiting one state (current state) and entering a new one
    {
        if (this.currentState != null)
        {
            this.currentState.Exit();
        }

        this.previousState = this.currentState;
        this.currentState = newState;
        currentState.Enter();
    }

    public void UpdateState() //executing content of the current state
    {
        var runningState = this.currentState;

        if (runningState != null)
        {
            this.currentState.Execute();
        }
    }

    public void RevertToPreviousState() //exit current state, set current state to prev. state, enter new current state (aka that previous state)
    {
        this.currentState.Exit(); 
        this.currentState = this.previousState;
        this.currentState.Enter();
    }

    public bool IsInState(IState state)
    {
        if (currentState == state)
        {
            return true;
        }
        else return false;
    }
}