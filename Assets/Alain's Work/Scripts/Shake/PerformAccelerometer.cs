using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformAccelerometer : MonoBehaviour
{
    private PlayerStatistics stats;

    // Start is called before the first frame update
    private void Start()
    {
        ShakeScript.Instance.OnShake += ActionToRunWhenShakingDevice;
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    private void OnDestroy()
    {
        ShakeScript.Instance.OnShake -= ActionToRunWhenShakingDevice;
    }
    private void ActionToRunWhenShakingDevice()
    {
        Debug.Log("NAALOG NA YUNG SELPON");
        if(stats.ammoCount < 70)
        {
            stats.ammoCount = 100;
        }
    }
    private void ActionToRunWhenDoneFading()
    {

    }
}
