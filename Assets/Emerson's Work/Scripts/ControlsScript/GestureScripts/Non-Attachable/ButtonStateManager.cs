using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStateManager : MonoBehaviour
{
    [HideInInspector] public static ButtonStateManager Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        //assigns the one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //destroys the duplicate gameObject 
            Destroy(gameObject);
        }
    }

    [HideInInspector] public bool onJoystick = false;
    [HideInInspector] public bool onFireButton = false;
}
