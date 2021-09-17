using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireRateSc : MonoBehaviour
{
    //get the status of the gun ammoCount
    private GameObject gunHolder;
    private float blue_fire_rate;
    private float green_fire_rate;
    private float red_fire_rate;

    private PlayerStatistics playerStats;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");
        gunHolder.transform.GetChild(0).gameObject.SetActive(true);
        gunHolder.transform.GetChild(1).gameObject.SetActive(true);
        gunHolder.transform.GetChild(2).gameObject.SetActive(true);
        blue_fire_rate = gunHolder.transform.GetChild(0).GetComponent<IceShotgunScript>().fireRate;
        green_fire_rate = gunHolder.transform.GetChild(1).GetComponent<GreenadeLauncherScript>().fireRate;
        red_fire_rate = gunHolder.transform.GetChild(2).GetComponent<RedLaserScript>().fireRate;
        gunHolder.transform.GetChild(1).gameObject.SetActive(false);
        gunHolder.transform.GetChild(2).gameObject.SetActive(false);
        playerStats = FindObjectOfType<PlayerStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        runFireRate();
    }

    private float blue_ticks = 0.0f;
    private float green_ticks = 0.0f;
    private float red_ticks = 0.0f;
    //meaning, can shoot
    [HideInInspector] public bool isBlueShoot = true;
    [HideInInspector] public bool isGreenShoot = true;
    [HideInInspector] public bool isRedShoot = true;

    private void runFireRate()
    {
        if(playerStats.currentGun == PlayerStatistics.sBlueGun && !isBlueShoot)
        {
            blue_ticks += Time.deltaTime;
            //Debug.Log($"Time: {blue_ticks}");
            if(blue_ticks >= blue_fire_rate)
            {
                blue_ticks = 0;
                isBlueShoot = true;
            }
        }
        else if (playerStats.currentGun == PlayerStatistics.sGreenGun && !isGreenShoot)
        {
            green_ticks += Time.deltaTime;
            if (green_ticks >= green_fire_rate)
            {
                green_ticks = 0;
                isGreenShoot = true;
            }
        }
        else if (playerStats.currentGun == PlayerStatistics.sRedGun && !isRedShoot)
        {
            red_ticks += Time.deltaTime;
            if (red_ticks >= red_fire_rate)
            {
                red_ticks = 0;
                isRedShoot = true;
            }
        }
    }

    public void refreshAllFireRate()
    {
        isBlueShoot = true;
        isGreenShoot = true;
        isRedShoot = true;
        blue_ticks = 0.0f;
        green_ticks = 0.0f;
        red_ticks = 0.0f;
    }
}
