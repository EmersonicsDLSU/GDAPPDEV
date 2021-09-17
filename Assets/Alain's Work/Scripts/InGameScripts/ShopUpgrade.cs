using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopUpgrade : MonoBehaviour
{

    public enum GunTypes { Blue = 0, Green, Red};
    //shotgun Room for upgrades
    private float[] shotgunDamage = { 10, 15, 20, 25, 30 };
    private float[] shotgunFireRate = { 1, 0.9f, 0.85f, 0.8f, 0.75f };
    private int[] shotgunMagazine = { 8, 9, 10, 11, 12 };
    //grenade Launcher Room for upgrades
    private float[] grenadeLauncherDamage = { 15, 20, 25, 30, 40 };
    private float[] grenadeLauncherFireRate = { 1.5f, 1.4f, 1.3f, 1.2f, 1.1f };
    private int[] grenadeLauncherMagazine = { 5, 6, 7, 8, 10 };
    //Laser gun Room for upgrades
    private float[] lasergunDamage = { 5, 7, 9, 12, 15 };
    private float[] lasergunFireRate = { 0.5f, 0.45f, 0.4f, 0.35f, 0.3f };
    private int[] lasergunMagazine = { 20, 24, 28, 32, 35 };
    //guns current levels; blue, green, red index
    [HideInInspector] public int[] gunDamageLvl = { 1, 1, 1};
    [HideInInspector] public int[] gunFireRateLvl = { 1, 1, 1};
    [HideInInspector] public int[] gunMagazineLvl = { 1, 1, 1};
    //gun properties
    private const int MAX_LEVEL = 5;

    [HideInInspector] public static ShopUpgrade Instance;
    private List<GameObject> guns = new List<GameObject>();

    private GameCredit credit;

    private float[] blueCost = { 400, 1000, 1400, 1800, 0 }; //this is the cost per level
    private float[] greenCost = { 500, 1200, 1600, 2400, 0 }; //this is the cost per level
    private float[] redCost = { 400, 900, 1300, 1800, 0 }; //this is the cost per level
    

    private void OnEnable()
    {
        //Time.timeScale = 0;
    }
    private void OnDisable()
    {
        //Time.timeScale = 1;
    }

    public void Awake()
    {
        //assigns the one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //destroys the duplicate gameObject 
            Destroy(gameObject);
            Debug.Log("Destroy this");
        }
        
    }
    private void Start()
    {
        initializeGun();
        AudioManagerScript.instance.playSound("ShopMusic");
        
    }

    public void Update()
    {
        /*
        Debug.Log("Damage" + gunDamageLvl[0] + ", " + gunDamageLvl[1] + ", " + gunDamageLvl[2]);
        Debug.Log("FireRate" + gunFireRateLvl[0] + ", " + gunFireRateLvl[1] + ", " + gunFireRateLvl[2]);
        Debug.Log("Magazine" + gunMagazineLvl[0] + ", " + gunMagazineLvl[1] + ", " + gunMagazineLvl[2]);
        */
    }

    public void initializeGun()
    {
        if(GameObject.FindGameObjectWithTag("Gun") != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Gun");
            guns.Add(go.transform.GetChild(0).gameObject); //ice shotgun
            guns.Add(go.transform.GetChild(1).gameObject); //greenade launcher
            guns.Add(go.transform.GetChild(2).gameObject); //red laser
            go.transform.GetChild(0).gameObject.SetActive(true);
            go.transform.GetChild(1).gameObject.SetActive(true);
            go.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void UpgradeGunDamage(int type)
    {
        Text buttonText = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        switch (type)
        {
            case (int)GunTypes.Blue:
                IceShotgunScript gun = GameObject.FindObjectOfType<IceShotgunScript>();
                if (gunDamageLvl[0] < MAX_LEVEL && GameCredit.gameMoney >= blueCost[gunDamageLvl[0] - 1])
                {
                    GameCredit.DeductCurrency(blueCost[gunDamageLvl[0] - 1]);
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunDamageLvl[0] < MAX_LEVEL)
                    {
                        ++this.gunDamageLvl[0];
                        gun.bulletDamage = shotgunDamage[this.gunDamageLvl[0] - 1];
                        buttonText.text = "Upgrade Damage\n" + blueCost[gunDamageLvl[0] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunDamageLvl[0] = MAX_LEVEL;
                    }
                }
                break;
            case (int)GunTypes.Green:
                GreenadeLauncherScript gun1 = GameObject.FindObjectOfType<GreenadeLauncherScript>();
                if (gunDamageLvl[1] < MAX_LEVEL && GameCredit.gameMoney >= greenCost[gunDamageLvl[1] - 1])
                {
                    GameCredit.DeductCurrency(greenCost[gunDamageLvl[1] - 1]);
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunDamageLvl[1] < MAX_LEVEL)
                    {
                        ++this.gunDamageLvl[1];
                        gun1.bulletDamage = grenadeLauncherDamage[this.gunDamageLvl[1] - 1];
                        buttonText.text = "Upgrade Damage\n" + greenCost[gunDamageLvl[1] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunDamageLvl[1] = MAX_LEVEL;
                    }
                }
                break;
            case (int)GunTypes.Red:
                RedLaserScript gun2 = GameObject.FindObjectOfType<RedLaserScript>();
                if (gunDamageLvl[2] < MAX_LEVEL && GameCredit.gameMoney >= redCost[gunDamageLvl[2] - 1])
                {
                    GameCredit.DeductCurrency(redCost[gunDamageLvl[2] - 1]);
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunDamageLvl[2] < MAX_LEVEL)
                    {
                        ++this.gunDamageLvl[2];
                        gun2.bulletDamage = lasergunDamage[this.gunDamageLvl[2] - 1];
                        buttonText.text = "Upgrade Damage\n" + redCost[gunDamageLvl[2] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunDamageLvl[2] = MAX_LEVEL;
                    }
                }
                break;
        }
    }

    public void UpgradeGunFireRate(int type)
    {
        Text buttonText = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        switch (type)
        {
            case (int)GunTypes.Blue:
                IceShotgunScript gun = GameObject.FindObjectOfType<IceShotgunScript>();
                if (gunFireRateLvl[0] < MAX_LEVEL && GameCredit.gameMoney >= blueCost[gunFireRateLvl[0] - 1])
                {
                    GameCredit.DeductCurrency(blueCost[gunFireRateLvl[0] - 1]);
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunFireRateLvl[0] < MAX_LEVEL)
                    {
                        ++this.gunFireRateLvl[0];
                        gun.fireRate = shotgunFireRate[this.gunFireRateLvl[0] - 1];
                        buttonText.text = "Upgrade Fire Rate\n" + blueCost[gunFireRateLvl[0] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunFireRateLvl[0] = MAX_LEVEL;
                    }
                }
                break;
            case (int)GunTypes.Green:
                GreenadeLauncherScript gun1 = GameObject.FindObjectOfType<GreenadeLauncherScript>();
                if (gunFireRateLvl[1] < MAX_LEVEL && GameCredit.gameMoney >= greenCost[gunFireRateLvl[1] - 1])
                {
                    GameCredit.DeductCurrency(greenCost[gunFireRateLvl[1] - 1]);
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunFireRateLvl[1] < MAX_LEVEL)
                    {
                        ++this.gunFireRateLvl[1];
                        gun1.fireRate = grenadeLauncherFireRate[this.gunFireRateLvl[1] - 1];
                        buttonText.text = "Upgrade Fire Rate\n" + greenCost[gunFireRateLvl[1] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunFireRateLvl[1] = MAX_LEVEL;
                    }
                }
                break;
            case (int)GunTypes.Red:
                RedLaserScript gun2 = GameObject.FindObjectOfType<RedLaserScript>();
                if (gunFireRateLvl[2] < MAX_LEVEL && GameCredit.gameMoney >= redCost[gunFireRateLvl[2] - 1])
                {
                    GameCredit.DeductCurrency(redCost[gunFireRateLvl[2] - 1]);
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunFireRateLvl[2] < MAX_LEVEL)
                    {
                        ++this.gunFireRateLvl[2];
                        gun2.fireRate = lasergunFireRate[this.gunFireRateLvl[2] - 1];
                        buttonText.text = "Upgrade Fire Rate\n" + redCost[gunFireRateLvl[2] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunFireRateLvl[2] = MAX_LEVEL;
                    }

                }
                break;
        }
    }

    public void UpgradeGunMagazine(int type)
    {
        Text buttonText = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        switch (type)
        {
            case (int)GunTypes.Blue:
                IceShotgunScript gun = GameObject.FindObjectOfType<IceShotgunScript>();
                if (gunMagazineLvl[0] < MAX_LEVEL && GameCredit.gameMoney >= blueCost[gunMagazineLvl[0] - 1])
                {
                    GameCredit.DeductCurrency(blueCost[gunMagazineLvl[0] - 1]);
                   
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunMagazineLvl[0] < MAX_LEVEL)
                    {
                        ++this.gunMagazineLvl[0];
                        gun.magazine = shotgunMagazine[this.gunMagazineLvl[0] - 1];
                        buttonText.text = "Upgrade Ammo\n" + blueCost[gunMagazineLvl[0] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunMagazineLvl[0] = MAX_LEVEL;
                    }
                }
                break;
            case (int)GunTypes.Green:
                GreenadeLauncherScript gun1 = GameObject.FindObjectOfType<GreenadeLauncherScript>();
                if (gunMagazineLvl[1] < MAX_LEVEL && GameCredit.gameMoney >= greenCost[gunMagazineLvl[1] - 1])
                {
                    GameCredit.DeductCurrency(greenCost[gunMagazineLvl[1] - 1]);
                  
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunMagazineLvl[1] < MAX_LEVEL)
                    {
                        ++this.gunMagazineLvl[1];
                        gun1.magazine = grenadeLauncherMagazine[this.gunMagazineLvl[1] - 1];
                        buttonText.text = "Upgrade Ammo\n" + greenCost[gunMagazineLvl[1] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunMagazineLvl[1] = MAX_LEVEL;
                    }
                }
                break;
            case (int)GunTypes.Red:
                RedLaserScript gun2 = GameObject.FindObjectOfType<RedLaserScript>();
                if (gunMagazineLvl[2] < MAX_LEVEL && GameCredit.gameMoney >= redCost[gunMagazineLvl[2] - 1])
                {
                    GameCredit.DeductCurrency(redCost[gunMagazineLvl[2] - 1]);
             
                    FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
                    if (this.gunMagazineLvl[2] < MAX_LEVEL)
                    {
                        ++this.gunMagazineLvl[2];
                        gun2.magazine = lasergunMagazine[this.gunMagazineLvl[2] - 1];
                        buttonText.text = "Upgrade Ammo\n" + greenCost[gunMagazineLvl[2] - 1].ToString() + " Credits";
                    }
                    else
                    {
                        this.gunMagazineLvl[2] = MAX_LEVEL;
                    }
                }
                break;
        }
    }

    private float[] PlayerHealth = { 150, 180, 220, 250 , 300};
    private float[] PlayerSpeed = { 3, 3.5f, 4.0f, 4.5f, 5.0f };
    //guide here
    private float[] healthCost = {800, 1100, 1400, 1500, 0 }; //this is the cost per level
    private float[] speedCost = {800, 1200, 1600, 1800, 0 }; //this is the cost per level

    [HideInInspector] public int[] playerlevel = { 1, 1 }; // player level health and speed

    public void UpgradeHealth()
    {
        Text buttonText = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        PlayerStatistics health;
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        //guide here
        if (playerlevel[0] < MAX_LEVEL && GameCredit.gameMoney >= healthCost[playerlevel[0] - 1])
        {
            //guide here
            GameCredit.DeductCurrency(healthCost[playerlevel[0] - 1]);
            FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
            if (this.playerlevel[0] < MAX_LEVEL)
            {
                ++this.playerlevel[0];
                health.playerMaxHealth = PlayerHealth[this.playerlevel[0] - 1];
                buttonText.text = "Upgrade Health\n" + healthCost[playerlevel[0] - 1].ToString() + "Credits";
            }
            else
            {
                this.playerlevel[0] = MAX_LEVEL;
            }
        }

    }

    public void UpgradeSpeed()
    {
        Text buttonText = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        PlayerStatistics speed;
        speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        if (playerlevel[1] < MAX_LEVEL && GameCredit.gameMoney >= speedCost[playerlevel[1] - 1])
        {
            GameCredit.DeductCurrency(speedCost[playerlevel[1] - 1]);
            FindObjectOfType<AudioManagerScript>().playSound("ShopButton");
            if (this.playerlevel[1] < MAX_LEVEL)
            {
                ++this.playerlevel[1];
                speed.playerSpeed = PlayerSpeed[this.playerlevel[1] - 1];
                buttonText.text = "Upgrade Speed\n" + speedCost[playerlevel[1] - 1].ToString() + "Credits";
            }
            else
            {
                this.playerlevel[1] = MAX_LEVEL;
            }
        }
    }
}
