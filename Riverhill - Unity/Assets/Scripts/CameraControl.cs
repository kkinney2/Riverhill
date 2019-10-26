using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomInLimit;
    public float zoomOutLimit;
    //center camera on active player

    //zoom function, centered on active player

    //want camera to follow the active player as they move

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && ((GetComponent<Camera>().orthographicSize) >= zoomInLimit)) //zoom in
        {
            //GetComponent<Camera>().fieldOfView--;
            GetComponent<Camera>().orthographicSize -= 1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && ((GetComponent<Camera>().orthographicSize) <= zoomOutLimit)) //zoom out  
        {
            //GetComponent<Camera>().fieldOfView++;
            GetComponent<Camera>().orthographicSize += 1f;
        }   
    }
}
