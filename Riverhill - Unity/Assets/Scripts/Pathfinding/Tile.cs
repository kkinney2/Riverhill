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

    public GameObject tileHighlight_Positive;
    public GameObject tileHighlight_Negative;

    public bool hasCharacter = false;
    public CharacterState characterState;

    // Start is called before the first frame update
    void Start()
    {
        tileHighlight_Positive = Instantiate(GameSettings.Instance.tileHighlight_Positive);
        tileHighlight_Negative = Instantiate(GameSettings.Instance.tileHighlight_Negative);

        tileHighlight_Positive.transform.SetParent(TileManager.Instance.tilesHighlights.transform);
        tileHighlight_Negative.transform.SetParent(TileManager.Instance.tilesHighlights.transform);

        tileHighlight_Positive.transform.position = transform.position;
        tileHighlight_Negative.transform.position = transform.position;

        tileHighlight_Positive.SetActive(false);
        tileHighlight_Negative.SetActive(false);
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
