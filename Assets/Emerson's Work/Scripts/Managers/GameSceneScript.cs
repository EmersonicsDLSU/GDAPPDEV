using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneScript : MonoBehaviour
{
    //Statistics of the Player
    [HideInInspector] public int currentScene = 0;
    //current map level
    [HideInInspector] public int map_Level = 1;

    [HideInInspector] public static GameSceneScript Instance;
    public void Awake()
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
