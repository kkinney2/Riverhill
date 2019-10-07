using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public int speed = 5;
    public Vector2Int range = new Vector2Int(1, 2);

    public List<Tile> path = new List<Tile>();

    public bool attack = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        StartCoroutine(Move_Coroutine());
    }

    #region Movement

    IEnumerator Move_Coroutine()
    {
        Debug.Log("Move Coroutine");

        bool hasPath = false;

        while (!hasPath)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Mouse Click");

                Vector3 worldFromScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("WorldFromScreen: " + worldFromScreen);

                RaycastHit2D hit = Physics2D.Raycast(worldFromScreen, Camera.main.transform.TransformDirection(Vector3.forward), 100);

                if (hit.collider != null)
                {
                    Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(hit.point.x, hit.point.y, 0)));
                    Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);

                    path = TileManager.Instance.FindPath(transform.position, testPoint);
                    if (path != null)
                    {
                        hasPath = true;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.001f);
        }

        Debug.Log("PathLength: " + path.Count);
        if (path.Count != 0 && path.Count >= range.x && path.Count <= range.y)
        {
            StartCoroutine(MoveAlongPath());
        }

        yield break;
    }

    IEnumerator MoveAlongPath()
    {
        // Teleport to Position
        //transform.position = path[path.Count - 1].transform.position;

        // Travel towards Position
        for (int i = 0; i < path.Count; i++)
        {
            Transform target = path[i].transform;

            while (Vector3.Distance(transform.position, target.position) > 0.001f)
            {
                // Move our position a step closer to the target.
                float step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }

    #endregion

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
