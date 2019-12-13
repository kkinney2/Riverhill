using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameObject Grid_Tile;
    public GameObject Grid_Obstacles;
    public GameObject Grid_Unwalkable;
    //public GameObject[] spawnMaps;
    //public List<Vector3> spawnPositions_Player;
    //public List<Vector3> spawnPositions_Enemy;
    public GameObject characters;
    public GameObject[] players;
    public GameObject[] enemies;
    // TODO: Spawn map for other characters
    public GameController gameController;
    public TileManager tileManager;
    public BattleManager battleManager;

    [Header("Bounds")]
    public Transform UpperBound;
    public Transform LowerBound;

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    public void Load()
    {
        LoadLevel(true);
    }

    public void Unload()
    {
        LoadLevel(false);
        //StartCoroutine(Coroutine_LoadLevel(false));
    }

    public void LoadLevel(bool toggle)
    {
        gameController.battleManager.currentLevel = this;

        Grid_Tile.gameObject.SetActive(toggle);
        Grid_Obstacles.gameObject.SetActive(toggle);
        Grid_Unwalkable.gameObject.SetActive(toggle);
        characters.SetActive(toggle);

        if (toggle)
        {
            TileManager.Instance.grid = Grid_Tile.GetComponent<Grid>();
            TileManager.Instance.Reset();
        }

        gameController.battleManager.isLevelLoaded = toggle;
    }

    IEnumerator Coroutine_LoadLevel(bool toggle)
    {
        gameController.battleManager.currentLevel = this;

        Grid_Tile.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Tile.gameObject.activeSelf == toggle);

        Grid_Obstacles.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Obstacles.gameObject.activeSelf == toggle);

        Grid_Unwalkable.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Unwalkable.gameObject.activeSelf == toggle);

        characters.SetActive(toggle);

        if (toggle)
        {
            TileManager.Instance.grid = Grid_Tile.GetComponent<Grid>();
            TileManager.Instance.Reset();
        }

        gameController.battleManager.isLevelLoaded = toggle;

        yield break;
    }
}
