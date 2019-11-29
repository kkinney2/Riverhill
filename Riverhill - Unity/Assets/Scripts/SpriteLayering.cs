using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteLayering : MonoBehaviour
{
    public List<GameObject> objectsWithRenderer;

    public List<SpriteRenderer> spriteRenderers;
    public List<SpriteRenderer> characters;
    public List<SpriteRenderer> tileHighlights;

    Level currentLevel;
    Transform UpperBound;
    Transform LowerBound;
    float distBetweenUpperLower_Y;
    float distBetweenUpperLower_Z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.Instance != null)
        {
            UpdateReferences();

            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                Vector3 currentPos = spriteRenderers[i].gameObject.transform.position;
                // Percentage of the level's height
                float heightPercentage = ((UpperBound.position.y - currentPos.y) / distBetweenUpperLower_Y);
                float distFromUpper = heightPercentage * distBetweenUpperLower_Z;

                currentPos = new Vector3(currentPos.x, currentPos.y, UpperBound.position.z - distFromUpper);
                spriteRenderers[i].gameObject.transform.position = currentPos;
            }
        }
        
        

        /*
        for (int i = 0; i < characters.Count; i++)
        {
            Vector3 currentPos = characters[i].gameObject.transform.position;
            // Percentage of the level's height
            float heightPercentage = ((UpperBound.position.y - currentPos.y) / distBetweenUpperLower_Y);
            float distFromUpper = heightPercentage * distBetweenUpperLower_Z;

            currentPos = new Vector3(currentPos.x, currentPos.y, UpperBound.position.z - distFromUpper);
            characters[i].gameObject.transform.position = currentPos;
        }

        /*
        for (int i = 0; i < tileHighlights.Count; i++)
        {
            Vector3 currentPos = tileHighlights[i].gameObject.transform.position;
            // Percentage of the level's height
            float heightPercentage = ((UpperBound.position.y - currentPos.y) / distBetweenUpperLower_Y);
            float distFromUpper = heightPercentage * distBetweenUpperLower_Z;

            currentPos = new Vector3(currentPos.x, currentPos.y, UpperBound.position.z - distFromUpper + 0.1f);
            tileHighlights[i].gameObject.transform.position = currentPos;
        }
        */
    }

    public void UpdateReferences()
    {
        currentLevel = BattleManager.Instance.currentLevel;
        if (currentLevel != null)
        {
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

                    if (obj.CompareTag("Player") || obj.CompareTag("Enemy"))
                    {
                        characters.Add(obj.GetComponent<SpriteRenderer>());
                    }
                    /*
                    else if(obj.CompareTag("TileHighlight"))
                    {
                        tileHighlights.Add(obj.GetComponent<SpriteRenderer>());
                    }
                    */
                    continue;
                }
            }
        } 
    }
}
