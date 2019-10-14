using System.Collections;
using System.Collections.Generic; //using list
using UnityEngine;
using UnityEngine.UI; //using Unity UI

public class BattleStateMachine //: MonoBehaviour
{
    //general state machine (it will switch b/w states)
    //now we can mess around with creating different states
    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState) //exiting one state (currently in) and entering a new one
    {
        if (this.currentState != null)
        {
            this.currentState.Exit();
        }

        this.previousState = this.currentState;
        this.currentState = newState;
        currentState.Enter();
    }

    public void UpdateState() //executing content of the state you're currently in
    {
        var runningState = this.currentState;

        if (runningState != null)
        {
            this.currentState.Execute();
        }
    }

    public void RevertToPreviousState() //exit current, set current to prev., enter that previous state
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