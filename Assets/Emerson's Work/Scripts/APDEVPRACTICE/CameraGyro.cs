using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGyro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Input.gyro.enabled = false;
            Debug.Log("no gyro");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.gyro.enabled)
        {
            //this calculation is in degrees
            Vector3 rotation = Input.gyro.rotationRate;

            //invert x and y
            rotation.x += -1;
            rotation.y += -1;

            transform.Rotate(rotation);
        }
    }
}
