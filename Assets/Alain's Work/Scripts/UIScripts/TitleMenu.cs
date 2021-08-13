using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleMenu : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private GameObject BG1;
    [SerializeField] private GameObject BG2;
    [SerializeField] private GameObject parent1;
    [SerializeField] private GameObject parent2;
    [SerializeField] private GameObject parent3;

    // Start is called before the first frame update
    void Start()
    {
        this.parent1.SetActive(true);
        this.parent2.SetActive(false);
        this.parent3.SetActive(false);
        this.BG1.SetActive(true);
        this.BG2.SetActive(false);

        Button btn = start.GetComponent<Button>();
        btn.onClick.AddListener(StartApplication);
    }

    public void StartApplication()
    {
        this.parent1.SetActive(false);
        this.parent2.SetActive(true);
        this.BG1.SetActive(false);
        this.BG2.SetActive(true);

    }

    public void loadTitleMenuScene()
    {
        LoaderScript.loadScene(0, SceneManager.sceneCountInBuildSettings - 1);
    }
    public void loadGameLevelScene()
    {
        LoaderScript.loadScene(1, SceneManager.sceneCountInBuildSettings - 1);
        AudioManagerScript.instance.stopSound("TitleMenu");
    }


}
