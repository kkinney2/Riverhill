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
        Grid_Tile.gameObject.SetActive(false);
        Grid_Obstacles.gameObject.SetActive(false);
        Grid_Unwalkable.gameObject.SetActive(false);
    }

    public void Load()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        Grid_Tile.gameObject.SetActive(true);
        yield return new WaitUntil(() => Grid_Tile.gameObject.activeSelf == true); 

        Grid_Obstacles.gameObject.SetActive(true);
        yield return new WaitUntil(() => Grid_Obstacles.gameObject.activeSelf == true);

        Grid_Unwalkable.gameObject.SetActive(true);
        yield return new WaitUntil(() => Grid_Unwalkable.gameObject.activeSelf == true);

        yield break;
    }
}
