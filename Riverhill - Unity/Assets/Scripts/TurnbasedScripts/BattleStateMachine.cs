using System.Collections;
using System.Collections.Generic; //use list
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{



    public enum PerformAction
    {
        WAITING,
        TAKEACTION,
        PERFORMACTION
     }

    public PerformAction performStates;

    public List<TurnHandler> performList = new List<TurnHandler>();
    public List<GameObject> playersInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        performStates = PerformAction.WAITING;

        playersInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player")); //finding all players + adding to list
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); //finding all enemies + adding to list
    }

    // Update is called once per frame
    void Update()
    {
        switch (performStates)
        {
            case (PerformAction.WAITING):
                break;
            case (PerformAction.TAKEACTION):
                break;
            case (PerformAction.PERFORMACTION):
                break;
        }
    }

    public void GetActions(TurnHandler input)
    {
        performList.Add(input); 
    }
}
