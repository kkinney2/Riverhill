using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSwitch : MonoBehaviour
{

    public GameObject offCanvas;
    public GameObject onCanvas;
    public GameObject firstObj;

    public AudioSource menuUISound;
    public AudioClip buttonSwitch;
    public AudioClip buttonSelect;

    public void Start()
    {
        AudioSource menuUISound = GetComponent<AudioSource>();
    }

    public void Switch()
    {
        menuUISound.clip = buttonSelect;
        menuUISound.Play();
        //Debug.Log("Play sound: " + buttonSelect);
        StartCoroutine(CreditsButtonDelay());
    }

    private IEnumerator CreditsButtonDelay()
    {
        //Debug.Log(Time.time);
        yield return new WaitForSeconds(0.5f);
        //Debug.Log(Time.time);
        offCanvas.SetActive(true);
        onCanvas.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObj, null);
    }
}
