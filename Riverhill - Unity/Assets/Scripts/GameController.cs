using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    BattleManager battleManager;
    public Level[] levels;

    // Start is called before the first frame update
    void Start()
    {
        battleManager = BattleManager.Instance;

        // If the scene isn't Cutscene, load it
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }

        // Load Menu if not already loaded by build settings
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
        if (battleManager.currentLevel != null)
        {
            battleManager.Unloadlevel();
        }

        battleManager.currentLevel = levels[levelNum];
        battleManager.LoadLevel();

        battleManager.Startup();
    }
}
