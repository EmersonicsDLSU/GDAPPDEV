using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAccountSc : MonoBehaviour
{
    //Player Properties
    [HideInInspector] public int UserGameScore = 0;
    [HideInInspector] public string UserName = "";
    public int LifeCount = 3;
    [HideInInspector] public int maxLifeCount = 3;
    [HideInInspector] public bool AdFree = false;

    [HideInInspector] public static UserAccountSc Instance;
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

    public void restartLifeCount()
    {
        LifeCount = maxLifeCount;
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
