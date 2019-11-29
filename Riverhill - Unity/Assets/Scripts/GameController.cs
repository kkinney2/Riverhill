using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public CameraControl mainCameraController;

    public List<GameObject> prefab_Characters;


    [Header("Levels Status")]
    private int currentLevelNum = 0;
    public bool hasActiveLevel = false;
    // Tutorial Level
    public bool level0_Unlocked = true;
    public bool level0_Completed = false;

    public bool level1_Unlocked = false;
    public bool level1_Completed = false;

    public bool level2_Unlocked = false;
    public bool level2_Completed = false;

    public bool level3_Unlocked = false;
    public bool level3_Completed = false;

    public bool level4_Unlocked = false;
    public bool level4_Completed = false;

    [Header("Character Status")]
    public List<bool> characterStatuses;

    public bool alyss_Unlocked = true;

    public bool dayana_Unlocked = false;

    public bool nelson_Unlocked = false;


    public List<GameObject> currentTeam;
    public List<GameObject> enemyTeam;


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
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: Break each level out into seperate coroutines so that they can be skipped for level loading
    IEnumerator GameSequence()
    {
        cutsceneManager.StartCutscene("Intro cutscene");

        yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);

        cutsceneManager.StartCutscene("CH_1 Tutorial");

        yield return new WaitUntil(() => cutsceneManager.hasActiveCutscene == false);

        //battleManager.currentLevel = battleManager.levels[0]; 
        LoadLevel(0);                                         // I think this as like a cartridge.
        battleManager.Startup(currentTeam, enemyTeam);        // And this is turning on the console.
        hasActiveLevel = true;

        // TODO: Needs a way to repeat if not beaten or a way to re enter the level
        yield return new WaitUntil(() => hasActiveLevel == false);



        // Placeholder to validate the IEnumerator
        yield return new WaitForEndOfFrame();
    }

    public void NewGame()
    {
        StartCoroutine(GameSequence());
    }

    public void LoadGame()
    {
        // TODO: Implement Saving
        //       This tutorial seems to have an easy to implement format.
        //       https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity#toc-anchor-004
    }

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
}
