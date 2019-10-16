using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [Tooltip("Name of the Scene for the CutsceneManager to Find")]
    public string SceneName;
    public GameObject[] Frames;


    private void Start()
    {
        for (int i = 0; i < Frames.Length; i++)
        {
            Frames[i].SetActive(false);
        }
    }

    public void StartScene()
    {
        StartCoroutine(Scene());
    }

    IEnumerator Scene()
    {
        Debug.Log("Scene Started");
        for (int i = 0; i < Frames.Length; i++)
        {
            if (i != 0)
            {
                Frames[i - 1].SetActive(false);
            }
            Frames[i].SetActive(true);
            while (!Input.GetButtonDown("Submit"))
            {
                Debug.Log("Waiting for User Input");
                yield return new WaitForEndOfFrame();
            }
        }

        CutsceneManager.Instance.hasActiveCutscene = false;
        yield return null;
    }
}
