using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneHUD : MonoBehaviour
{
    [SerializeField] private Text [] ammoText;
    [SerializeField] private Text [] storedAmmoText;
    [SerializeField] private Text GameTimer;
    [SerializeField] private Text IngameCreditsText;
    [SerializeField] private Text LivesCountText;
    [SerializeField] private GameObject ReloadText;

    private int earn_Credits = 0;
    [HideInInspector] public float game_timer = 300.0f;
    public float max_game_timer = 300.0f;

    private PlayerStatistics playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        game_timer = max_game_timer;
        earn_Credits = 0;
        

        openCurrentMap();
    }
    private void openCurrentMap()
    {
        GameObject levelsHolder = GameObject.FindGameObjectWithTag("Levels");
        levelsHolder.transform.GetChild(0).gameObject.SetActive(false);
        levelsHolder.transform.GetChild(1).gameObject.SetActive(false);
        levelsHolder.transform.GetChild(2).gameObject.SetActive(false);
        //Debug.Log("Level: " + GameSceneScript.Instance.map_Level);
        switch (GameSceneScript.Instance.map_Level)
        {
            case 1:
                levelsHolder.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("A*").GetComponent<AstarPath>().Scan();
                Debug.Log("Level1");
                break;
            case 2:
                levelsHolder.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("A*").GetComponent<AstarPath>().Scan();
                Debug.Log("Level2");
                break;
            case 3:
                levelsHolder.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("A*").GetComponent<AstarPath>().Scan();
                Debug.Log("Level3");
                break;
        }
       
    }

    private void OnEnable()
    {
        game_timer = max_game_timer;
        earn_Credits = 0;
    }

    private void OnDisable()
    {
        game_timer = 0;
        earn_Credits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //timer for each level
        game_timer -= Time.deltaTime;

        //change the text display for the ammoCount
        setAmmoText();

        //For beta version only
        GameTimer.text = "Timer: " + ((int)game_timer / 60).ToString() + ":" + ((int)game_timer % 60).ToString();
        IngameCreditsText.text = "Credits: " + GameCredit.getCurrency().ToString();

        if(game_timer <= 0.0f)
        {
            AudioManagerScript.instance.stopSound("Ingame");
            GameSceneScript.Instance.map_Level = GameSceneScript.Instance.map_Level + 1;
            GameSceneScript.Instance.currentScene = 0;
            if (GameSceneScript.Instance.map_Level > 3)
            {
                //loads to the winning scene
                Debug.Log("Load winning Scene");
                GameObject.FindObjectOfType<WebHandlerScript>().CreatePlayer();
                GameSceneScript.Instance.map_Level = 1;
                Time.timeScale = 1;
                LoaderScript.loadScene(3, SceneManager.sceneCountInBuildSettings - 1);
            }
            else
            {
                //loads to the shop scene
                UserAccountSc.Instance.LifeCount = UserAccountSc.Instance.maxLifeCount;
                Time.timeScale = 1;
                LoaderScript.loadScene(2, SceneManager.sceneCountInBuildSettings - 1);
            }
        }
        Reloadtext();
        updateHealthBar();
        updateLives();

    }

    [SerializeField]private Slider healthBar;

    private void updateLives()
    {
        LivesCountText.text = $"Lives: {UserAccountSc.Instance.LifeCount}";
        if(UserAccountSc.Instance.LifeCount <= 0)
        {
            //gameover
            //loads to the losing scene
            Debug.Log("Load winning Scene");
            GameSceneScript.Instance.map_Level = 1;
            Time.timeScale = 1;
            UserAccountSc.Instance.LifeCount = UserAccountSc.Instance.maxLifeCount;
            LoaderScript.loadScene(4, SceneManager.sceneCountInBuildSettings - 1);
        }
    }

    private void updateHealthBar()
    {
        healthBar.value = playerStats.PlayerHealth / playerStats.playerMaxHealth;
    }

    private void Reloadtext()
    {
        if (playerStats.currentGun == PlayerStatistics.sBlueGun)
        {
            IceShotgunScript bluegun = GameObject.FindObjectOfType<IceShotgunScript>();
            if (bluegun.currentAmmoCount == 0)
            {
                ReloadText.SetActive(true);
            }
            else if (bluegun.currentAmmoCount > 0)
            {
                ReloadText.SetActive(false);
            }
        }
        if (playerStats.currentGun == PlayerStatistics.sGreenGun)
        {
            GreenadeLauncherScript greengun = GameObject.FindObjectOfType<GreenadeLauncherScript>();
            if (greengun.currentAmmoCount == 0)
            {
                ReloadText.SetActive(true);
            }
            else if (greengun.currentAmmoCount > 0)
            {
                ReloadText.SetActive(false);
            }
        }
        if (playerStats.currentGun == PlayerStatistics.sRedGun)
        {
            RedLaserScript redgun = GameObject.FindObjectOfType<RedLaserScript>();
            if (redgun.currentAmmoCount == 0)
            {
                ReloadText.SetActive(true);
            }
            else if (redgun.currentAmmoCount > 0)
            {
                ReloadText.SetActive(false);
            }
        } 
    }

    private void setAmmoText()
    {
        //get the status of the gun ammoCount
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");
        Debug.Log("cURRENT gUN: " + playerStats.currentGun);
        switch (playerStats.currentGun)
        {
            case PlayerStatistics.sBlueGun:
                IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
                ammoText[0].text = $"{blueGun.currentAmmoCount} / {blueGun.magazine}";
                storedAmmoText[0].text = $"{blueGun.storedAmmoCount}";
                break;
            case PlayerStatistics.sGreenGun:
                GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
                ammoText[1].text = $"{greenGun.currentAmmoCount} / {greenGun.magazine}";
                storedAmmoText[1].text = $"{greenGun.storedAmmoCount}";
                break;
            case PlayerStatistics.sRedGun:
                RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
                ammoText[2].text = $"{redGun.currentAmmoCount} / {redGun.magazine}";
                storedAmmoText[2].text = $"{redGun.storedAmmoCount}";
                break;
        }
    }
}
