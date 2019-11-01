using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //zoom function
    public float zoomInLimit; //5 ?
    public float zoomOutLimit; //35 ?

    //center camera on active player
    private Transform playerTransform; //need to set as active player

    public float offset; //if wanted? going to work with offset vertically for now, since camera is centering around player pivot (at feet?)

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position; //current pos stored in temp var
        temp.x = playerTransform.position.x; //set camera's pos to player's pos
        temp.y = playerTransform.position.y;
        temp.y += offset; //add's offset to cam's x pos
        transform.position = temp; //cam's temp pos set back to cam's current pos

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && ((GetComponent<Camera>().orthographicSize) >= zoomInLimit)) //zoom in
        {
            //GetComponent<Camera>().fieldOfView--;
            camera.orthographicSize -= 1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && ((GetComponent<Camera>().orthographicSize) <= zoomOutLimit)) //zoom out  
        {
            //GetComponent<Camera>().fieldOfView++;
            camera.orthographicSize += 1f;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            camera.orthographicSize = 10f;
        }
    }
}
