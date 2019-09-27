using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileManager owner;

    Grid grid;
    public Vector2Int cellPosition;

    public Tile Parent; // For the a* algorithm, will store what node 
                        // it previously came from so it can trace the shortest path

    public int gCost; // Cost of moving to the next square
    public int hCost; // The distance to the goal from this node

    public int FCost { get { return gCost + hCost; } }

    public List<Tile> neighborTiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // Assigns the NEXT thing to be drawn

        Gizmos.DrawSphere(transform.position, 1.3f);

        Gizmos.color = Color.red;
        if (Application.isPlaying == true)
        {
            for (int i = 0; i < neighborTiles.Count; i++)
            {
                Gizmos.DrawSphere(neighborTiles[i].transform.position, 1);
            }
        }
    }
}
