using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInstance : MonoBehaviour
{
    public static GunInstance instance;
    void Awake()
    {
        //doesn't cut the background music that starts playing from the mainMenu
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
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
