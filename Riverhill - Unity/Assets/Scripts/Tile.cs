using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Grid grid;

    Tile[] neighbors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetNeighbors()
    {
        neighbors = new Tile[6];

        neighbors[0] = (grid.GetComponent<GridLayout>().LocalToCell + new Vector3 (1,0,0));
    }
}
