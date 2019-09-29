using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSwitch : MonoBehaviour
{

    public GameObject offCanvas;
    public GameObject onCanvas;
    public GameObject firstObj;

    public void Switch()
    {

        offCanvas.SetActive(true);
        onCanvas.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstObj, null);

    }

}
