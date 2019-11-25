using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsMenu : MonoBehaviour
{

    public EventSystem eventSys;
    private GameObject storeSelect;

    public void Start()
    {
        storeSelect = eventSys.firstSelectedGameObject;
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

}
