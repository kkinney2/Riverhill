using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    public PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        player.currentHP = player.baseHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
