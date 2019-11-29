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


    public Level[] levels;
    GameController gameController;

    public GameObject prefab_CharacterUI;
    CharacterUI characterUI;
    GameObject characterUI_Object;

    public List<CharacterStats> characterStats;
    public List<CharacterState> characterStates;

    public List<CharacterState> characterStates_Enemy;
    public List<CharacterState> characterStates_Player;

    public SpriteLayering spriteLayering;

    public Level currentLevel;
    public LevelConditions levelConditions;
    public bool isLevelLoaded = false;

    public int turnCount = 0;

    public Text turnText;

    bool hasPlayableCharacter = false;
    bool hasPlayableEnemy = false;

    public bool nextCharacter = false;

    private BattleStateMachine battleStateMachine;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.battleManager = this;
        turnText.gameObject.SetActive(false);
    }

    // GameController now supplies the characters to play with
    public void Startup(List<GameObject> players, List<GameObject> enemies)
    {
        battleStateMachine = new BattleStateMachine();
        characterStates = new List<CharacterState>();
        characterStates_Enemy = new List<CharacterState>();
        characterStates_Player = new List<CharacterState>();

        for (int i = 0; i < players.Count; i++)
        {
            characterStats.Add(players[i].GetComponent<CharacterStats>());
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            characterStats.Add(enemies[i].GetComponent<CharacterStats>());
        }

        levelConditions = GameSettings.Instance.gameObject.GetComponent<LevelConditions>();

        // Create Characters
        StartCoroutine(CreateCharacters());

        GenerateCharacterUI();
        Debug.Log("***CharacterUI Created***");

        LoadLevel();

        //StartCoroutine(UpdateTiles());

        // TODO: Move Camera targeting to when characters are switched
        gameController.mainCameraController.FindPlayer();
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

    IEnumerator TurnSequence()
    {
        turnCount++;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => characterStates_Player[characterStates_Player.Count - 1].characterStats.CurrentHP > 0); // WaitUntil the last character has their health set
        yield return new WaitUntil(() => isLevelLoaded == true);
        levelConditions.levelName = currentLevel.name;

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
                // TODO: Communicate the game is over
                Debug.Log("GAME_OVER");
                gameController.hasActiveLevel = false;
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
                // TODO: Communicate the game is over
                Debug.Log("CONGRATS");

                //TODO: ResetLevel Loading
                gameController.hasActiveLevel = false;
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
            if (characterStates.Count > 0)
            {
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

    private void LoadLevel()
    {
        if (currentLevel == null)
        {
            Debug.LogWarning("No level to Load");
        }
        else
        {
            currentLevel.Load();
            turnText.gameObject.SetActive(true);
            StartCoroutine(UpdateTiles());
        }

    }

    public void Unloadlevel()
    {
        turnText.gameObject.SetActive(false);
        currentLevel.Unload();
    }
}