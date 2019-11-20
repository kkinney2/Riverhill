using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathfinding : MonoBehaviour
{
    public int speed = 5;
    public Vector2 range = new Vector2(1, 2);

    public List<Tile> path = new List<Tile>();

    bool hasPath = false;
    public bool isDone = false;

    //walk direction, sprite change
    public SpriteRenderer charSpriteRenderer;
    public bool movingLeft = false;
    public bool movingRight = false;

    //walk sound
    public AudioSource characterSound;
    public AudioClip walkSound;

    private void Awake()
    {
        charSpriteRenderer = GetComponent<SpriteRenderer>();

        AudioSource characterSound = GetComponent<AudioSource>();
    }

    public void Move()
    {
        StartCoroutine(Move_Coroutine());
    }

    public void FindPath_AutoUseInput()
    {
        Vector3 worldFromScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("WorldFromScreen: " + worldFromScreen);

        RaycastHit2D hit = Physics2D.Raycast(worldFromScreen, Camera.main.transform.TransformDirection(Vector3.forward), 100);

        if (hit.collider != null)
        {
            Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(hit.point.x, hit.point.y, 0)));
            Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);

            /*
            Debug.Log("WorldToCell: " + worldToCell);
            Debug.Log("HitPoint: " + hit.point);
            Debug.Log("Test Point: " + testPoint);
            */

            if (worldToCell != null || testPoint != null || TileManager.Instance.GetTileFromWorldPosition(testPoint).GetComponent<Tile>().hasCharacter == false)
            {
                path = TileManager.Instance.FindPath(transform.position, testPoint);
            }

            //path = TileManager.Instance.FindPath(transform.position, testPoint);
            if (path != null)
            {
                hasPath = true;
            }
        }
    }

    public void FindPath(Vector3 a_Position)
    {
        Vector3Int worldToCell = TileManager.Instance.grid.WorldToCell((new Vector3(a_Position.x, a_Position.y, 0)));
        Vector3 testPoint = TileManager.Instance.grid.CellToWorld(worldToCell);

        /*
        Debug.Log("WorldToCell: " + worldToCell);
        Debug.Log("HitPoint: " + hit.point);
        Debug.Log("Test Point: " + testPoint);
        */

        if (worldToCell != null || testPoint != null || TileManager.Instance.GetTileFromWorldPosition(testPoint).GetComponent<Tile>().hasCharacter == false)
        {
            path = TileManager.Instance.FindPath(transform.position, testPoint);
        }

        //path = TileManager.Instance.FindPath(transform.position, testPoint);
        if (path != null)
        {
            hasPath = true;
        }

    }

    public void MoveAlongPath()
    {
        StartCoroutine(FollowPath());
    }

    #region Movement

    /// <summary>
    /// Move_Coroutine takes in the user's click and then attempts to move the player
    /// to the selected location, if possible.
    /// </summary>
    /// <returns></returns>
    IEnumerator Move_Coroutine()
    {
        Debug.Log("Move Coroutine");
        hasPath = false;
        yield return new WaitForSeconds(0.3f);
        while (!hasPath)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Mouse Click");
                FindPath_AutoUseInput();
            }
            // TODO: Better polling numbers while waiting for user input?
            yield return new WaitForSeconds(0.001f);
        }

        Debug.Log("PathLength: " + path.Count);
        if (path.Count != 0 && path.Count >= range.x && path.Count <= range.y)
        {
            MoveAlongPath();
        }

        yield break;
    }

    /// <summary>
    /// Follows Path
    /// </summary>
    /// <returns></returns>
    IEnumerator FollowPath()
    {
        if (path == null)
        {
            Debug.Log("FollowPath() has no path to follow");
            yield break;
        }

        // Teleport to Position
        //transform.position = path[path.Count - 1].transform.position;

        // Travel towards Position

        for (int i = 0; i < path.Count; i++)
        {
            if (i == range.y)
            {
                isDone = true;
                break;
            }
            Transform target = path[i].transform;

            // TODO: Cheap way of stopping characters from overlapping
            if (TileManager.Instance.GetTileFromWorldPosition(target.position).hasCharacter)
            {
                continue;
            }

            Vector3 zeroZ_TargetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

            while (Vector3.Distance(transform.position, zeroZ_TargetPos) > 0.001f)
            //while ((transform.position.x - target.position.x) > 0.001f && (transform.position.y - target.position.y) > 0.001f)
            {
                // Move our position a step closer to the target.
                float step = speed * Time.deltaTime; // calculate distance to move

                //Adjust for sprite layering - Maintain Target, but allow for adjustment in the Z
                zeroZ_TargetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

                #region Sprite Flipping
                //if moving left, set movingLeft = true;
                if (target.position.x < transform.position.x)
                {
                    movingLeft = true;
                }
                //if moving right, set movingRight = true;
                if (target.position.x > transform.position.x)
                {
                    movingRight = true;
                }
                //WORKING WELL? flip sprite in direction of movement...
                if (movingLeft == true)
                {
                    charSpriteRenderer.flipX = false;
                    movingLeft = false;
                }
                if (movingRight == true)
                {
                    charSpriteRenderer.flipX = true;
                    movingRight = false;
                }
                #endregion

                characterSound.clip = walkSound;
                characterSound.Play();

                transform.position = Vector3.MoveTowards(transform.position, zeroZ_TargetPos, step);
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), step);

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(0.5f);
        }

        path = new List<Tile>();
        isDone = true;
        yield break;
    }

    #region Old FollowPath()
    /*
    IEnumerator FollowPath()
    {
        if (path == null)
        {
            Debug.Log("FollowPath() has no path to follow");
            yield break;
        }

        // Teleport to Position
        //transform.position = path[path.Count - 1].transform.position;

        // Travel towards Position

        for (int i = 0; i < path.Count; i++)
        {
            if (i == range.y)
            {
                isDone = true;
                break;
            }
            Transform target = path[i].transform;

            // TODO: Cheap way of stopping characters from overlapping
            if (TileManager.Instance.GetTileFromWorldPosition(target.position).hasCharacter)
            {
                continue;
            }

            while (Vector3.Distance(transform.position, target.position) > 0.001f)
            //while ((transform.position.x - target.position.x) > 0.001f && (transform.position.y - target.position.y) > 0.001f)
            {
                // Move our position a step closer to the target.
                float step = speed * Time.deltaTime; // calculate distance to move

                #region Sprite Flipping
                //if moving left, set movingLeft = true;
                if (target.position.x < transform.position.x)
                {
                    movingLeft = true;
                }
                //if moving right, set movingRight = true;
                if (target.position.x > transform.position.x)
                {
                    movingRight = true;
                }
                //WORKING WELL? flip sprite in direction of movement...
                if (movingLeft == true)
                {
                    charSpriteRenderer.flipX = false;
                    movingLeft = false;
                }
                if (movingRight == true)
                {
                    charSpriteRenderer.flipX = true;
                    movingRight = false;
                }
                #endregion

                characterSound.clip = walkSound;
                characterSound.Play();

                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), step);

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(0.5f);
        }

        path = new List<Tile>();
        isDone = true;
        yield break;
    }
    */
    #endregion

    #endregion

    /// <summary>
    /// Highlights the character's path while the character
    /// is selected
    /// </summary>
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
