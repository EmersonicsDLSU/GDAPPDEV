using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingamepause : MonoBehaviour
{

    public GameObject past;
    public GameObject current;


    public void isPausePressed()
    {
        Time.timeScale = 0;
        past.SetActive(false);
        current.SetActive(true);
    }

    public void isRestartPressed()
    {
        past.SetActive(false);
        current.SetActive(true);
    }
    public void isResumePressed()
    {
        Time.timeScale = 1;
        past.SetActive(false);
        current.SetActive(true);
    }
    public void NormalTransition()
    {
        past.SetActive(false);
        current.SetActive(true);
    }

    public void muteAudio()
    {
        bool isOn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn;
        if (!isOn)
        {
            AudioManagerScript.instance.unMuteAllSound();
        }
        else
        {
            AudioManagerScript.instance.muteAllSound();
        }
    }

    public void loadToMainMenu()
    {
        LoaderScript.loadScene(0, SceneManager.sceneCountInBuildSettings - 1);
    }

}