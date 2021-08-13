using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject TitleMenuPanel;
    [SerializeField] GameObject OptionsMenuPanel;

    [SerializeField] Dropdown dropDownOption;
    [SerializeField] Text dropDownText;

    [SerializeField] GameObject toggleGroupGO;
    [SerializeField] Text toggleGroupText;

    [SerializeField] GameObject parentSpawn;
    [SerializeField] GameObject childSpawn;

    [SerializeField] InputField field;
    [SerializeField] Text upperLeftText;

    public void changeUpperLeftText()
    {
        upperLeftText.text = field.text;
    }

    public void spawnButton()
    {
        GameObject go = Instantiate(childSpawn, parentSpawn.transform.position, Quaternion.identity);
        go.transform.SetParent(parentSpawn.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
    }

    public void changeToggleGroupText()
    {
        ToggleGroup toggleGroup =  toggleGroupGO.GetComponent<ToggleGroup>();
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        foreach (var item in toggles)
        {
            if(item.isOn)
            {
                toggleGroupText.text = item.GetComponentInChildren<Text>().text;
            }
        }
    }

    public void changeDropDownText()
    {
        dropDownText.text =  dropDownOption.options[dropDownOption.value].text;
    }
    public void exitButton()
    {
        Application.Quit();
    }
    public void DisplayTitleMenu()
    {
        TitleMenuPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void DisplayMainMenu()   //GameMenu
    {
        TitleMenuPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        OptionsMenuPanel.SetActive(false);
    }

    public void DisplayOptionsMenu()
    {
        OptionsMenuPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

}
