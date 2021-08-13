using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    Button btn;
    bool reverse = true;
    
    // Start is called before the first frame update
    void Start()
    {
        btn = this.GetComponent<Button>();
    }

    public void displayText()
    {
        btn.GetComponentInChildren<Text>().text = reverse ? "Hello" : "";
        reverse = !reverse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
