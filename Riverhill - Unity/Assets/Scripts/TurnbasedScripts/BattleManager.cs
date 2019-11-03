﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    /* Use if ever only one instance of battle manager (battle stats)
     * Can be referenced using BattleManager.Instance (BattleStats.Instance).***
     * 
     * A good reference is in TileManager for the singleton and ActorController for the application
     */
    #region Singleton
    private static BattleManager _instance;
    public static BattleManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    // Removed for scaleability
    /*
    //referring to stats
    public PlayerStats player;
    public EnemyStats enemy;
    */

    public GameObject prefab_CharacterUI;
    CharacterUI characterUI;
    GameObject characterUI_Object;

    public List<CharacterStats> characterStats;
    public List<CharacterState> characterStates;

    public int turnCount = 0;

    public Text turnText;

    // No longer needed - Characters are told when their turn is, not waiting for their turn
    /* 
    public bool playerTurn = false;
    public bool enemyTurn = false;
    */

    public bool nextCharacter = false;

    private BattleStateMachine battleStateMachine;

    // Start is called before the first frame update
    void Start()
    {
        battleStateMachine = new BattleStateMachine();
        characterStates = new List<CharacterState>();

        // Create Characters
        for (int i = 0; i < characterStats.Count; i++)
        {
            CreateCharacter(characterStats[i]);
        }
        Debug.Log("***Characters Created***");

        //TODO: Character UI Generate
        GenerateCharacterUI();
        Debug.Log("***CharacterUI Created***");

        turnCount++; //inc. turn count on start, starts @ 1, player turn
        //Debug.Log(turnCount);

        StartCoroutine(UpdateTiles());
        StartCoroutine(TurnSequence());
    }

    IEnumerator TurnSequence()
    {
        Debug.Log("Turn Sequence Started");
        while (true)
        {
            for (int i = 0; i < characterStats.Count; i++)
            {
                //Debug.Log("");
                Debug.Log("Start " + characterStates[i].character.name + "'s Turn");
                turnText.text = "Turn: " + characterStates[i].character.name;
                yield return new WaitForSeconds(1f);
                battleStateMachine.ChangeState(characterStates[i]);
                characterUI.AssignNewCharacter(characterStates[i]);

                if (characterStates[i].characterStats.isEnemy)
                {
                    characterUI.gameObject.SetActive(false);
                }
                else
                {
                    characterUI.gameObject.SetActive(true);
                }

                // Was waiting for nextCharacter, but nothing was updating the state INORDER to get nextCharacter
                //yield return new WaitUntil(() => nextCharacter == true); // WaitUntil nextCharacter == true

                while (nextCharacter == false)
                {
                    battleStateMachine.UpdateState();
                    //yield return new WaitForSeconds(0.0001f);
                    yield return new WaitForEndOfFrame();
                }
                nextCharacter = false;

                characterUI.AssignNewCharacter(null);
                Debug.Log("End " + characterStates[i].character.name + "'s Turn");
            }
            nextCharacter = false;
            turnCount++;
            yield return new WaitForEndOfFrame();
        }

        //yield return null;
    }

    public void CreateCharacter(CharacterStats a_CharacterStat)
    {
        // Creates CharacterState and Assigns GameObject
        CharacterState a_CState = new CharacterState(a_CharacterStat.gameObject);
        a_CharacterStat.Name = a_CharacterStat.gameObject.name; // TODO: Is CharacterStats.name necessary?
        characterStates.Add(a_CState);
        Debug.Log("Character Created: " + characterStates[characterStates.Count - 1].characterStats.Name);
    }

    /// <summary>
    /// Generates the UI characters can use, and will be able to transfer between characters
    /// </summary>
    private void GenerateCharacterUI()
    {
        characterUI_Object = Instantiate(prefab_CharacterUI);
        characterUI = characterUI_Object.GetComponent<CharacterUI>();
    }

    IEnumerator UpdateTiles()
    {
        while (true)
        {
            for (int i = 0; i < characterStates.Count; i++)
            {
                // Update Tile location via TileManager
                if (characterStates[i].localTile != TileManager.Instance.TileFromWorldPosition(characterStates[i].characterStats.gameObject.transform.position))
                {
                    if (characterStates[i].localTile != null)
                    {
                        characterStates[i].localTile.hasCharacter = false;
                        characterStates[i].localTile.character = null;
                    }

                    //Debug.Log("UpdatingTile");
                    characterStates[i].localTile = TileManager.Instance.TileFromWorldPosition(characterStates[i].characterStats.gameObject.transform.position);

                    characterStates[i].localTile.hasCharacter = true;
                    characterStates[i].localTile.character = characterStates[i];
                }
            }
            yield return new WaitForSeconds(1f / GameSettings.Instance.FramerateTarget);
        }
    }

    public void AttackCharacter(CharacterState attacker, CharacterState defender)
    {
        defender.characterStats.CurrentHP = defender.characterStats.CurrentHP - (attacker.characterStats.attack /*+attacker.characterStats.MODIFIERS- defender.characterStats.MODIFIERS*/ );
    }
}

#region Original Code
/*
public class BattleManager : MonoBehaviour
{
    /* Use if ever only one instance of battle manager (battle stats)
     * Can be referenced using BattleManager.Instance (BattleStats.Instance).***
     * 
     * A good reference is in TileManager for the singleton and ActorController for the application
     //

#region Singleton
private static BattleManager _instance;
public static BattleManager Instance { get { return _instance; } }

private void Awake()
{
    if (_instance != null && _instance != this)
    {
        Destroy(this.gameObject);
    }
    else
    {
        _instance = this;
    }
}
#endregion

//referring to stats
public PlayerStats player;
public EnemyStats enemy;

//keeping track of player vs. enemy turn (odd #s = player turn, even #s = enemy turn)
public int turnCount = 0;
public bool playerTurn = false;
public bool enemyTurn = false;

//keeping track of / limiting multiple actions per turn (we're limiting to 2 for now?)
public int actionCount = 0;
//UI button options
public bool moveSelected = false;
public bool attackSelected = false;
public bool specialSelected = false;
public bool endTurnSelected = false; //essentially a pass option

#region TurnCounter
IEnumerator TurnCounter()
{
    while (true)
    {
        //       Maybe via array/lists?
        if (turnCount % 2 == 1) //odd number, it's the playerTurn
        {
            playerTurn = true;
            enemyTurn = false;
        }

        if (turnCount % 2 == 0) //even number, it's the enemyTurn
        {
            playerTurn = false;
            enemyTurn = true;
        }

        Debug.Log("Turn: " + turnCount + ", PT: " + playerTurn + ", ET: " + enemyTurn); //success, keep showing for now
        yield return new WaitForFixedUpdate();
    }
}
#endregion

private BattleStateMachine battleStateMachine = new BattleStateMachine();

// Start is called before the first frame update
void Start()
{
    player.playerCurrentHP = player.playerBaseHP;
    enemy.enemyCurrentHP = enemy.enemyBaseHP;

    turnCount++; //inc. turn count on start, starts @ 1, player turn
                 //Debug.Log(turnCount);
    StartCoroutine(TurnCounter());

    this.battleStateMachine.ChangeState(new CharacterState(battleStateMachine, this.gameObject)); //success!
}

public void Moving()
{
    //have to limit UpdateState(); cause each button would Update the same state
    //POSSIBLY: disable buttons while in state?
    moveSelected = true;
    //Debug.Log("MS: " + moveSelected); //success!
    this.battleStateMachine.UpdateState();
}

public void Attacking()
{
    //have to limit UpdateState(); cause each button would Update the same state
    //see POSSIBLY...
    attackSelected = true;
    //Debug.Log("AS: " + attackSelected); //success!
    this.battleStateMachine.UpdateState();
}

public void Special()
{
    //have to limit UpdateState(); cause each button would Update the same state
    //see POSSIBLY...
    specialSelected = true;
    //Debug.Log("SS: " + specialSelected); //success!
    this.battleStateMachine.UpdateState();
}

public void EndTurn()
{
    endTurnSelected = true;
    Debug.Log("ET: " + endTurnSelected);
    //this.battleStateMachine.UpdateState(); //Unity crashes?
    ResetSelected();
}

private void ResetSelected()
{
    moveSelected = false;
    attackSelected = false;
    specialSelected = false;
}
}
*/
#endregion