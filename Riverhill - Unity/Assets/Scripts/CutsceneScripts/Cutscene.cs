using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
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
        for (int i = 0; i < Frames.Length; i++)
        {
            if (i != 0)
            {
                Frames[i - 1].SetActive(false);
            }
            Frames[i].SetActive(true);
            while (!Input.GetButtonDown("Submit"))
            {
                yield return new WaitForEndOfFrame();
            }
        }

        CutsceneManager.Instance.hasActiveCutscene = false;
        yield return null;
    }
}
