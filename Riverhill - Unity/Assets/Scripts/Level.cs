using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject Grid_Tile;
    public GameObject Grid_Obstacles;
    public GameObject Grid_Unwalkable;
    public GameObject[] spawnMaps;
    public List<Vector3> spawnPositions_Player;
    public List<Vector3> spawnPositions_Enemy;
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

        if (toggle == true)
        {
            // Spawn Positions
            //TODO: This only works for the tutorial spawn due to unclear spawnmaps

            //          Player
            spawnMaps[0].gameObject.SetActive(true);
            spawnPositions_Player = TileManager.Instance.GetTilePositionsFromGrid(spawnMaps[0].GetComponent<Grid>());
            spawnMaps[0].gameObject.SetActive(false);

            //          Enemy
            spawnMaps[1].gameObject.SetActive(true);
            spawnPositions_Enemy = TileManager.Instance.GetTilePositionsFromGrid(spawnMaps[1].GetComponent<Grid>());
            spawnMaps[1].gameObject.SetActive(false);
        }

        Grid_Tile.gameObject.SetActive(toggle);
        Grid_Obstacles.gameObject.SetActive(toggle);
        Grid_Unwalkable.gameObject.SetActive(toggle);


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

        if (toggle)
        {
            TileManager.Instance.grid = Grid_Tile.GetComponent<Grid>();
            TileManager.Instance.Reset();
        }

        BattleManager.Instance.isLevelLoaded = toggle;

        yield break;
    }
}
