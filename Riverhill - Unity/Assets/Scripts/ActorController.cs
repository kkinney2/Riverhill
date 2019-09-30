using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public int speed = 5;

    public bool moveAgain = true;
    public List<Tile> path = new List<Tile>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveINFINITE());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveINFINITE()
    {
        while (true)
        {
            if (moveAgain == true)
            {
                StartCoroutine(Move());
                moveAgain = false;
            }


            yield return new WaitForFixedUpdate();
        }
        
    }

    IEnumerator Move()
    {
        Debug.Log("Move Coroutine");

        bool hasPath = false;
        //List<Tile> path = new List<Tile>();

        while (!hasPath)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Mouse Click");

                Vector3 testPoint = TileManager.Instance.grid.CellToWorld(TileManager.Instance.grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));

                /*
                Debug.Log("Input.mousePosition: " + Input.mousePosition);
                Debug.Log("Input.mousePosition_Revised: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Debug.Log("CellPos: " + TileManager.Instance.grid.WorldToCell(Input.mousePosition));
                Debug.Log("CellPos_Revised: " + TileManager.Instance.grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));

                Debug.Log("TestPoint: " + testPoint);
                */
                
                if (Physics2D.Raycast(testPoint + Vector3.back*5f, Vector3.forward, Mathf.Infinity, 1))
                {
                    Debug.Log("Ping");

                    path = TileManager.Instance.FindPath(transform.position, testPoint);
                    if (path!= null)
                    {
                        hasPath = true;
                        Debug.Log("Has Path");
                        break;
                    }
                    
                    
                }
            }
            yield return new WaitForSeconds(0.001f);
        }

        /*bool hasReachedTarget = false;

        while (!hasReachedTarget)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Transform target = path[i].transform;

                while (Vector3.Distance(transform.position, target.position) < 0.001f)
                {
                    // Move our position a step closer to the target.
                    float step = speed * Time.deltaTime; // calculate distance to move
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);

                    yield return new WaitForFixedUpdate();
                }
                
            }
        }*/

        Debug.Log("PathLength: " + path.Count);

        transform.position = path[path.Count-1].transform.position;

        yield return new WaitForSeconds(1);

        moveAgain = true;
        yield return null;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (Application.isPlaying == true)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Gizmos.DrawSphere(path[i].transform.position, 1);
            }
        }
    }
}
