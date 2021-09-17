using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put this script component in your gameObject
public class AccelGyroHandlerScript : MonoBehaviour
{
    public float speed = 5;
    //Min change in x before we start moving
    public float minChange = 0.2f;

    private void Start()
    {
        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.Log("No gyro in device.");
        }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void FixedUpdate()
    {
        //use only for gyro
        //Get the current rotation of the phone and convert it
        Quaternion rot = GyroToUnity(Input.gyro.attitude);
        //Assign the rotation to the object
        transform.rotation = rot;

        /*
        //use only for accelerometer
        //Gets how much the phone is tilted in the X axis
        //this is used better to detect shake mechanic in your game
        float x = Input.acceleration.x;
        if(Mathf.Abs(x) >= minChange)
        {
            x *= speed * Time.deltaTime;
            //Moves the character by how much the phone is tilted
            transform.Translate(x, 0, 0);
        }
        */
    }

    private void Ondraw()
    {
        //Only draw Ray when game is running
        if (Application.isPlaying)
        {
            //Draw a red ray from the origin with the same direction as the accelerometer values
            //Get acceloremeter values
            Debug.DrawRay(transform.position, Input.acceleration.normalized, Color.red);
        }
    }
}
