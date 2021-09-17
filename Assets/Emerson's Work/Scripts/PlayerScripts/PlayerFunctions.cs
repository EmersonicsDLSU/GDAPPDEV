using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private PlayerStatistics playerStats;
    private PlayerAnimation plyAnim;


    private void Start()
    {
        playerStats = GetComponent<PlayerStatistics>();
        plyAnim = GetComponent<PlayerAnimation>(); ;
    }

    public void respawnPlayer()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").GetComponent<Transform>();
        //respawns the player and relcate it to the recorded spawnPoint
        refreshPlayer();
        this.transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, 0.0f);
        playerStats.isDead = false;
        playerStats.vulnerable = true;
        //Debug.Log(playerStats.playerMaxHealth);
        playerStats.PlayerHealth = playerStats.playerMaxHealth;
        plyAnim.deadAnimation();
    }

    public void refreshPlayer()
    {
        playerStats.isDead = false;
        plyAnim.deadAnimation();
        playerStats.vulnerable = true;
        playerStats.PlayerHealth = playerStats.playerMaxHealth;
        refreshAmmo();
    }

    //refreshes all the gun's ammoCount
    public void refreshAmmo()
    {
        //get the status of the gun ammoCount
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");

        gunHolder.transform.GetChild(0).gameObject.SetActive(true);
        gunHolder.transform.GetChild(1).gameObject.SetActive(true);
        gunHolder.transform.GetChild(2).gameObject.SetActive(true);
        IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
        blueGun.currentAmmoCount = blueGun.magazine;
        blueGun.storedAmmoCount = 60;
        GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
        greenGun.currentAmmoCount = greenGun.magazine;
        greenGun.storedAmmoCount = 60;
        RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
        redGun.currentAmmoCount = redGun.magazine;
        redGun.storedAmmoCount = 60;
        switch(playerStats.currentGun)
        {
            case PlayerStatistics.sBlueGun:
                gunHolder.transform.GetChild(1).gameObject.SetActive(false);
                gunHolder.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case PlayerStatistics.sGreenGun:
                gunHolder.transform.GetChild(0).gameObject.SetActive(false);
                gunHolder.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case PlayerStatistics.sRedGun:
                gunHolder.transform.GetChild(0).gameObject.SetActive(false);
                gunHolder.transform.GetChild(1).gameObject.SetActive(false);
                break;
        }
    }

    private float vulnerable_timer = 0.0f;
    public void vulnerabilityTimer()
    {
        //create a timer for the invulnerability of the character
        if(!playerStats.vulnerable)
        {
            vulnerable_timer += Time.deltaTime;
            if(vulnerable_timer >= GeneralAnimationProperty.player_vulnerable_duration)
            {
                vulnerable_timer = 0.0f;
                playerStats.vulnerable = true;
            }
        }
    }
}
