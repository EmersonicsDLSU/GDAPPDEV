using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement plyMoveSc;
    private PlayerFunctions plyFunc;
    private PlayerStatistics plyStats;
    private Rigidbody2D rb;

    public static PlayerController instance;
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
        rb = this.GetComponent<Rigidbody2D>();
        plyMoveSc = GetComponent<PlayerMovement>();
        plyFunc = GetComponent<PlayerFunctions>();
        plyStats = GetComponent<PlayerStatistics>();
    }

    // Update is called once per frame
    //Fixed update since where tranforming(translating) the position of our player per frame
    void FixedUpdate()
    {


        if (GameSceneScript.Instance.currentScene != 0 && !plyStats.isDead)
        {
            plyMoveSc.playerMove();
            checkJoystick();
        }
        plyFunc.vulnerabilityTimer();
    }

    private void checkJoystick()
    {
        if(plyMoveSc.MovementX > 0.0f || plyMoveSc.MovementY > 0.0f)
        {
            ButtonStateManager.Instance.onJoystick = true;
        }
        else
        {
            ButtonStateManager.Instance.onJoystick = false;
        }
    }
}
