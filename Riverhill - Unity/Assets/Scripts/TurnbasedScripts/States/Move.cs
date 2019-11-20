using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    CharacterState characterState;
    BattleStateMachine characterStateMachine;
    public BattleStateMachine battleStateMachine;

    IState state_CharacterAction;

    BattleManager battleManager;

    CharacterPathfinding pathfinder;

    GameObject tileHighlight_Pos;
    GameObject tileHighlight_Neg;

    Vector2 previousPos;

    public Move(CharacterState a_CharacterState, BattleStateMachine a_BattleStateMachine)
    {
        this.characterState = a_CharacterState;
        this.characterStateMachine = a_BattleStateMachine;

        pathfinder = characterState.pathfinder;
    }

    public void Enter()
    {
        Debug.Log("Entering move state"); //success!
        battleManager = BattleManager.Instance;

        characterState.hasActiveAction = true;


        if (!characterState.characterStats.isEnemy)
        {
            for (int i = 0; i < TileManager.Instance.tilesList.Count; i++)
            {
                //TileManager.Instance.tilesList[i].GetComponent<Tile>();

                pathfinder.FindPath(TileManager.Instance.tilesList[i].transform.position);

                #region Tile Highlighting
                if (pathfinder.path.Count >= pathfinder.range.x) // If the path is longer than or equal to the min range
                {
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(true);
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);

                    // Will toggle negative(red) when cursor is hovering

                    if (pathfinder.path.Count > pathfinder.range.y || TileManager.Instance.tilesList[i].GetComponent<Tile>().hasCharacter == true) // If the path is shorter than the max range OR if it has a character
                    {
                        TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(false);
                        TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(true);
                    }

                }
                else
                {
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(false);
                    TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);
                }
                #endregion
            }


            pathfinder.Move();
        }
        else
        {
            pathfinder.FindPath(characterState.AI_Target.characterStats.gameObject.transform.position);
            pathfinder.MoveAlongPath();
        }

        previousPos = new Vector2(characterState.characterStats.gameObject.transform.position.x, characterState.characterStats.gameObject.transform.position.y);
    }

    public void Execute()
    {
        if (previousPos != new Vector2(characterState.characterStats.gameObject.transform.position.x, characterState.characterStats.gameObject.transform.position.y))
        {
            characterState.characterStats.IsWalking(true);
        }
        else
        {
            characterState.characterStats.IsWalking(false);
        }

        previousPos = new Vector2(characterState.characterStats.gameObject.transform.position.x, characterState.characterStats.gameObject.transform.position.y);

        // TODO: Moving user input to here so that tile selection can be toggled here
        /*
        Vector3 worldFromScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("WorldFromScreen: " + worldFromScreen);

        RaycastHit2D hit = Physics2D.Raycast(worldFromScreen, Camera.main.transform.TransformDirection(Vector3.forward), 100);

        if (hit.collider != null)
        {
            Tile a_Tile = TileManager.Instance.GetTileFromWorldPosition(worldFromScreen);

            // If the tile does not have a character
            if (!a_Tile.hasCharacter)
            {
                // TODO: Path Highlighting: This can wait until after alpha
                // a_Tile.tileHighlight_Positive.SetActive(false);
                //a_Tile.tileHighlight_Negative.SetActive(true);
                // Then path find
                Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(hit.point.x, hit.point.y, 0)));
                Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);



                if (worldToCell != null || testPoint != null)
                {
                    pathfinder.FindPath(testPoint);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (pathfinder.path.Count != 0 && pathfinder.path.Count >= pathfinder.range.x && pathfinder.path.Count <= pathfinder.range.y)
                    {
                        pathfinder.MoveAlongPath();
                    }
                }
            }
        }
        */
        //Debug.Log("Move_Execute");
        if (pathfinder.isDone == true)
        {
            Debug.Log("Move isDone");
            pathfinder.isDone = false;
            characterState.actionCount++; //inc. actionCount, by one to allow for multi-move selections per turn //success!
            Debug.Log("actionCount: " + characterState.actionCount);
            characterStateMachine.ChangeState(characterState.state_Idle);
            Debug.Log("actionCount: " + characterState.actionCount);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Move State");

        characterState.characterStats.IsWalking(false);

        if (!characterState.characterStats.isEnemy)
        {
            for (int i = 0; i < TileManager.Instance.tilesList.Count; i++)
            {
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Positive.SetActive(false);
                TileManager.Instance.tilesList[i].GetComponent<Tile>().tileHighlight_Negative.SetActive(false);
            }
        }

        characterState.hasActiveAction = false;
    }
}
