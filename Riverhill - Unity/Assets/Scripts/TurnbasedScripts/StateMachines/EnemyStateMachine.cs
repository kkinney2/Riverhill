using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine = new BattleStateMachine();

    public EnemyStats enemy;

    private void Start()
    {
        //Debug.Log("ESM start");
        enemy.enemyCurrentHP = enemy.enemyBaseHP; //working correctly, can see stats in inspector
    }

    private void Update()
    {
        GameObject battleManager = GameObject.Find("BattleManager");
        BattleStats battleStats = battleManager.GetComponent<BattleStats>();

        //Debug.Log("ESM start, enemy turn: " + battleStats.enemyTurn); //running, but enemyTurn is false

        if (battleStats.enemyTurn == true)
        {
            //Debug.Log("Enemy turn (SM) started");
            this.battleStateMachine.ChangeState(new ActionSelect());
            //Debug.Log("Enemy AS");
        }

        this.battleStateMachine.UpdateState();
    }
}