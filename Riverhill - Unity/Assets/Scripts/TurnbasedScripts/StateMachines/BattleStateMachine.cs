using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine //: MonoBehaviour --- not needed
{
    //this is our general state machine (it will switch b/w states)
    public IState currentState; //public for testing purposes
    public IState previousState; //public for testing purposes

    public void ChangeState(IState newState) //change to a new state
    {
        if (this.currentState != null) //if already in a state
        {
            this.currentState.Exit(); //exit that state (the current state)
        }

        this.previousState = this.currentState;
        this.currentState = newState;
        currentState.Enter(); //and entering a new one (new current state)
        //Debug.Log("CS: " + currentState); //testing, success!
        //Debug.Log("PS: " + previousState); //testing, success!
    }

    public void UpdateState() //update / execute content of the current state
    {
        var runningState = this.currentState;

        if (runningState != null) //if already in a state
        {
            this.currentState.Execute(); //execute state's content
        }
    }

    /* NOT IN USE RN...
    public void RevertToPreviousState() //switch back to last state
    {
        this.currentState.Exit(); //exit the current state
        this.currentState = this.previousState; //set current state to whatever the previous state was
        this.currentState.Enter(); //enter new current (aka the previous) state
    }
    */

    public bool IsInState(IState state)
    {
        if (currentState == state)
        {
            return true;
        }
        else return false;
    }
}