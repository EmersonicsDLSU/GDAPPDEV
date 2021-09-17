using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    private PlayerStatistics playerStats;
    Vector3 acceleration;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Input.acceleration;

        if(acceleration.sqrMagnitude >= 4.0f)
        {
            ActionToRunWhenShakingDevice();
        }
    }


    private void ActionToRunWhenShakingDevice()
    {
        Debug.Log("Gun Reloaded");

        int addedAmmo = 0;
        //get the status of the gun ammoCount
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");
        switch (playerStats.currentGun)
        {
            case PlayerStatistics.sBlueGun:
                IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
                if (blueGun.storedAmmoCount > 0)
                {
                    updateAmmoCount(ref addedAmmo, blueGun.magazine, blueGun.currentAmmoCount, blueGun.storedAmmoCount);
                    blueGun.storedAmmoCount -= addedAmmo;
                    blueGun.currentAmmoCount += addedAmmo;
                    AudioManagerScript.instance.playSound("Reload");
                }
                else
                {
                    Debug.Log("No more blue ammo for reload!");
                }
                break;
            case PlayerStatistics.sGreenGun:
                GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
                if (greenGun.storedAmmoCount > 0)
                {
                    updateAmmoCount(ref addedAmmo, greenGun.magazine, greenGun.currentAmmoCount, greenGun.storedAmmoCount);
                    greenGun.storedAmmoCount -= addedAmmo;
                    greenGun.currentAmmoCount += addedAmmo;
                    AudioManagerScript.instance.playSound("Reload");
                }
                else
                {
                    Debug.Log("No more green ammo for reload!");
                }
                break;
            case PlayerStatistics.sRedGun:
                RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
                if (redGun.storedAmmoCount > 0)
                {
                    updateAmmoCount(ref addedAmmo, redGun.magazine, redGun.currentAmmoCount, redGun.storedAmmoCount);
                    redGun.storedAmmoCount -= addedAmmo;
                    redGun.currentAmmoCount += addedAmmo;
                    AudioManagerScript.instance.playSound("Reload");
                }
                else
                {
                    Debug.Log("No more red ammo for reload!");
                }
                break;
        }
    }
    private void updateAmmoCount(ref int addedAmmo, int magazine, int currentAmmoCount, int storedAmmoCount)
    {
        //this is the difference
        addedAmmo = magazine - currentAmmoCount;
        //if it's the last reload
        if(addedAmmo >= storedAmmoCount)
        {
            //add the remaining bullets
            addedAmmo = storedAmmoCount;
        }
    }
}
