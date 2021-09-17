using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopSceneHUD : MonoBehaviour
{
    public Text CreditsDisplay;

    public AdvertismentManager Ads;

    PlayerStatistics playerStats;

    IceShotgunScript blueGun;
    GreenadeLauncherScript greenGun;
    RedLaserScript redGun;
    ShopUpgrade shop_up;

    private void Start()
    {
        blueGun = FindObjectOfType<IceShotgunScript>();
        greenGun = FindObjectOfType<GreenadeLauncherScript>();
        redGun = FindObjectOfType<RedLaserScript>();
        shop_up = FindObjectOfType<ShopUpgrade>();
        if(!UserAccountSc.Instance.AdFree)
        {
            Ads.ShowBanner();
            Ads.ShowInterstitialAd();
        }
        playerStats = GameObject.FindObjectOfType<PlayerStatistics>();
        AliensKilledUpgrade.text = $"Aliens Killed: {UserAccountSc.Instance.UserGameScore}";
    }
    void Update()
    {
        CreditsDisplay.text = $"Credits: {GameCredit.gameMoney.ToString()}";
        checkIfBannerIsOpen();
    }

    [SerializeField] private GameObject closeButtonBanner;

    private void checkIfBannerIsOpen()
    {
        if(Ads.isBannerAvailable())
        {
            closeButtonBanner.SetActive(true);
        }
        else
        {
            closeButtonBanner.SetActive(false);
        }
    }

    public void loadGameScene()
    {
        LoaderScript.loadScene(1, SceneManager.sceneCountInBuildSettings - 1);
        GameSceneScript.Instance.currentScene = 1;
        //Debug.Log("loadGameScene");
        AudioManagerScript.instance.stopSound("ShopMusic");
        PlayerFunctions plyFunc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
        plyFunc.refreshPlayer();
        initializeGun();
    }
    private void initializeGun()
    {
        if (GameObject.FindGameObjectWithTag("Gun") != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Gun");
            go.transform.GetChild(0).gameObject.SetActive(true);
            go.transform.GetChild(1).gameObject.SetActive(false);
            go.transform.GetChild(2).gameObject.SetActive(false);
            playerStats.currentGun = PlayerStatistics.sBlueGun;
            playerStats.currentBullet = PlayerStatistics.sBlueBul;
        }
    }

    public void loadMainMenu()
    {
        AudioManagerScript.instance.stopSound("ShopMusic");
        LoaderScript.loadScene(0, SceneManager.sceneCountInBuildSettings - 1);
        GameSceneScript.Instance.currentScene = 0;
        Debug.Log("loadMainMenu");
    }

    [SerializeField] private GameObject ShopUpgradePanel;
    [SerializeField] private GameObject InfoPanel;

    public void onSeeInfoTab()
    {
        AliensKilledInfo.text = $"Aliens Killed: {UserAccountSc.Instance.UserGameScore}";
        ShopUpgradePanel.SetActive(false);
        InfoPanel.SetActive(true);
        UpdateInfo();
    }
    public void onUpgradeTab()
    {
        ShopUpgradePanel.SetActive(true);
        InfoPanel.SetActive(false);
    }

    [SerializeField] private Text[] Shotgun;
    [SerializeField] private Text[] GrenadeLauncher;
    [SerializeField] private Text[] LaserGun;
    [SerializeField] private Text[] PlayerInf;
    [SerializeField] private Text AliensKilledUpgrade;
    [SerializeField] private Text AliensKilledInfo;

    private void UpdateInfo()
    {
        Shotgun[0].text = $"SHOTGUN\nAmmo: {blueGun.magazine}\nLvl. {shop_up.gunMagazineLvl[0]}";
        Shotgun[1].text = $"SHOTGUN\nFire Rate: {blueGun.fireRate}\nLvl. {shop_up.gunFireRateLvl[0]}";
        Shotgun[2].text = $"SHOTGUN\nDamage: {blueGun.bulletDamage}\nLvl. {shop_up.gunDamageLvl[0]}";

        GrenadeLauncher[0].text = $"GREENADE LAUNCHER\nAmmo: {greenGun.magazine}\nLvl. {shop_up.gunMagazineLvl[1]}";
        GrenadeLauncher[1].text = $"GREENADE LAUNCHER\nFire Rate: {greenGun.fireRate}\nLvl. {shop_up.gunFireRateLvl[1]}";
        GrenadeLauncher[2].text = $"GREENADE LAUNCHER\nDamage: {greenGun.bulletDamage}\nLvl. {shop_up.gunDamageLvl[1]}";

        LaserGun[0].text = $"LASER GUN\nAmmo: {redGun.magazine}\nLvl. {shop_up.gunMagazineLvl[2]}";
        LaserGun[1].text = $"LASER GUN\nFire Rate: {redGun.fireRate}\nLvl. {shop_up.gunFireRateLvl[2]}";
        LaserGun[2].text = $"LASER GUN\nDamage: {redGun.bulletDamage}\nLvl. {shop_up.gunDamageLvl[2]}";

        PlayerInf[0].text = $"{UserAccountSc.Instance.UserName}\nHealth: {playerStats.playerMaxHealth}\nLvl. {shop_up.playerlevel[0]}";
        PlayerInf[1].text = $"{UserAccountSc.Instance.UserName}\nRunning Speed: {playerStats.playerSpeed}\nLvl. {shop_up.playerlevel[1]}";
    }
}
