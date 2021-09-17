using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugWindowSc : MonoBehaviour
{
    [SerializeField] private GameObject debugWindow;
    private GameSceneHUD gsHUD;
    private PlayerStatistics playerStats;
    private GestureManager gsM;

    //GameCredit instance
    
    // Start is called before the first frame update
    void Start()
    {
        gsHUD = FindObjectOfType<GameSceneHUD>();
        playerStats = FindObjectOfType<PlayerStatistics>();
        gsM = FindObjectOfType<GestureManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void debugTimer(bool isIncrement)
    {
        if (isIncrement)
        {
            gsHUD.game_timer += 30;
        }
        else
        {
            gsHUD.game_timer -= 30;
        }
    }
    public void debugSpeed(bool isIncrement)
    {
        if (isIncrement)
        {
            playerStats.playerSpeed += 5;
        }
        else
        {
            playerStats.playerSpeed -= 5;
        }
    }
    public void debugCash(bool isIncrement)
    {
        if(isIncrement)
        {
            GameCredit.addCurrency(1000);
        }
        else
        {
            GameCredit.DeductCurrency(1000);
        }
    }
    public void infiniteDash()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
        {
            gsM.dash_coolDown = 0;
        }
        else
        {
            gsM.dash_coolDown = gsM.original_dash_coolDown;
        }
    }
    public void infiniteAmmo()
    {
        //get the status of the gun ammoCount
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");

        gunHolder.transform.GetChild(0).gameObject.SetActive(true);
        gunHolder.transform.GetChild(1).gameObject.SetActive(true);
        gunHolder.transform.GetChild(2).gameObject.SetActive(true);
        IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
        blueGun.storedAmmoCount = 9999;
        GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
        greenGun.storedAmmoCount = 999;
        RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
        redGun.storedAmmoCount = 9999;
        switch (playerStats.currentGun)
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
    public void openDebugWindow()
    {
        debugWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeButton()
    {
        debugWindow.SetActive(false);
        Time.timeScale = 1;
    }
}
