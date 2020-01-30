using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 3f;
    public float panDetect = 30f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float x = Camera.main.transform.position.x;
        float y = Camera.main.transform.position.y;
        float z = Camera.main.transform.position.z;

        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        if(Input.GetKey(KeyCode.W) || mousePositionY < Screen.height && Screen.height - mousePositionY < panDetect) {
            z += panSpeed;
        }

        if(Input.GetKey(KeyCode.S) || mousePositionY > 0 && mousePositionY < panDetect) {
            z -= panSpeed;
        }

        if(Input.GetKey(KeyCode.A) || mousePositionX > 0 && mousePositionX < panDetect) {
            x -= panSpeed;
        }

        if(Input.GetKey(KeyCode.D) || mousePositionX < Screen.width && Screen.width - mousePositionX < panDetect) {
            x += panSpeed;
        }       

        Camera.main.transform.position = new Vector3(x, y, z);
    }
}
