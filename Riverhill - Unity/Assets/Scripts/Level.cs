using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public TileManager tileManager;

    [Header("Bounds")]
    public Transform UpperBound;
    public Transform LowerBound;

    private void Start()
    {

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
        BattleManager.Instance.currentLevel = this;

        Grid_Tile.gameObject.SetActive(toggle);
        Grid_Obstacles.gameObject.SetActive(toggle);
        Grid_Unwalkable.gameObject.SetActive(toggle);
        characters.SetActive(toggle);

        if (toggle)
        {
            TileManager.Instance.grid = Grid_Tile.GetComponent<Grid>();
            TileManager.Instance.Reset();
        }

        BattleManager.Instance.isLevelLoaded = toggle;
    }

    IEnumerator Coroutine_LoadLevel(bool toggle)
    {
        BattleManager.Instance.currentLevel = this;

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

        BattleManager.Instance.isLevelLoaded = toggle;

        yield break;
    }
}
