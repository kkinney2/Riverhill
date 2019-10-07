using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();

    private void Start()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();

        if (battleStats.enemyTurn == true)
        {
            this.battleStateMachine.ChangeState(new ActionSelect());
            Debug.Log("Enemy AS");
        }
    }

    private void Update()
    {
        this.battleStateMachine.UpdateState();

        //may need to take some code from above?
    }
}