using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteLayering : MonoBehaviour
{
    public List<GameObject> objectsWithRenderer;

    public List<SpriteRenderer> spriteRenderers;
    public List<TilemapRenderer> tilemapRenderers;
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
        float sum = 0;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].sortingOrder = (int)(spriteRenderers[i].gameObject.transform.position.y * 100); // 100 helps to round decimal places
            sum += (int)(spriteRenderers[i].gameObject.transform.position.y * 100);
        }

        float avg = sum / spriteRenderers.Count;

        for (int i = 0; i < tilemapRenderers.Count; i++)
        {
            if (tilemapRenderers[i].gameObject.name == "Ground")
            {
                //tilemapRenderers[i].sortingOrder = int.MinValue;
                tilemapRenderers[i].sortingOrder = -13824;
            }

            if (tilemapRenderers[i].gameObject.name == "Obstacles")
            {
                tilemapRenderers[i].sortingOrder = (int)avg;
            }

            if (tilemapRenderers[i].gameObject.name == "Unwalkable")
            {
                tilemapRenderers[i].sortingOrder = -13824;
            }
        }
    }

    public void UpdateReferences()
    {
        objectsWithRenderer = new List<GameObject>();
        spriteRenderers = new List<SpriteRenderer>();
        tilemapRenderers = new List<TilemapRenderer>();

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {

            if (obj.GetComponent<SpriteRenderer>() != null)
            {
                spriteRenderers.Add(obj.GetComponent<SpriteRenderer>());
                continue;
            }

            if (obj.GetComponent<TilemapRenderer>() != null)
            {
                tilemapRenderers.Add(obj.GetComponent<TilemapRenderer>());
                continue;
            }
        }
    }
}
