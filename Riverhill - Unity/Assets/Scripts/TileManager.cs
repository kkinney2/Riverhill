using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Grid grid;
    public Vector2Int gridSize;

    public Tile[,] tiles;
    GameObject tilesParent;

    // Start is called before the first frame update
    void Start()
    {
        if (gridSize.x == 0 || gridSize.y == 0)
        {
            gridSize = new Vector2Int(10, 10);
        }
        tiles = new Tile[100, 100]; // TODO: Hardcoded Tiles array
        tilesParent = new GameObject();
        tilesParent.name = "Tiles";

        StartCoroutine(Setup());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Setup()
    {
        GameObject tile;

        for (int x = -gridSize.x; x < gridSize.x; x++)
        {
            for (int y = -gridSize.y; y < gridSize.y; y++)
            {
                //Debug.Log("Ting");
                Vector3 cellPoint = grid.CellToWorld(new Vector3Int(x, y, 0));
                //Debug.Log("CastPoint: " + cellPoint);

                if (Physics2D.Raycast(cellPoint + Vector3.back, Vector3.forward, Mathf.Infinity, 1))
                {
                    //Debug.Log("ping");
                    GameObject a_tempGO = Instantiate(new GameObject(), cellPoint, Quaternion.identity, tilesParent.transform);
                    a_tempGO.name = cellPoint.ToString();

                    Tile tileComp = a_tempGO.AddComponent<Tile>();
                    tileComp = new Tile(this);

                    tiles[x + gridSize.x, y + gridSize.y] = a_tempGO.GetComponent<Tile>();
                }
            }
        }

        yield return null;
    }

    public Tile GetTile(Vector2Int a_Cell)
    {
        return tiles[a_Cell.x + gridSize.x, a_Cell.y + gridSize.y];
    }
}
