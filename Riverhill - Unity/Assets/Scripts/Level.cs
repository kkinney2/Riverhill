using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject Grid_Tile;
    public GameObject Grid_Obstacles;
    public GameObject Grid_Unwalkable;
    public GameObject[] spawnMaps;
    public Vector3[] spawnPositions;
    public TileManager tileManager;

    [Header("Bounds")]
    public Transform UpperBound;
    public Transform LowerBound;

    private void Start()
    {
        /*
        gameObject.SetActive(false);
        Grid_Tile.gameObject.SetActive(false);
        Grid_Obstacles.gameObject.SetActive(false);
        Grid_Unwalkable.gameObject.SetActive(false);
        */
    }

    public void Load()
    {
        //StartCoroutine(LoadLevel(true));
        Grid_Tile.gameObject.SetActive(true);
        Grid_Obstacles.gameObject.SetActive(true);
        Grid_Unwalkable.gameObject.SetActive(true);

        TileManager.Instance.grid = Grid_Tile.GetComponent<Grid>();
        TileManager.Instance.Reset();
        BattleManager.Instance.isLevelLoaded = true;
    }

    public void Unload()
    {
        StartCoroutine(LoadLevel(false));
    }

    IEnumerator LoadLevel(bool toggle)
    {
        BattleManager.Instance.currentLevel = this;

        Grid_Tile.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Tile.gameObject.activeSelf == toggle); 

        Grid_Obstacles.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Obstacles.gameObject.activeSelf == toggle);

        Grid_Unwalkable.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Unwalkable.gameObject.activeSelf == toggle);

        TileManager.Instance.grid = Grid_Tile.GetComponent<Grid>();
        TileManager.Instance.Reset();
        BattleManager.Instance.isLevelLoaded = true;

        yield break;
    }
}
