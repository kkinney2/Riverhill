using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void newGame()
    {
        SceneManager.LoadScene("_MainScene_");
    }

    public void loadGame()
    {
        //no scene or existing games to load rn
        Debug.Log("Load Game");
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

}
