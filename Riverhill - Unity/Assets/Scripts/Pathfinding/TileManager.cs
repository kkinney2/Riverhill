using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region Singleton
    private static TileManager _instance;

    public static TileManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public Grid grid;
    public Vector2Int gridSize;

    public Tile[,] tiles;

    public GameObject tilesHighlights;

    public List<GameObject> tilesList;
    GameObject tilesParent;

    Pathfinding pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        if (tilesHighlights == null)
        {
            tilesHighlights = new GameObject();
            tilesHighlights.name = "Tile Highlights";
        }

        if (gridSize.x == 0 || gridSize.y == 0)
        {
            gridSize = new Vector2Int(10, 10);
        }
        tiles = new Tile[1000, 1000]; // TODO: Hardcoded Tiles array
        tilesList = new List<GameObject>();
        tilesParent = new GameObject();
        tilesParent.name = "Tiles";

        pathfinder = new Pathfinding();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset()
    {
        if (tilesHighlights == null)
        {
            tilesHighlights = new GameObject();
            tilesHighlights.name = "Tile Highlights";
        }

        if (gridSize.x == 0 || gridSize.y == 0)
        {
            gridSize = new Vector2Int(10, 10);
        }
        tiles = new Tile[1000, 1000]; // TODO: Hardcoded Tiles array
        tilesList = new List<GameObject>();
        tilesParent = new GameObject();
        tilesParent.name = "Tiles_" + grid.name;

        pathfinder = new Pathfinding();

        Setup();
        FindNeighbors();
        /*
        StartCoroutine(Coroutine_Setup());
        StartCoroutine(Coroutine_FindNeighbors());
        */
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (Application.isPlaying == true)
        {
            for (int i = 0; i < tilesList.Count; i++)
            {
                Gizmos.DrawSphere(tilesList[i].transform.position, 1);
            }
        }
    }

    public List<Vector3> GetTilePositionsFromGrid(Grid a_Grid)
    {
        List<Vector3> tempList = new List<Vector3>();
        for (int x = -gridSize.x; x < gridSize.x; x++)
        {
            for (int y = -gridSize.y; y < gridSize.y; y++)
            {
                Vector3 cellPoint = a_Grid.CellToWorld(new Vector3Int(x, y, 0));

                if (Physics2D.Raycast(cellPoint + Vector3.back, Vector3.forward, Mathf.Infinity, 1))
                {

                    tempList.Add(cellPoint);
                }
            }
        }

        return tempList;
    }

    public void Setup()
    {
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
                    GameObject a_tempGO = new GameObject();
                    a_tempGO.transform.parent = tilesParent.transform;
                    a_tempGO.transform.position = cellPoint;

                    a_tempGO.gameObject.name = cellPoint.ToString();

                    Tile tileComp = a_tempGO.AddComponent<Tile>();
                    tileComp.owner = this;
                    tileComp.cellPosition = new Vector2Int(x, y);

                    tiles[x + gridSize.x, y + gridSize.y] = tileComp;
                    tilesList.Add(a_tempGO);
                }
            }
        }
    }

    IEnumerator Coroutine_Setup()
    {
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
                    GameObject a_tempGO = new GameObject();
                    a_tempGO.transform.parent = tilesParent.transform;
                    a_tempGO.transform.position = cellPoint;

                    a_tempGO.gameObject.name = cellPoint.ToString();

                    Tile tileComp = a_tempGO.AddComponent<Tile>();
                    tileComp.owner = this;
                    tileComp.cellPosition = new Vector2Int(x, y);

                    tiles[x + gridSize.x, y + gridSize.y] = tileComp;
                    tilesList.Add(a_tempGO);
                }
            }
        }

        yield return null;
    }

    public void FindNeighbors()
    {
        for (int i = 0; i < tilesList.Count; i++)
        {
            Tile currentTile = tilesList[i].GetComponent<Tile>();
            currentTile.neighborTiles = GetNeighborTiles(currentTile);
        }
    }

    IEnumerator Coroutine_FindNeighbors()
    {

        for (int i = 0; i < tilesList.Count; i++)
        {
            Tile currentTile = tilesList[i].GetComponent<Tile>();
            currentTile.neighborTiles = GetNeighborTiles(currentTile);
        }

        yield return null;
    }

    public Tile GetTile(Vector2Int a_Cell)
    {
        return tiles[a_Cell.x + gridSize.x, a_Cell.y + gridSize.y];
    }

    public Tile GetTileFromWorldPosition(Vector3 a_WorldPosition)
    {
        Vector3Int cellPos = grid.WorldToCell(a_WorldPosition);
        return GetTile(new Vector2Int(cellPos.x, cellPos.y));
    }

    public List<Tile> GetNeighborTiles(Tile a_Tile)
    {
        List<Tile> NeighboringTiles = new List<Tile>();
        int xCheck;
        int yCheck;

        for (int i = 0; i < 6; i++)
        {
            // Even Columns
            if (a_Tile.cellPosition.y % 2 == 0)
            {
                switch (i)
                {
                    case 0:
                        // Top
                        xCheck = a_Tile.cellPosition.x + 1;
                        yCheck = a_Tile.cellPosition.y;
                        break;
                    case 1:
                        // Right Top
                        xCheck = a_Tile.cellPosition.x;
                        yCheck = a_Tile.cellPosition.y + 1;
                        break;
                    case 2:
                        // Right Bottom
                        xCheck = a_Tile.cellPosition.x - 1;
                        yCheck = a_Tile.cellPosition.y + 1;
                        break;
                    case 3:
                        // Bottom
                        xCheck = a_Tile.cellPosition.x - 1;
                        yCheck = a_Tile.cellPosition.y;
                        break;
                    case 4:
                        // Left Bottom
                        xCheck = a_Tile.cellPosition.x - 1;
                        yCheck = a_Tile.cellPosition.y - 1;
                        break;
                    case 5:
                        // Left Top
                        xCheck = a_Tile.cellPosition.x;
                        yCheck = a_Tile.cellPosition.y - 1;
                        break;
                    default:
                        Debug.Log("Too many sides to a tile");
                        xCheck = 0;
                        yCheck = 0;
                        break;
                }
            }

            // Odd Columns
            else
            {
                switch (i)
                {
                    case 0:
                        // Top
                        xCheck = a_Tile.cellPosition.x + 1;
                        yCheck = a_Tile.cellPosition.y;
                        break;
                    case 1:
                        // Right Top
                        xCheck = a_Tile.cellPosition.x + 1;
                        yCheck = a_Tile.cellPosition.y + 1;
                        break;
                    case 2:
                        // Right Bottom
                        xCheck = a_Tile.cellPosition.x;
                        yCheck = a_Tile.cellPosition.y + 1;
                        break;
                    case 3:
                        // Bottom
                        xCheck = a_Tile.cellPosition.x - 1;
                        yCheck = a_Tile.cellPosition.y;
                        break;
                    case 4:
                        // Left Bottom
                        xCheck = a_Tile.cellPosition.x;
                        yCheck = a_Tile.cellPosition.y - 1;
                        break;
                    case 5:
                        // Left Top
                        xCheck = a_Tile.cellPosition.x + 1;
                        yCheck = a_Tile.cellPosition.y - 1;
                        break;
                    default:
                        Debug.Log("Too many sides to a tile");
                        xCheck = 0;
                        yCheck = 0;
                        break;
                }
            }

            if (tiles[xCheck + gridSize.x, yCheck + gridSize.x] != null)
            {
                NeighboringTiles.Add(tiles[xCheck + gridSize.x, yCheck + gridSize.x]);
            }
        }

        return NeighboringTiles;
    }

    public List<Tile> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        return pathfinder.FindPath(startPos, targetPos);
    }
}
