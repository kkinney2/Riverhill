using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteLayering : MonoBehaviour
{
    public List<GameObject> objectsWithRenderer;

    public List<SpriteRenderer> spriteRenderers;

    Level currentLevel;
    Transform UpperBound;
    Transform LowerBound;
    float distBetweenUpperLower_Y;
    float distBetweenUpperLower_Z;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(UpdateRefs());
    }

    IEnumerator UpdateRefs()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            UpdateReferences();
            //yield return new WaitForSeconds(10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Vector3 currentPos = spriteRenderers[i].gameObject.transform.position;
            // Percentage of the level's height
            float heightPercentage = ((UpperBound.position.y - currentPos.y) / distBetweenUpperLower_Y);
            float distFromUpper = heightPercentage * distBetweenUpperLower_Z;

            currentPos = new Vector3(currentPos.x, currentPos.y, UpperBound.position.z - distFromUpper);
            spriteRenderers[i].gameObject.transform.position = currentPos;
        }

        #region Tilemap Stuff
        /*
        float sum = 0;
        int obstacles_OrderLayer = 0;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            //spriteRenderers[i].sortingOrder = (int)(spriteRenderers[i].gameObject.transform.position.y * -100); // 100 helps to round decimal places
            //sum += (int)(spriteRenderers[i].gameObject.transform.position.y * -100);
        }

        float avg = sum / spriteRenderers.Count;

        for (int i = 0; i < tilemapRenderers.Count; i++)
        {
            if (tilemapRenderers[i].gameObject.name == "Ground")
            {
                //tilemapRenderers[i].sortingOrder = int.MinValue;
                //tilemapRenderers[i].sortingOrder = -13824;
            }

            if (tilemapRenderers[i].gameObject.name == "Obstacles")
            {
                //tilemapRenderers[i].sortingOrder = (int)avg;
            }

            if (tilemapRenderers[i].gameObject.name == "Unwalkable")
            {
                //tilemapRenderers[i].sortingOrder = -13824;
            }
        }
        */
        #endregion
    }

    public void UpdateReferences()
    {
        currentLevel = BattleManager.Instance.levels[BattleManager.Instance.currentLevel];

        UpperBound = currentLevel.UpperBound;
        LowerBound = currentLevel.LowerBound;
        distBetweenUpperLower_Y = UpperBound.position.y - LowerBound.position.y;
        distBetweenUpperLower_Z = UpperBound.position.z - LowerBound.position.z;

        objectsWithRenderer = new List<GameObject>();
        spriteRenderers = new List<SpriteRenderer>();

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<SpriteRenderer>() != null)
            {
                spriteRenderers.Add(obj.GetComponent<SpriteRenderer>());
                continue;
            }
        }
    }
}
