using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    public void Pause()
    {
        //Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    //Gets called even when app is slightly hidden
    //Similar behavior to Android's OnPause
    //unity own function for detecting the focus of the game in phone
    public void OnApplicationFocus(bool focus)
    {
        //Debug.Log($"OnPause: {focus}");
        if(!focus)
        {
            //Pause();
        }
    }

    //Gets called when home / overview is pressed
    //Similar behavior to Android's OnPause
    // make sure to only use one funciton either "OnApplicationFocus" or "OnApplicationPause"
    //overlay like inputTextField will not be pause
    public void OnApplicationPause(bool pause)
    {
        //Debug.Log($"OnStop: {pause}");
        if (!pause)
        {
            Pause();
        }
        else if(pause)
        {
            Resume();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
