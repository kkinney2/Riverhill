using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //zoom function
    public float zoomInLimit; //5 ?
    public float zoomOutLimit; //30 ?

    public bool hasTarget = false;

    //center camera on active player
    private Transform targetTransform; //need to set as active player

    Vector3 originalPosition;

    public float offset; //if wanted? going to work with offset vertically for now, since camera is centering around player pivot (at feet?)

    private Camera myCamera;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            Vector3 temp = transform.position; //current pos stored in temp var
            temp.x = targetTransform.position.x; //set camera's pos to player's pos
            temp.y = targetTransform.position.y;
            temp.y += offset; //add's offset to cam's x pos
            transform.position = temp; //cam's temp pos set back to cam's current pos

            if (Input.GetAxis("Mouse ScrollWheel") > 0 && ((GetComponent<Camera>().orthographicSize) >= zoomInLimit)) //zoom in
            {
                //GetComponent<Camera>().fieldOfView--;
                myCamera.orthographicSize -= 1f;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && ((GetComponent<Camera>().orthographicSize) <= zoomOutLimit)) //zoom out  
            {
                //GetComponent<Camera>().fieldOfView++;
                myCamera.orthographicSize += 1f;
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                myCamera.orthographicSize = 10f;
            }
        }
    }

    public void Reset()
    {
        targetTransform = null;
        hasTarget = false;

        transform.position = originalPosition;
    }

    public void FindPlayer()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        hasTarget = true;
    }
}

