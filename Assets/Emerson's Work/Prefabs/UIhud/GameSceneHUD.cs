using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneHUD : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Text GameTimer;
    [SerializeField] private Text IngameCreditsText;

    private int earn_Credits = 0;
    private float game_timer = 10.0f;
    private float max_game_timer = 10.0f;

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
        Debug.Log("Level: " + GameSceneScript.Instance.map_Level);
        switch (GameSceneScript.Instance.map_Level)
        {
            case 1:
                levelsHolder.transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Level1");
                break;
            case 2:
                levelsHolder.transform.GetChild(1).gameObject.SetActive(true);
                Debug.Log("Level2");
                break;
            case 3:
                levelsHolder.transform.GetChild(2).gameObject.SetActive(true);
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

        ammoText.text = playerStats.ammoCount.ToString();
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
                LoaderScript.loadScene(3, SceneManager.sceneCountInBuildSettings - 1);
                GameSceneScript.Instance.map_Level = 1;
            }
            else
            {
                //loads to the shop scene
                LoaderScript.loadScene(2, SceneManager.sceneCountInBuildSettings - 1);
            }
        }
    }
}
