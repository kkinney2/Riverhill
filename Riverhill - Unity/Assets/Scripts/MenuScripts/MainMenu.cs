using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public EventSystem eventSys;
    private GameObject storeSelect;

    public void Start()
    {
        storeSelect = eventSys.firstSelectedGameObject;
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
        SceneManager.LoadScene("_MainScene_");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("_MainScene_");
        //Debug.Log("Load Game");
    }

    public void quitGame()
    {
        Application.Quit();
        //Debug.Log("Quit Game");
    }

}
