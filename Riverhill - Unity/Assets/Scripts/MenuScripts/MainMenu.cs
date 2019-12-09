using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    GameController gameController;

    public EventSystem eventSys;
    private GameObject storeSelect;

    //sound effects
    public AudioSource menuUISound;
    public AudioClip buttonSwitch;
    public AudioClip buttonSelect;
    public AudioClip newGameSelect;

    //music
    GameObject panelWithMusic;
    AudioSource panelWithMusicAS;

    private static CutsceneManager cutsceneManager;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void Start()
    {
        storeSelect = eventSys.firstSelectedGameObject;

        AudioSource menuUISound = GetComponent<AudioSource>();
        
        if (cutsceneManager.hasActiveCutscene == false && cutsceneManager.cutsceneMusicIsPlaying == true)
        {
            cutsceneManager.cutsceneMusicAS.enabled = false;
            cutsceneManager.cutsceneMusicIsPlaying = false;
        }
        
        panelWithMusic = GameObject.Find("Panel_MainMenu");
        panelWithMusicAS = panelWithMusic.GetComponent<AudioSource>();
        panelWithMusicAS.enabled = true;
        //panelWithMusicAS.Play();
    }

    public void Update()
    {
        if (eventSys.currentSelectedGameObject != storeSelect)
        {
            if (eventSys.currentSelectedGameObject == null)
            {
                eventSys.SetSelectedGameObject(storeSelect);
            }

            else
            {
                storeSelect = eventSys.currentSelectedGameObject;
            }
        }
    }

    #region New Game
    public void newGame()
    {
        //panelWithMusicAS.Stop();
        panelWithMusicAS.enabled = false;
        menuUISound.clip = newGameSelect;
        menuUISound.Play();
        //Debug.Log("Play sound: " + newGameSelect);
        StartCoroutine(NewGameButtonDelay());
    }

    private IEnumerator NewGameButtonDelay()
    {
        //Debug.Log(Time.time);
        yield return new WaitForSeconds(3f);
        //Debug.Log(Time.time);
        //SceneManager.LoadScene("CutScene");
        gameController.NewGame();

        // TODO: Disabling "Hardcoded"
        //gameObject.transform.parent.transform.parent.GetComponent<Canvas>().enabled = false;
        gameObject.SetActive(false);
    }
    #endregion

    #region Load Game
    public void loadGame()
    {
        menuUISound.clip = buttonSelect;
        menuUISound.Play();
        //Debug.Log("Play sound: " + buttonSelect);
        StartCoroutine(LoadGameButtonDelay());
    }

    private IEnumerator LoadGameButtonDelay()
    {
        //Debug.Log(Time.time);
        yield return new WaitForSeconds(0.5f);
        //Debug.Log(Time.time);
        //SceneManager.LoadScene("TurnBasedTest");
        gameController.LoadGame();
    }
    #endregion

    #region Level Selection
    public void levelSelect()
    {
        menuUISound.clip = buttonSelect;
        menuUISound.Play();
        //Debug.Log("Play sound: " + buttonSelect);
        StartCoroutine(LevelSelectButtonDelay());
    }

    private IEnumerator LevelSelectButtonDelay()
    {
        //Debug.Log(Time.time);
        yield return new WaitForSeconds(0.5f);
        //Debug.Log(Time.time);
        //SceneManager.LoadScene("TurnBasedTest");
        gameController.Coroutine_LevelSelection();
    }
    #endregion

    public void Load_Level(string a_Level)
    {
        menuUISound.clip = buttonSelect;
        menuUISound.Play();
        //Debug.Log("Play sound: " + buttonSelect);
        StartCoroutine(LoadLevelButtonDelay(a_Level));
    }

    private IEnumerator LoadLevelButtonDelay(string a_Level)
    {
        yield return new WaitForSeconds(0.5f);

        gameController.LoadLevel(a_Level);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
