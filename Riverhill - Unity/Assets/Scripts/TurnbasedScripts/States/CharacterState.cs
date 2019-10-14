using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    //keep track of player char vs. enemy char turn
    private bool isPlayerTurn = false; //player being controlled when true, call for ActionSelect
    private bool isEnemyTurn = false; //enemy being controlled when true, call for AIState

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
