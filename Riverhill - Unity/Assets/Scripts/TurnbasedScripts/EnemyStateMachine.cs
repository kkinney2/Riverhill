using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public EnemyStats enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy.currentHP = enemy.baseHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
