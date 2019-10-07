using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();
    BattleStats battleStats;

    public EnemyStats enemy;

    private void Start()
    {
        //Debug.Log("ESM start");
        enemy.enemyCurrentHP = enemy.enemyBaseHP; //working correctly, can see stats in inspector

        battleStats = BattleStats.Instance;
    }

    private void Update()
    {

        //Debug.Log("ESM start, enemy turn: " + battleStats.enemyTurn); //running, but enemyTurn is false

        if (battleStats.enemyTurn == true)
        {
            //Debug.Log("Enemy turn (SM) started");
            this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.gameObject));
            //Debug.Log("Enemy AS");

        }

        if (battleStats.enemyTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }

        battleStats.enemyTurn = false;
    }
}