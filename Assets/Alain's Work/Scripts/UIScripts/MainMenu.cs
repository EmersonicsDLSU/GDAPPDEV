using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button play;
 
    [SerializeField] private Button options;
    [SerializeField] private Button credits;
    [SerializeField] private Button home;
    [SerializeField] private GameObject BG1;
    [SerializeField] private GameObject BG2;
    [SerializeField] private GameObject parent1;
    [SerializeField] private GameObject parent2;
    [SerializeField] private GameObject parent3;
    [SerializeField] private GameObject parent4;


    public void loadHowToPlay()
    {
        this.parent1.SetActive(false);
        this.parent2.SetActive(false);
        this.parent3.SetActive(false);
        this.parent4.SetActive(true);
    }

    public void backFromHTPly()
    {
        this.parent1.SetActive(false);
        this.parent2.SetActive(true);
        this.parent3.SetActive(false);
        this.parent4.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        this.parent1.SetActive(false);
        this.parent2.SetActive(true);
        this.parent3.SetActive(false);
        this.BG1.SetActive(false);
        this.BG2.SetActive(true);

        Button btn = options.GetComponent<Button>();
        btn.onClick.AddListener(OnTapOptions);

        Button btn2 = home.GetComponent<Button>();
        btn2.onClick.AddListener(OnTapHome);
    }

    public void muteAudio()
    {
        bool isOn =  UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn;
        if(!isOn)
        {
            AudioManagerScript.instance.unMuteAllSound();
        }
        else
        {
            AudioManagerScript.instance.muteAllSound();
        }
    }

    public void OnTapOptions()
    {
        this.parent2.SetActive(false);
        this.parent3.SetActive(true);
        this.BG2.SetActive(true);
    }
    public void OnTapHome()
    {
        this.parent1.SetActive(true);
        this.parent2.SetActive(false);

        this.BG2.SetActive(false);
        this.BG1.SetActive(true);
    }

}
