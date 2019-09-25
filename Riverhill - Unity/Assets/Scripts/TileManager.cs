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
    List<GameObject> tilesList;
    GameObject tilesParent;

    Pathfinding pathfinder;

    // Start is called before the first frame update
    void Start()
    {

        if (gridSize.x == 0 || gridSize.y == 0)
        {
            gridSize = new Vector2Int(10, 10);
        }
        tiles = new Tile[100, 100]; // TODO: Hardcoded Tiles array
        tilesList = new List<GameObject>();
        tilesParent = new GameObject();
        tilesParent.name = "Tiles";

        pathfinder = new Pathfinding();

        StartCoroutine(Setup());
    }

    // Update is called once per frame
    void Update()
    {

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

    IEnumerator Setup()
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
                    //GameObject a_tempGO = Instantiate(tile, cellPoint, Quaternion.identity, tilesParent.transform);
                    GameObject a_tempGO = new GameObject();
                    a_tempGO.transform.parent = tilesParent.transform;
                    a_tempGO.transform.position = cellPoint;

                    a_tempGO.gameObject.name = cellPoint.ToString();

                    Tile tileComp = a_tempGO.AddComponent<Tile>();
                    tileComp.owner = this;
                    tileComp.cellPosition = new Vector2Int(x, y);

                    tiles[x + gridSize.x, y + gridSize.y] = a_tempGO.GetComponent<Tile>();
                    tilesList.Add(a_tempGO);
                }
            }
        }

        yield return null;
    }

    public Tile GetTile(Vector2Int a_Cell)
    {
        return tiles[a_Cell.x + gridSize.x, a_Cell.y + gridSize.y];
    }

    public Tile TileFromWorldPosition(Vector3 a_WorldPosition)
    {
        return GetTile((Vector2Int)grid.WorldToCell(a_WorldPosition));
    }

    public List<Tile> GetNeighborTiles(Tile a_Tile)
    {
        List<Tile> NeighboringTiles = new List<Tile>();
        int xCheck;
        int yCheck;

        for (int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    // Right Side
                    xCheck = a_Tile.cellPosition.x + 1;
                    yCheck = a_Tile.cellPosition.y;
                    break;
                case 1:
                    // Left Side
                    xCheck = a_Tile.cellPosition.x;
                    yCheck = a_Tile.cellPosition.y + 1;
                    break;
                case 2:
                    // Top Side
                    xCheck = a_Tile.cellPosition.x - 1;
                    yCheck = a_Tile.cellPosition.y + 1;
                    break;
                case 3:
                    // Bottom Side
                    xCheck = a_Tile.cellPosition.x - 1;
                    yCheck = a_Tile.cellPosition.y;
                    break;
                case 4:
                    // Left Side
                    xCheck = a_Tile.cellPosition.x - 1;
                    yCheck = a_Tile.cellPosition.y - 1;
                    break;
                case 5:
                    // Top Side
                    xCheck = a_Tile.cellPosition.x;
                    yCheck = a_Tile.cellPosition.y - 1;
                    break;
                default:
                    Debug.Log("Too many sides to a tile");
                    xCheck = 0;
                    yCheck = 0;
                    break;
            }

            if (xCheck >= 0 && xCheck < gridSize.x)
            {
                if (yCheck >= 0 && yCheck < gridSize.y)
                {
                    NeighboringTiles.Add(tiles[xCheck, yCheck]);
                }
            }
        }

        return NeighboringTiles;
    }

    public List<Tile> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        return pathfinder.FindPath(startPos, targetPos);
    }
}
