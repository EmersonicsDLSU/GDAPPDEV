using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinSceneHUD : MonoBehaviour
{
    public void loadMainMenuScene()
    {
        LoaderScript.loadScene(0, SceneManager.sceneCountInBuildSettings - 1);
        AudioManagerScript.instance.stopSound("Win");
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManagerScript.instance.playSound("Win");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
