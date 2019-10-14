using System.Collections;
using System.Collections.Generic; //using list
using UnityEngine;
using UnityEngine.UI; //using Unity UI

public class BattleStateMachine //: MonoBehaviour
{
    //this is our general state machine (it will switch b/w states)
    //now we can mess around with creating different states
    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState)
    {
        if (this.currentState != null) //if you are already in a state
        {
            this.currentState.Exit(); //exit said state
        }

        this.previousState = this.currentState; //state just exited is stored as the previous state
        this.currentState = newState; //current state is updated to the new state
        currentState.Enter(); //enter new state
    }

    public void UpdateState()
    {
        var runningState = this.currentState;

        if (runningState != null) //if you are currently in a state
        {
            this.currentState.Execute(); //execute content of said state
        }
    }

    public bool IsInState(IState state)
    {
        if (currentState == state)
        {
            return true;
        }
        else return false;
    }

    public void RevertToPreviousState()
    {
        this.currentState.Exit(); //exit current state
        this.currentState = this.previousState; //set the current state as whatever the previous state was
        this.currentState.Enter(); //enter the new current state (aka the previous state, set to the current state...)
    }
}