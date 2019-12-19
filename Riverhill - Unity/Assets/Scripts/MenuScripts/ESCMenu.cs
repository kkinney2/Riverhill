using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ESCMenu : MonoBehaviour
{
    //sound effects
    public AudioSource escUISound;
    public AudioClip buttonSwitch;
    public AudioClip buttonSelect;

    public GameObject escMenu;

    public BattleManager battleManager;
    public GameObject battleManagerObj;

    public bool escMenuOn;
    public bool escMenuOff;

    // Start is called before the first frame update
    void Start()
    {
        escMenuOn = false;
        escMenuOff = true; //starts off
    }

    // Update is called once per frame
    void Update()
    {
        if (battleManager == null && GameObject.Find("BattleManager") != null)
        {
            battleManagerObj = GameObject.Find("BattleManager");
            battleManager = battleManagerObj.GetComponent<BattleManager>();
        }

        if (battleManager.isInBattle == true && battleManager != null)
        {
            if (Input.GetKeyDown("escape"))
            {
                escUISound.clip = buttonSelect;
                escUISound.Play();

                if (escMenuOff == true) //if off
                {
                    //show this menu
                    escMenu.SetActive(true); //turn on
                    escMenuOn = true; //is now on
                    escMenuOff = false;
                }
                else //if on
                {
                    escMenu.SetActive(false); //turn off
                    escMenuOn = false;
                    escMenuOff = true; //is now off
                }
            }
        }
    }

    public void returnButton()
    {
        escUISound.clip = buttonSelect;
        escUISound.Play();

        StartCoroutine(ReturnButtonDelay());
    }

    private IEnumerator ReturnButtonDelay()
    {
        yield return new WaitForSeconds(0.5f);

        //hide this menu
        escMenu.SetActive(false); //turn off
        escMenuOn = false;
        escMenuOff = true; //is now off
    }

    public void quitGame()
    {
        //application quit
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
