using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public EventSystem eventSys;
    private GameObject storeSelect;

    public AudioSource menuUISound;
    public AudioClip buttonSwitch;
    public AudioClip buttonSelect;
    public AudioClip newGameSelect;

    public void Start()
    {
        storeSelect = eventSys.firstSelectedGameObject;

        AudioSource menuUISound = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if(eventSys.currentSelectedGameObject != storeSelect)
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

    public void newGame()
    {
        menuUISound.clip = newGameSelect;
        menuUISound.Play();
        //Debug.Log("Play sound: " + newGameSelect);
        StartCoroutine(NewGameButtonDelay());
    }

    public void loadGame()
    {
        menuUISound.clip = buttonSelect;
        menuUISound.Play();
        //Debug.Log("Play sound: " + buttonSelect);
        StartCoroutine(LoadGameButtonDelay());
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    private IEnumerator NewGameButtonDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(2.5f);
        Debug.Log(Time.time);
        SceneManager.LoadScene("CutScene");
    }

    private IEnumerator LoadGameButtonDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(0.5f);
        Debug.Log(Time.time);
        SceneManager.LoadScene("TurnBasedTest");
    }
}
