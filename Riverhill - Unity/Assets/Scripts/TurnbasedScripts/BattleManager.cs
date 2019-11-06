using System.Collections;
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

    public List<CharacterState> characterStates_Enemy;
    public List<CharacterState> characterStates_Player;

    public SpriteLayering spriteLayering;

    public Level[] levels;
    public int currentLevel = 0;
    public LevelConditions levelConditions;
    public bool isLevelLoaded = false;

    public int turnCount = 0;

    public Text turnText;

    bool hasPlayableCharacter = false;
    bool hasPlayableEnemy = false;

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
        characterStates_Enemy = new List<CharacterState>();
        characterStates_Player = new List<CharacterState>();

        levelConditions = GameSettings.Instance.gameObject.GetComponent<LevelConditions>();

        // Create Characters
        StartCoroutine(CreateCharacters());

        GenerateCharacterUI();
        Debug.Log("***CharacterUI Created***");

        turnCount++; //inc. turn count on start, starts @ 1, player turn
        //Debug.Log(turnCount);

        LoadNextLevel();

        StartCoroutine(UpdateTiles());
        StartCoroutine(TurnSequence());
    }

    IEnumerator CreateCharacters()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < characterStats.Count; i++)
        {
            CreateCharacter(characterStats[i]);
        }
        Debug.Log("***Characters Created***");
        yield break;
    }


    #region Old Turn Sequence
    /*
    IEnumerator TurnSequence()
    {
        levelConditions.levelName = environment.name;

        Debug.Log("Turn Sequence Started");
        while (true)
        {
            for (int i = 0; i < characterStats.Count; i++)
            {
                if (characterStats[i].CurrentHP <= 0f)
                {
                    hasPlayableCharacter = false;
                    continue;
                }
                else if (!characterStats[i].isEnemy)
                {
                    hasPlayableCharacter = true;
                }

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
                    //yield return new WaitForEndOfFrame(); // Works better for input
                    battleStateMachine.UpdateState();
                    yield return new WaitForSeconds(0.0001f);
                    //yield return new WaitForEndOfFrame();
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
    */
    #endregion

    IEnumerator TurnSequence()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => characterStates_Player[characterStates_Player.Count - 1].characterStats.CurrentHP > 0); // WaitUntil the last character has their health set
        yield return new WaitUntil(() => isLevelLoaded == true);
        levelConditions.levelName = levels[currentLevel - 1].name;

        Debug.Log("Turn Sequence Started");
        while (true)
        {
            #region Player Turn
            Debug.Log("Player count: " + characterStates_Player.Count);
            for (int i = 0; i < characterStates_Player.Count; i++)
            {
                if (characterStates_Player[i].characterStats.CurrentHP <= 0f)
                {
                    hasPlayableCharacter = false;
                    continue;
                }
                else
                {
                    hasPlayableCharacter = true;
                }

                Debug.Log("Start " + characterStates_Player[i].character.name + "'s Turn");
                turnText.text = "Turn: " + characterStates_Player[i].character.name;
                yield return new WaitForSeconds(1f);

                battleStateMachine.ChangeState(characterStates_Player[i]);

                characterUI.AssignNewCharacter(characterStates_Player[i]);
                characterUI.gameObject.SetActive(true);

                while (nextCharacter == false)
                {
                    //yield return new WaitForEndOfFrame(); // Works better for input
                    battleStateMachine.UpdateState();
                    yield return new WaitForSeconds(0.0001f);
                    //yield return new WaitForEndOfFrame();
                }
                nextCharacter = false;

                characterUI.gameObject.SetActive(false);
                characterUI.AssignNewCharacter(null);
                Debug.Log("End " + characterStates_Player[i].character.name + "'s Turn");
            }
            #endregion

            if (!hasPlayableCharacter)
            {
                Debug.Log("GAME_OVER");
                break;
            }
            // TODO: End player turn on button, but allow for cycling between players while they still have actions
            #region Enemy Turn
            for (int i = 0; i < characterStates_Enemy.Count; i++)
            {
                if (characterStates_Enemy[i].characterStats.CurrentHP <= 0f)
                {
                    hasPlayableEnemy = false;
                    continue;
                }
                else
                {
                    hasPlayableEnemy = true;
                }

                //Debug.Log("");
                Debug.Log("Start " + characterStates_Enemy[i].character.name + "'s Turn");
                turnText.text = "Turn: " + characterStates_Enemy[i].character.name;
                yield return new WaitForSeconds(1f);
                battleStateMachine.ChangeState(characterStates_Enemy[i]);

                // Was waiting for nextCharacter, but nothing was updating the state INORDER to get nextCharacter
                //yield return new WaitUntil(() => nextCharacter == true); // WaitUntil nextCharacter == true

                while (nextCharacter == false)
                {
                    //yield return new WaitForEndOfFrame(); // Works better for input
                    battleStateMachine.UpdateState();
                    yield return new WaitForSeconds(0.0001f);
                    //yield return new WaitForEndOfFrame();
                }
                nextCharacter = false;

                characterUI.AssignNewCharacter(null);
                Debug.Log("End " + characterStates_Enemy[i].character.name + "'s Turn");
            }
            #endregion

            if (!hasPlayableEnemy)
            {
                Debug.Log("CONGRATS");

                LoadNextLevel();
                StartCoroutine(TurnSequence());
                break;
            }
            nextCharacter = false;
            turnCount++;
            yield return new WaitForEndOfFrame();
        }

        //Debug.Log("TurnSequence Exiting");

        yield break;
    }

    public void CreateCharacter(CharacterStats a_CharacterStat)
    {
        // Creates CharacterState and Assigns GameObject
        CharacterState a_CState = new CharacterState(a_CharacterStat.gameObject);
        a_CharacterStat.Name = a_CharacterStat.gameObject.name; // TODO: Is CharacterStats.name necessary?
        characterStates.Add(a_CState);
        if (a_CState.characterStats.isEnemy)
        {
            characterStates_Enemy.Add(a_CState);
        }
        else
        {
            characterStates_Player.Add(a_CState);
        }

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
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < characterStates.Count; i++)
            {
                // Update Tile location via TileManager

                // If the local tiles doesn't match it's current tile...
                if (characterStates[i].localTile != TileManager.Instance.GetTileFromWorldPosition(characterStates[i].characterStats.gameObject.transform.position))
                {
                    // ...If it has a tile...
                    if (characterStates[i].localTile != null)
                    {
                        // ... reset the local tile from it's previous location
                        characterStates[i].localTile.hasCharacter = false;
                        characterStates[i].localTile.characterState = null;
                    }

                    //... assign the current tile to the local tile
                    //Debug.Log("UpdatingTile");
                    characterStates[i].localTile = TileManager.Instance.GetTileFromWorldPosition(characterStates[i].characterStats.gameObject.transform.position);

                    characterStates[i].localTile.hasCharacter = true;
                    characterStates[i].localTile.characterState = characterStates[i];
                    //Debug.Log(characterStates[i].character.name + "'s Local Tile" + characterStates[i].localTile);
                }
            }

        }
    }

    public void AttackCharacter(CharacterState attacker, CharacterState defender)
    {
        StartCoroutine(CharacterAttacking(attacker, defender));
        defender.characterStats.CurrentHP = defender.characterStats.CurrentHP - (attacker.characterStats.attack /*+attacker.characterStats.MODIFIERS- defender.characterStats.MODIFIERS*/ );
    }

    IEnumerator CharacterAttacking(CharacterState attacker, CharacterState defender)
    {
        attacker.characterStats.IsAttacking();
        yield return new WaitForSeconds(1f);
        defender.characterStats.WasHit();
    }

    public void LoadNextLevel()
    {
        isLevelLoaded = false;

        currentLevel++;

        if (currentLevel > 1)
        {
            levels[currentLevel - 2].Unload();
        }
        levels[currentLevel - 1].Load();

        for (int i = 0; i < characterStates_Player.Count; i++)
        {
            characterStates_Player[i].characterStats.ResetHealth();
        }
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