using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopSceneHUD : MonoBehaviour
{
   public Text CreditsDisplay;


    void Update()
    {
        CreditsDisplay.text = GameCredit.gameMoney.ToString();
    }

    public void loadGameScene()
    {
        LoaderScript.loadScene(1, SceneManager.sceneCountInBuildSettings - 1);
        GameSceneScript.Instance.currentScene = 1;
        Debug.Log("loadGameScene");
        AudioManagerScript.instance.stopSound("ShopMusic");

        initializeGun();
        PlayerFunctions plyFunc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunctions>();
        plyFunc.refreshPlayer();
    }
    private void initializeGun()
    {
        if (GameObject.FindGameObjectWithTag("Gun") != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Gun");
            go.transform.GetChild(0).gameObject.SetActive(true);
            go.transform.GetChild(1).gameObject.SetActive(false);
            go.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void loadMainMenu()
    {
        AudioManagerScript.instance.stopSound("ShopMusic");
        LoaderScript.loadScene(0, SceneManager.sceneCountInBuildSettings - 1);
        GameSceneScript.Instance.currentScene = 0;
        Debug.Log("loadMainMenu");
    }
}
