using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{
    CharacterState characterState;
    private BattleStateMachine characterStateMachine;

    BattleManager battleManager;

    CharacterPathfinding pathfinder;

    bool isDone = false;

    public Attack(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;

        pathfinder = characterState.pathfinder;
    }

    /*
    private BattleStateMachine battleStateMachine;
    BattleStateMachine owner;
    BattleManager battleManager;

    //private object gameObject;
    private GameObject character;
    public Attack(BattleStateMachine newOwner, GameObject a_Character)
    {
        this.owner = newOwner;
        character = a_Character;
    }
    */

    public void Enter()
    {
        Debug.Log("Entering attack state"); //success!
        battleManager = BattleManager.Instance;

        characterState.hasActiveAction = true;
    }

    public void Execute()
    {
        Debug.Log("Executing attack state, **ADD FUNC.**");
        //TODO: do attacking function here!
        if (characterState.characterStats.isEnemy)
        {
            // TODO: Enemy Attack
            battleManager.AttackCharacter(characterState, characterState.AI_Target);
            characterState.characterStats.IsAttacking();
            characterState.AI_Target.characterStats.WasHit();
            isDone = true;
        }
        else
        {
            Vector3 worldFromScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("WorldFromScreen: " + worldFromScreen);

            RaycastHit2D hit = Physics2D.Raycast(worldFromScreen, Camera.main.transform.TransformDirection(Vector3.forward), 100);

            if (hit.collider != null)
            {
                Tile a_Tile = TileManager.Instance.GetTileFromWorldPosition(worldFromScreen);

                if (a_Tile.hasCharacter)
                {
                    if (a_Tile.characterState.characterStats.isEnemy)
                    {
                        Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(hit.point.x, hit.point.y, 0)));
                        Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);

                        // Pathfind to them to determine distance
                        if (worldToCell != null || testPoint != null)
                        {
                            pathfinder.FindPath(testPoint);
                        }
                        // TODO: Input not registering -- Not checking enough?
                        if (pathfinder.path.Count == 1f && Input.GetMouseButtonUp(0)) // TODO: Script a attack range variable/Get from characterStats
                        {
                            battleManager.AttackCharacter(characterState, a_Tile.characterState);
                            isDone = true;
                        }
                    }
                }
            }
        }
        

        if (isDone)
        {
            characterState.actionCount = (characterState.actionCount + 2); //inc. actionCount, by two to avoid multi-attack selections per turn //success!
            characterStateMachine.ChangeState(characterState.state_Idle);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Attack State");
        characterState.hasActiveAction = false;
        isDone = false;
    }
}

