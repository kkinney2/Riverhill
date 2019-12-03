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
        if (!characterState.characterStats.isEnemy)
        {
            for (int i = 0; i < battleManager.characterStates_Enemy.Count; i++)
            {
                pathfinder.FindPath(battleManager.characterStates_Enemy[i].character.gameObject.transform.position);
                if (pathfinder.path.Count == characterState.characterStats.meleeAttackRange.y)
                {
                    TileManager.Instance.GetTileFromWorldPosition(battleManager.characterStates_Enemy[i].character.gameObject.transform.position).tileHighlight_Negative.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        
    }

    public void Execute()
    {
        Debug.Log("Executing attack state");
        if (characterState.characterStats.isEnemy)
        {
            // TODO: Enemy Attack
            battleManager.AttackCharacter(characterState, characterState.AI_Target);
            isDone = true;
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 worldFromScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("WorldFromScreen: " + worldFromScreen);

                RaycastHit2D hit = Physics2D.Raycast(worldFromScreen, Camera.main.transform.TransformDirection(Vector3.forward), 100);

                if (hit.collider != null)
                {
                    Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(hit.point.x, hit.point.y, 0)));
                    Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);

                    Tile a_Tile = TileManager.Instance.GetTileFromWorldPosition((new Vector3(hit.point.x, hit.point.y, 0)));
                    if (a_Tile.hasCharacter || a_Tile != null || a_Tile.characterState != null)
                    {
                        if (a_Tile.characterState.characterStats.isEnemy)
                        {
                            // Pathfind to them to determine distance
                            if (worldToCell != null || testPoint != null)
                            {
                                pathfinder.FindPath(testPoint);
                            }

                            if (pathfinder.path.Count <= characterState.characterStats.meleeAttackRange.y)
                            {
                                battleManager.AttackCharacter(characterState, a_Tile.characterState);
                                isDone = true;
                            }
                        }
                    }
                }
            }
            
        }
        

        if (isDone)
        {
            characterState.actionCount = (characterState.actionCount + battleManager.gameController.gameSettings.MaxActionCount); //inc. actionCount, by max to avoid multi-attack selections per turn //success!
            characterStateMachine.ChangeState(characterState.state_Idle);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Attack State");
        characterState.hasActiveAction = false;
        isDone = false;

        for (int i = 0; i < TileManager.Instance.tilesList.Count; i++)
        {
            TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

