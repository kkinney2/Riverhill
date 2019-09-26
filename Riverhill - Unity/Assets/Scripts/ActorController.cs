using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Move()
    {
        bool hasPath = false;
        List<Tile> path = new List<Tile>();

        while (!hasPath)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Input.mousePosition + Vector3.back, Vector3.forward, Mathf.Infinity, 1))
                {
                    Vector3Int cellPos = TileManager.Instance.grid.WorldToCell(Input.mousePosition);

                    path = TileManager.Instance.FindPath(transform.position, TileManager.Instance.grid.CellToWorld(cellPos));
                    hasPath = true;
                }
            }
        }

        bool hasReachedTarget = false;

        while (!hasReachedTarget)
        {
            for (int i = 0; i < path.Count; i++)
            {
                // Move our position a step closer to the target.
                float step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, target.position) < 0.001f)
                {
                    // Swap the position of the cylinder.
                    target.position *= -1.0f;
                }
            }
        }

        yield return null;
    }
}
