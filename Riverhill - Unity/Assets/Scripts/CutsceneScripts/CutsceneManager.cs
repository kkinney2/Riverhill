using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] Cutscene_GameObjects;

    Cutscene[] cutscenes;

    public bool hasActiveCutscene = false;

    [Header("Scene Testing")]
    public GameObject TestScene;
    public bool isTestingScene = false;

    [Range(1, 45)]
    [Tooltip("Controls text speed based on a 1/x delay between characters and a 2/x delay on punctuation")]
    public int TextSpeed = 25;

    //cutscene music
    public AudioSource cutsceneMusicAS;
    public AudioClip cutsceneMusic;

    public bool cutsceneMusicIsPlaying;

    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().cutsceneManager = this;
            isTestingScene = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cutsceneMusicAS = GetComponent<AudioSource>();

        cutscenes = new Cutscene[Cutscene_GameObjects.Length];

        for (int i = 0; i < Cutscene_GameObjects.Length; i++)
        {
            cutscenes[i] = Cutscene_GameObjects[i].GetComponent<Cutscene>();
        }
        //  OR
        //StartCoroutine(ObtainCutsceneScripts());

        if (TestScene != null && isTestingScene)
        {
            StartCutscene(TestScene.GetComponent<Cutscene>().SceneName);
        }
        else
        {
            StartCutscene("Test");
        }
    }

    IEnumerator ObtainCutsceneScripts()
    {
        for (int i = 0; i < Cutscene_GameObjects.Length; i++)
        {
            cutscenes[i] = Cutscene_GameObjects[i].GetComponent<Cutscene>();
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public void StartCutscene(string sceneName)
    {
        if (!hasActiveCutscene)
        {
            foreach (Cutscene cutscene in cutscenes)
            {
                if (cutscene.SceneName == sceneName)
                {
                    cutscene.StartScene();
                    hasActiveCutscene = true;
                    cutsceneMusicAS.mute = false;
                    cutsceneMusicAS.Play();
                    cutsceneMusicIsPlaying = true;
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Unable to start new scene '" + sceneName + "' because another scene is still/already started");
        }
    }
}
