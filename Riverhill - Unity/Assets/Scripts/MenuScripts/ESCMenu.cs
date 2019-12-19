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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        battleManagerObj = GameObject.Find("BattleManager");
        battleManager = battleManagerObj.GetComponent<BattleManager>();

        if (battleManager.isInBattle == true)
        {
            if (Input.GetKey("escape"))
            {
                //show this menu
                escMenu.SetActive(true);
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
        escMenu.SetActive(false);
    }

    public void quitGame()
    {
        //application quit
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
