using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();

    private void Start()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();

        if (battleStats.playerTurn == true)
        {
            this.battleStateMachine.ChangeState(new ActionSelect());
            Debug.Log("Player AS");
        }
    }

    private void Update()
    {
        this.battleStateMachine.UpdateState();

        //may need to take some code from above?
    }
}