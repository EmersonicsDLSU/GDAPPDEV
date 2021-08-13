using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelGyroHandleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Input.gyro.enabled = false;
            Debug.Log("no gyro");
        }
    }
    private float speed = 5;
    private float minChange = 0.2f;

    private static Quaternion GyroToUnity(Quaternion q)
    {
        //invert z and w values
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.gyro.enabled)
        {
            Quaternion rotation = GyroToUnity(Input.gyro.attitude);
            transform.rotation = rotation;
        }

        /*Vector3 accel = Input.acceleration;
        if(Mathf.Abs(accel.x) >= minChange)
        {
            accel.x *= speed * Time.deltaTime;
            transform.Translate(accel.x, 0, 0);
        }*/
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Debug.DrawRay(transform.position, Input.acceleration.normalized, Color.red);
            
        }
    }
}
