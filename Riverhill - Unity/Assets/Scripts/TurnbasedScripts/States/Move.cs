using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;
    
    public Vector2Int range = new Vector2Int(1, 2);

    public List<Tile> path = new List<Tile>();

    public bool attack = true;
    bool isDone = false;

    public Move(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;
    }

    /*
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    private GameObject character;
    public Move(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }

    public ActorController acScript;
    */

    public void Enter()
    {
        Debug.Log("Entering move state"); //success!
        battleManager = BattleManager.Instance;
        characterState.actionCount++; //inc. actionCount, by one to allow for multi-move selections per turn //success!

        /* TODO: Correct Move functionality to work without coroutine?
         * OR create a helper function that does the work, like ActorController,
         * but is able to report when it has completed so the Move state can wait and 
         * then move on
         */
    }

    public void Execute()
    {
        Debug.Log("Executing move state, **ADD FUNC.**");
        if (isDone)
        {
            characterStateMachine.ChangeState(characterState.state_Idle);
        }

        /*
        Debug.Log("Executing Move");
        acScript.Move();
        
        battleManager.actionCount++;
        Debug.Log("Action count:" + battleManager.actionCount); //success
        battleManager.moveSelected = false; //returns to false correctly
        */
    }

    public void Exit()
    {
        Debug.Log("Exiting Move State");
        characterState.hasActiveAction = false;
        isDone = false;
    }


    #region Movement

    IEnumerator Move_Coroutine()
    {
        Debug.Log("Move Coroutine");

        bool hasPath = false;

        while (!hasPath)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Mouse Click");

                Vector3 worldFromScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("WorldFromScreen: " + worldFromScreen);

                RaycastHit2D hit = Physics2D.Raycast(worldFromScreen, Camera.main.transform.TransformDirection(Vector3.forward), 100);

                if (hit.collider != null)
                {
                    Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(hit.point.x, hit.point.y, 0)));
                    Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);

                    path = TileManager.Instance.FindPath(characterState.character.transform.position, testPoint);
                    if (path != null)
                    {
                        hasPath = true;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.001f);
        }

        Debug.Log("PathLength: " + path.Count);
        if (path.Count != 0 && path.Count >= range.x && path.Count <= range.y)
        {
            //StartCoroutine(MoveAlongPath());
        }

        yield break;
    }

    IEnumerator MoveAlongPath()
    {
        // Teleport to Position
        //transform.position = path[path.Count - 1].transform.position;

        // Travel towards Position
        for (int i = 0; i < path.Count; i++)
        {
            Transform target = path[i].transform;

            while (Vector3.Distance(characterState.character.transform.position, target.position) > 0.001f)
            {
                // Move our position a step closer to the target.
                float step = GameSettings.Instance.CharacterMoveSpeed * Time.deltaTime; // calculate distance to move
                characterState.character.transform.position = Vector3.MoveTowards(characterState.character.transform.position, target.position, step);

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }

    #endregion
}
