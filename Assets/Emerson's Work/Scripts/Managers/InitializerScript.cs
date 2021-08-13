using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializerScript : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().Initialize();
        GameSceneScript.Instance.currentScene = 1;
        AudioManagerScript.instance.playSound("Ingame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
