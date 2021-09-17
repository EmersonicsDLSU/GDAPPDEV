using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(AudioManagerScript.instance.isMute)
        {
            this.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            this.GetComponent<Toggle>().isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
