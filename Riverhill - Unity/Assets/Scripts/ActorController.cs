using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move()
    {
        bool hasPath = false;
        List<Tile> path;
        RaycastHit2D[] results;

        while (!hasPath)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Input.mousePosition + Vector3.back, Vector3.forward, Mathf.Infinity, 1))
                {


                    TileManager.Instance.FindPath(transform.position, )
                }
            }
        }

        yield return null;
    }
}
