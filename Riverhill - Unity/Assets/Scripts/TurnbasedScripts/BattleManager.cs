using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            gameController.battleManager = this;
        }
        else
        {

        }
    }
    #endregion


    public Level[] levels;
    public GameController gameController;

    public GameObject prefab_CharacterUI;
    public Canvas characterUICanvas;

    CharacterUI characterUI;
    GameObject characterUI_Object;

    public List<CharacterStats> characterStats;
    public List<CharacterState> characterStates;

    public List<CharacterState> characterStates_Enemy;
    public List<CharacterState> characterStates_Player;

    public SpriteLayering spriteLayering;

    public Level currentLevel;
    public bool isLevelLoaded = false;

    public int turnCount = 0;

    public Text turnText;

    //trying to get health bar stuff to appear
    /* MOVING OVER TO CHAR STATS
    public Image P1HPBar;
    public Image E1HPBar;
    public CharacterStats charStatsAlyss;
    public CharacterStats charStatsDayana;
    */

    bool hasPlayableCharacter = false;
    bool hasPlayableEnemy = false;

    public bool nextCharacter = false;

    private BattleStateMachine battleStateMachine;
    
    //gameplay music
    public AudioSource gameplayMusicAS;
    public AudioClip gameplayMusic;
    //for stopping gameplay music?
    public bool gameplayMusicIsPlaying;
    public bool shouldTurnOffMusic;

    // Start is called before the first frame update
    void Start()
    {
        turnText.gameObject.SetActive(false);

        //P1HPBar.gameObject.SetActive(false);
        //E1HPBar.gameObject.SetActive(false);

        gameplayMusicAS = GetComponent<AudioSource>();

        characterUICanvas = prefab_CharacterUI.GetComponent<Canvas>();
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
            players[i] = Instantiate(players[i]);
            players[i].gameObject.tag = "Player";
            characterStats.Add(players[i].GetComponent<CharacterStats>());
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i] = Instantiate(enemies[i]);
            enemies[i].gameObject.tag = "Enemy";
            characterStats.Add(enemies[i].GetComponent<CharacterStats>());
        }

        // Create Characters
        StartCoroutine(CreateCharacters());

        GenerateCharacterUI();
        Debug.Log("***CharacterUI Created***");

        LoadLevel();

        //StartCoroutine(UpdateTiles());

        // TODO: Move Camera targeting to when characters are switched
        if (gameController != null)
        {
            gameController.mainCameraController.FindPlayer();
        }
        else
        {
            Camera.main.GetComponent<CameraControl>().FindPlayer();
        }

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

                Debug.Log("Start " + characterStates_Player[i].characterStats.Name + "'s Turn");
                turnText.text = "Turn: " + characterStates_Player[i].characterStats.Name;

                //EMILY ADDED TO GET HEALTH BARS WORKING UPON LOAD (NO UNITY PAUSE)
                /*
                charStatsAlyss.currentHealthText.text = ("Health: " + charStatsAlyss.CurrentHP);
                charStatsAlyss.HPBarFill.fillAmount = ((charStatsAlyss.CurrentHP) / 100);
                charStatsDayana.currentHealthText.text = ("Health: " + charStatsDayana.CurrentHP);
                charStatsDayana.HPBarFill.fillAmount = ((charStatsDayana.CurrentHP) / 100);
                */

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
                if (gameController != null)
                {
                    gameController.currentStatus = "LevelFailed";
                    gameController.hasActiveLevel = false;
                }

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
                Debug.Log("Start " + characterStates_Enemy[i].characterStats.Name + "'s Turn");
                turnText.text = "Turn: " + characterStates_Enemy[i].characterStats.Name;

                //EMILY ADDED TO GET HEALTH BARS WORKING UPON LOAD (NO UNITY PAUSE)
                /*
                charStatsAlyss.currentHealthText.text = ("Health: " + charStatsAlyss.CurrentHP);
                charStatsAlyss.HPBarFill.fillAmount = ((charStatsAlyss.CurrentHP) / 100);
                charStatsDayana.currentHealthText.text = ("Health: " + charStatsDayana.CurrentHP);
                charStatsDayana.HPBarFill.fillAmount = ((charStatsDayana.CurrentHP) / 100);
                */

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
                if (gameController != null)
                {
                    gameController.currentStatus = "LevelCompleted";
                    gameController.hasActiveLevel = false;
                }

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
        //a_CharacterStat.Name = a_CharacterStat.gameObject.name;
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
    }

    IEnumerator CharacterAttacking(CharacterState attacker, CharacterState defender)
    {
        attacker.characterStats.IsAttacking();
        yield return new WaitForSeconds(1f);
        defender.characterStats.WasHit();
        defender.characterStats.CurrentHP = defender.characterStats.CurrentHP - (attacker.characterStats.attack /*+attacker.characterStats.MODIFIERS- defender.characterStats.MODIFIERS*/ );
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

            gameplayMusicAS.enabled = true;
            gameplayMusicAS.Play();
            gameplayMusicIsPlaying = true;

            //P1HPBar.gameObject.SetActive(true);
            //E1HPBar.gameObject.SetActive(true);

            StartCoroutine(UpdateTiles());
        }

    }

    public void Unloadlevel()
    {
        turnText.gameObject.SetActive(false);

        //P1HPBar.gameObject.SetActive(false);
        //E1HPBar.gameObject.SetActive(false);

        if (gameplayMusicIsPlaying == true)
        {
            shouldTurnOffMusic = true;

            if (shouldTurnOffMusic == true)
            {
                gameplayMusicAS.enabled = false;
                gameplayMusicIsPlaying = false;
                shouldTurnOffMusic = false;
            }
        }

        currentLevel.Unload();
    }
}