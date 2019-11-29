using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public BattleManager battleManager;
    public CameraControl mainCameraController;

    public List<GameObject> prefab_Characters;
    public List<GameObject> currentTeam;
    public List<GameObject> enemyTeam;

    [Header("Levels Status")]
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



    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // If the scene isn't Cutscene, load it
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); //Emily commented out, trying to test gameplay
        }

        // If the scene isn't TurnBasedTest, load it
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(2))
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive); //Emily commented out, trying to test gameplay
        }

        mainCameraController = Camera.main.GetComponent<CameraControl>();

        // Load Menu if not already loaded by build settings

        // TODO: Temp BattleManager startup for testing
        /*
        battleManager.currentLevel = levels[0];
        battleManager.Startup(currentTeam, enemyTeam);
        mainCameraController.FindPlayer();
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        /*
        isLevelLoaded = false;
        spriteLayering.UpdateReferences();

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
        */
    }

    public void LoadLevel(int levelNum)
    {
        if ( battleManager.currentLevel != null)
        {
            battleManager.Unloadlevel();
        }

        battleManager.currentLevel = battleManager.levels[levelNum];
        battleManager.LoadLevel();

        //battleManager.Startup(currentTeam, enemyTeam);
    }
}
