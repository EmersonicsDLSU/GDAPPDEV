using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPanelController : MonoBehaviour
{
    //the panel to be modified
    [SerializeField] GameObject LeftRedPanel;
    [SerializeField] GameObject RightRedPanel;
    [SerializeField] GameObject LowerRedPanel;
    [SerializeField] GameObject Footer;

    [SerializeField] GameObject OptionsLeftPanel;
    [SerializeField] GameObject SliderRed;
    [SerializeField] GameObject SliderGreen;
    [SerializeField] GameObject SliderBlue;

    float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the orientation of the device is in landscape mode
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Debug.Log("Landscape");
            //switch on the panel; reveals the extended design
            LeftRedPanel.SetActive(true);
            RightRedPanel.SetActive(true);
            LowerRedPanel.SetActive(false);
            Footer.SetActive(true);
        }
        //checks if the orientation of the device is in portrait mode
        if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            //turns off the panel for the meantime
            LeftRedPanel.SetActive(false);
            RightRedPanel.SetActive(false);
            LowerRedPanel.SetActive(true);
            Footer.SetActive(false);
        }

        x = SliderRed.GetComponent<Slider>().value;
        y = SliderGreen.GetComponent<Slider>().value;
        z = SliderBlue.GetComponent<Slider>().value;

        OptionsLeftPanel.GetComponent<Image>().color = new Color(x, y, z, 1.0f);
    }
}
