using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public int speed = 5;

    public bool moveAgain = true;
    public List<Tile> path = new List<Tile>();

    private LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<LineRenderer>();
        laserLine = GetComponent<LineRenderer>();

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

                Vector3 worldFromScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition); // This is slanted at the 50deg as the camera
                Debug.Log("WorldFromScreen: " + worldFromScreen);
                
                RaycastHit2D hit = Physics2D.Raycast(worldFromScreen, Camera.main.transform.TransformDirection(Vector3.forward), 100);

                Debug.Log("");
                Debug.Log("HitPointName: " + hit.transform.name);
                Debug.Log("");

                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.position = hit.point;

                
                laserLine.SetPosition(0, worldFromScreen);
                laserLine.SetPosition(1, Camera.main.transform.TransformDirection(Vector3.forward) * 100);
                //laserLine.SetPosition(1, hit.point);
                laserLine.enabled = true;


                if (hit.collider != null)
                {
                    Debug.Log("Ping");

                    float rayPercentage = ;





                    Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(hit.point.x, hit.point.y, 0)));
                    Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);

                    Debug.Log("HitPoint: " + hit.point);
                    Debug.Log("HitPointMod: " + (new Vector3(hit.point.x, hit.point.y, 0) - new Vector3(0, 10, 0)));
                    Debug.Log("WorldToCell: " + worldToCell);
                    Debug.Log("TestPoint: " + testPoint);

                    //path = TileManager.Instance.FindPath(transform.position, testPoint);
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
        if (path.Count > 0)
        {
            transform.position = path[path.Count - 1].transform.position;
        }

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
