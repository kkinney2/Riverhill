using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [Tooltip("Name of the Scene for the CutsceneManager to Find")]
    public string SceneName;
    public GameObject[] Frames;

    GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


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
        float tempNum = Camera.main.orthographicSize;
        Camera.main.orthographicSize = 11.2f;
        for (int i = 0; i < Frames.Length; i++)
        {
            if (i != 0)
            {
                Frames[i - 1].SetActive(false);
            }
            Frames[i].SetActive(true);
            while (true)
            {
                // TODO: Extremely high polling number for user input
                yield return new WaitForSeconds(0.00001f);
                if (Input.GetButtonUp("Submit"))
                {
                    break;
                }
            }
        }

        Frames[Frames.Length - 1].SetActive(false);

        gameController.cutsceneManager.hasActiveCutscene = false;
        Camera.main.orthographicSize = tempNum;
        yield return null;
    }
}
