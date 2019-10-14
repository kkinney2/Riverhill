using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    //Merged into CharacterState

    /*
    private BattleStateMachine battleStateMachine = new BattleStateMachine();
    BattleManager battleManager;

    private void Start()
    {
        //Debug.Log("ESM start");
        //enemy.enemyCurrentHP = enemy.enemyBaseHP; //working correctly, can see stats in inspector

        battleManager = BattleManager.Instance;
    }

    private void Update()
    {
        //Debug.Log("ESM start, enemy turn: " + battleStats.enemyTurn);

        if (battleManager.enemyTurn == true)
        {
            //Debug.Log("Enemy turn started");
            this.battleStateMachine.ChangeState(new ActionSelect(battleStateMachine, this.gameObject));
            //Debug.Log("Enemy AS");
        }

        if (battleManager.enemyTurn != false)
        {
            this.battleStateMachine.UpdateState();
        }
        battleManager.enemyTurn = false;
    }
    */
}