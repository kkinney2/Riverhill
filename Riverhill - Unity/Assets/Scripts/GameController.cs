using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Tooltip("This is recieved by script once available." +
        "This only serves to update the Developer.")]
    // Also allows the specified script to report itself without a method
    public BattleManager battleManager;
    [Tooltip("This is recieved by script once available." +
        "This only serves to update the Developer.")]
    // Also allows the specified script to report itself without a method
    public CutsceneManager cutsceneManager;
    public GameSettings gameSettings;
    public CameraControl mainCameraController;

    public List<GameObject> prefab_Characters;

    [Header("Game Status")]
    public string currentStatus;
    private int currentLevelNum = 0;

    [Header("Levels Status")]
    public GameObject MenuUI;
    public Button LoadGame_button;
    public Button LevelSelect_button;
    public GameObject LevelSelect_Menu;
    public bool hasStartedGame = false;

    public bool hasActiveLevel = false;
    // Tutorial Level
    public bool level0_Unlocked = true;
    public bool level0_Completed = false;
    public Button level0_button;
    public bool alyssMoved = false;
    public bool alyssAttacked = false;
    public bool dayanaAttacked = false;

    public bool level1_Unlocked = false;
    public bool level1_Completed = false;
    public Button level1_button;

    public bool level2_Unlocked = false;
    public bool level2_Completed = false;
    public Button level2_button;

    public bool level3_Unlocked = false;
    public bool level3_Completed = false;
    public Button level3_button;
    public bool talkedWithNelson = false;

    public bool level4_Unlocked = false;
    public bool level4_Completed = false;
    public Button level4_button;

    /*
    [Header("Character Status")]
    public List<bool> characterStatuses;

    public bool alyss_Unlocked = true;

    public bool dayana_Unlocked = false;

    public bool nelson_Unlocked = false;

    public List<GameObject> currentTeam;
    public List<GameObject> enemyTeam;

    */
    Level[] levels;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // If the scene isn't Cutscene, load it
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }

        // If the scene isn't TurnBasedTest, load it
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(2))
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }

        mainCameraController = Camera.main.GetComponent<CameraControl>();

        currentStatus = "MainMenu";
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStatus == "MainMenu")
        {
            #region Load/LevelSelect Button
            //TODO: Need to check state for load/levelSelect **Hardcoded**
            if (hasStartedGame)
            {
                LevelSelect_button.interactable = true;
            }
            else
            {
                LevelSelect_button.interactable = false;
            }
            if (false)
            {
                LoadGame_button.gameObject.SetActive(true);
                LevelSelect_button.gameObject.SetActive(false);
            }
            else
            {
                LoadGame_button.gameObject.SetActive(false);
                LevelSelect_button.gameObject.SetActive(true);
            }
            #endregion
        }
    }

    #region Level Selection
    public void Coroutine_LevelSelection()
    {
        StartCoroutine(LevelSelection());
    }

    IEnumerator LevelSelection()
    {
        currentStatus = "LevelSelection";
        SetMenuUI(true);
        LevelSelect_Menu.SetActive(true);
        // Interactions
        while (true)
        {
            yield return new WaitForEndOfFrame();
            // Load Level Selection Screen

            // Wait While level is loaded

            #region Button Interactivity
            #region Tutorial Button
            if (level0_Unlocked)
            {
                level0_button.interactable = true;
            }
            else
            {
                level0_button.interactable = false;
            }
            #endregion

            #region LevelOne Button
            if (level1_Unlocked)
            {
                level1_button.interactable = true;
            }
            else
            {
                level1_button.interactable = false;
            }
            #endregion

            #region LevelTwo Button
            if (level2_Unlocked)
            {
                level2_button.interactable = true;
            }
            else
            {
                level2_button.interactable = false;
            }
            #endregion

            #region LevelThree Button
            if (level3_Unlocked)
            {
                level3_button.interactable = true;
            }
            else
            {
                level3_button.interactable = false;
            }
            #endregion

            #region LevelFour Button
            if (level4_Unlocked)
            {
                level4_button.interactable = true;
            }
            else
            {
                level4_button.interactable = false;
            }
            #endregion
            #endregion
        }


        yield return null;
    }
    #endregion

    #region Tutorial
    IEnumerator Level_Tutorial()
    {
        if (!gameSettings.canSkipCutscenes)
        {
            //cutsceneManager.StartCutscene("Tutorial - gameplay start");
            cutsceneManager.StartCutscene("CH_1 Tutorial");
            yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
        }

        CreateBattleManager(battleManager);

        LoadLevel(0);                   // I think this as like a cartridge.
        battleManager.Startup();        // And this is turning on the console.
        hasActiveLevel = true;

        if (!gameSettings.canSkipCutscenes)
        {
            StartCoroutine(Tutorial_MidBattle_Cutscenes());
        }

        yield return new WaitUntil(() => hasActiveLevel == false);

        if (currentStatus == "LevelCompleted")
        {
            level0_Completed = true;
            level1_Unlocked = true;
        }

        battleManager.Unloadlevel();

        mainCameraController.Reset();

        //       Show that next level is unlocked?
        StartCoroutine(LevelSelection());
        yield return null;
    }

    IEnumerator Tutorial_MidBattle_Cutscenes()
    {
        //cutsceneManager.StartCutscene("CH_1 Tutorial");
        cutsceneManager.StartCutscene("Tutorial - gameplay start");
        yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);

        while (hasActiveLevel)
        {
            yield return new WaitForEndOfFrame();

            if (currentStatus == "LevelFailed")
            {
                cutsceneManager.StartCutscene("Tutorial - Alyss is defeated");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }

            if (currentStatus == "LevelCompleted")
            {
                cutsceneManager.StartCutscene("Tutorial - Dayana is defeated");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }

            if (alyssMoved == true)
            {
                cutsceneManager.StartCutscene("Tutorial - player moves");
            }
            // TODO: Insert this 'if' in the battlemanager while loop line 189
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }

            if (alyssAttacked == true)
            {
                cutsceneManager.StartCutscene("Tutorial - player hits Dayana");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }

            if (dayanaAttacked == true)
            {
                cutsceneManager.StartCutscene("Tutorial - Dayana hits player");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }
        }
        yield return null;
    }
    #endregion

    #region LevelOne
    IEnumerator Level_One()
    {
        if (!gameSettings.canSkipCutscenes)
        {
            cutsceneManager.StartCutscene("CH 2 - First Battle");
            yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
        }

        CreateBattleManager(battleManager);

        //battleManager.currentLevel = battleManager.levels[0]; 
        LoadLevel(1);                   // I think this as like a cartridge.
        battleManager.Startup();        // And this is turning on the console.
        hasActiveLevel = true;

        yield return new WaitUntil(() => hasActiveLevel == false);

        if (currentStatus == "LevelCompleted")
        {
            level1_Completed = true;
            level2_Unlocked = true;
        }

        mainCameraController.Reset();

        //  Send back to level loading screen
        battleManager.Unloadlevel();
        //       Show that next level is unlocked?
        StartCoroutine(LevelSelection());
    }
    #endregion

    #region LevelTwo
    IEnumerator Level_Two()
    {
        if (!gameSettings.canSkipCutscenes)
        {
            cutsceneManager.StartCutscene("Ch 3 - Fort Munge");
            yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
        }

        CreateBattleManager(battleManager);

        //battleManager.currentLevel = battleManager.levels[0]; 
        LoadLevel(2);                      // I think this as like a cartridge.
        battleManager.Startup();           // And this is turning on the console.
        hasActiveLevel = true;

        StartCoroutine(LevelTwo_MidBattle_Cutscenes());

        yield return new WaitUntil(() => hasActiveLevel == false);

        if (currentStatus == "LevelCompleted")
        {
            level2_Completed = true;
            level3_Unlocked = true;
        }

        mainCameraController.Reset();

        // Send back to level loading screen
        battleManager.Unloadlevel();
        //       Show that next level is unlocked?
        StartCoroutine(LevelSelection());
    }

    IEnumerator LevelTwo_MidBattle_Cutscenes()
    {
        while (hasActiveLevel)
        {
            yield return new WaitForEndOfFrame();

            if (currentStatus == "LevelCompleted")
            {
                cutsceneManager.StartCutscene("Ch 3 - Fort Munge post battle");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }
        }
    }
    #endregion

    #region LevelThree
    IEnumerator Level_Three()
    {
        if (!gameSettings.canSkipCutscenes)
        {
            cutsceneManager.StartCutscene("CH 4_farmlands_intro");
            yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
        }

        CreateBattleManager(battleManager);

        //battleManager.currentLevel = battleManager.levels[0]; 
        LoadLevel(3);                   // I think this as like a cartridge.
        battleManager.Startup();        // And this is turning on the console.
        hasActiveLevel = true;

        StartCoroutine(LevelThree_MidBattle_Cutscenes());

        yield return new WaitUntil(() => hasActiveLevel == false);

        if (currentStatus == "LevelCompleted")
        {
            level2_Completed = true;
            level3_Unlocked = true;
        }

        mainCameraController.Reset();

        // Send back to level loading screen
        battleManager.Unloadlevel();
        //       Show that next level is unlocked?
        StartCoroutine(LevelSelection());
    }

    IEnumerator LevelThree_MidBattle_Cutscenes()
    {
        cutsceneManager.StartCutscene("CH 4- talking to nelson");
        yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);

        while (hasActiveLevel)
        {
            yield return new WaitForEndOfFrame();

            /*
            if (currentStatus == "LevelFailed")
            {
                cutsceneManager.StartCutscene("Tutorial - Alyss is defeated");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }
            */
            if (currentStatus == "LevelCompleted")
            {
                cutsceneManager.StartCutscene("CH 4_farmlands_after");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }

            /*
            if (talkedWithNelson == true)
            {
                cutsceneManager.StartCutscene("CH 4- talking to nelson");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }
            */
        }
    }
    #endregion

    #region LevelFour
    IEnumerator Level_Four()
    {
        if (!gameSettings.canSkipCutscenes)
        {
            cutsceneManager.StartCutscene("CH5 - confrontation");
            yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
        }

        CreateBattleManager(battleManager);

        //battleManager.currentLevel = battleManager.levels[0]; 
        LoadLevel(4);                   // I think this as like a cartridge.
        battleManager.Startup();        // And this is turning on the console.
        hasActiveLevel = true;

        StartCoroutine(LevelFour_MidBattle_Cutscenes());

        yield return new WaitUntil(() => hasActiveLevel == false);

        if (currentStatus == "LevelCompleted")
        {
            level2_Completed = true;
            level3_Unlocked = true;
        }

        mainCameraController.Reset();

        // Send back to level loading screen
        battleManager.Unloadlevel();
        //       Show that next level is unlocked?
        StartCoroutine(LevelSelection());
    }

    IEnumerator LevelFour_MidBattle_Cutscenes()
    {
        while (hasActiveLevel)
        {
            yield return new WaitForEndOfFrame();

            if (currentStatus == "LevelCompleted")
            {
                cutsceneManager.StartCutscene("CH5 - after");
            }
            if (cutsceneManager.hasActiveCutscene)
            {
                yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
            }
        }
    }
    #endregion

    #region New Game
    public void NewGame()
    {
        hasStartedGame = true;
        StartCoroutine(StartNewGame());
    }

    IEnumerator StartNewGame()
    {
        if (!gameSettings.canSkipCutscenes)
        {
            cutsceneManager.StartCutscene("Intro cutscene");
            yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);
        }

        StartCoroutine(LevelSelection());
    }
    #endregion

    public void LoadGame()
    {
        // TODO: Implement Saving
        //       This tutorial seems to have an easy to implement format.
        //       https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity#toc-anchor-004

        // Load Save Data
        // The rest should be the same due to references
    }
    #region Level Loading

    public void LoadNextLevel()
    {
        LoadLevel(currentLevelNum + 1);
    }

    public void LoadLevel(int levelNum)
    {
        // Ensures proper level loading
        if (battleManager.currentLevel != null)
        {
            battleManager.Unloadlevel();
        }

        battleManager.currentLevel = battleManager.levels[levelNum];
    }

    public void LoadLevel(string a_Level)
    {
        switch (a_Level)
        {
            case "Tutorial":
                StartCoroutine(Level_Tutorial());
                break;
            case "LevelOne":
                StartCoroutine(Level_One());
                break;
            case "LevelTwo":
                StartCoroutine(Level_Two());
                break;
            case "LevelThree":
                //Debug.Log("NOT IMPLEMENTED: LEVEL_THREE");
                StartCoroutine(Level_Three());
                break;
            case "LevelFour":
                //Debug.Log("NOT IMPLEMENTED: LEVEL_FOUR");
                StartCoroutine(Level_Four());
                break;
            default:
                break;
        }
        SetMenuUI(false);
        StopCoroutine(LevelSelection());
    }
    #endregion

    public void SetMenuUI(bool toggle)
    {
        MenuUI.SetActive(toggle);
    }

    public void CreateBattleManager(BattleManager oldManager)
    {
        BattleManager newManager = oldManager.gameObject.AddComponent<BattleManager>();

        newManager.gameController = this;
        newManager.levels = oldManager.levels;
        //newManager.currentLevel = oldManager.currentLevel;


        newManager.prefab_CharacterUI = oldManager.prefab_CharacterUI;
        /*
        newManager.characterUI = oldManager.characterUI;
        newManager.characterUI_Object = oldManager.characterUI_Object;
        newManager.characterUICanvas = oldManager.characterUICanvas;
        */
        Destroy(oldManager.characterUI_Object);
        newManager.spriteLayering = oldManager.spriteLayering;

        newManager.turnText = oldManager.turnText;

        newManager.gameplayMusicAS = oldManager.gameplayMusicAS;
        newManager.gameplayMusic = oldManager.gameplayMusic;
        newManager.gameplayMusicIsPlaying = oldManager.gameplayMusicIsPlaying;
        newManager.shouldTurnOffMusic = oldManager.shouldTurnOffMusic;

        battleManager = newManager;

        Destroy(oldManager);
    }
}
