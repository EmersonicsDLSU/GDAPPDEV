using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put this script component in your camera
//Gets how fast the device rotated
public class GyroCamera : MonoBehaviour
{
    void Start()
    {
        //Check if gyro exists; most phones have gyros
        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogError("No gyro in device");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.gyro.enabled)
        {
            Vector3 rotation = Input.gyro.rotationRate;

            //Invert x and y
            rotation.x *= -1;
            rotation.y *= -1;

            transform.Rotate(rotation);
        }
    }
}
