using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject Grid_Tile;
    public GameObject Grid_Obstacles;
    public GameObject Grid_Unwalkable;

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
        StartCoroutine(LoadLevel(true));
    }

    public void Unload()
    {
        StartCoroutine(LoadLevel(false));
    }

    IEnumerator LoadLevel(bool toggle)
    {
        Grid_Tile.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Tile.gameObject.activeSelf == toggle); 

        Grid_Obstacles.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Obstacles.gameObject.activeSelf == toggle);

        Grid_Unwalkable.gameObject.SetActive(toggle);
        yield return new WaitUntil(() => Grid_Unwalkable.gameObject.activeSelf == toggle);

        TileManager.Instance.Reset();
        BattleManager.Instance.isLevelLoaded = true;

        yield break;
    }
}
