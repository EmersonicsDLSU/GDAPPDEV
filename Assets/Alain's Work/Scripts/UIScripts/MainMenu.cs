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
    [SerializeField] private GameObject parent1; //titleMenu
    [SerializeField] private GameObject parent2; //MainMenu
    [SerializeField] private GameObject parent3; //Options
    [SerializeField] private GameObject parent4; //How To Play
    [SerializeField] private GameObject parent5; //CreateAccount
    [SerializeField] private GameObject parent6; //LeaderBoard

    public void loadHowToPlay()
    {
        this.parent1.SetActive(false);
        this.parent2.SetActive(false);
        this.parent3.SetActive(false);
        this.parent4.SetActive(true);
        this.parent5.SetActive(false);
        this.parent6.SetActive(false);
    }

    public void backFromHTPly()
    {
        this.parent1.SetActive(false);
        this.parent2.SetActive(true);
        this.parent3.SetActive(false);
        this.parent4.SetActive(false);
        this.parent5.SetActive(false);
        this.parent6.SetActive(false);
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
    public void OnTapCreateAccount()
    {
        this.parent2.SetActive(false);
        this.parent5.SetActive(true);

        this.BG2.SetActive(true);
    }
    private string password = "APDEV";
    public void OnCreateAccount()
    {
        InputField input_name = GameObject.FindGameObjectWithTag("InputName").GetComponent<InputField>();
        Debug.Log($"Name: {input_name.text}");
        if (input_name.text == "")
        {
            Debug.Log("No Name");
            return;
        }
        InputField pass_word = GameObject.FindGameObjectWithTag("InputPassword").GetComponent<InputField>();
        if(pass_word.text == this.password)
        {
            Debug.Log("Ad Free");
            UserAccountSc.Instance.AdFree = true;
        }
        UserAccountSc.Instance.UserName = input_name.text;
        GameObject.FindObjectOfType<TitleMenu>().loadGameLevelScene();
    }

    public void OnLeaderBoard()
    {
        this.parent2.SetActive(false);
        this.parent6.SetActive(true);
    }
}
