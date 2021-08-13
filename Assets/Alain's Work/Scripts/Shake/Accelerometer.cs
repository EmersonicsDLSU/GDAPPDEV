using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    private PlayerStatistics stats;
    Vector3 acceleration;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Input.acceleration;

        if(acceleration.sqrMagnitude >= 4.0f)
        {
            AudioManagerScript.instance.playSound("Reload");
            stats.ammoCount = 100;
        }
    }
}
