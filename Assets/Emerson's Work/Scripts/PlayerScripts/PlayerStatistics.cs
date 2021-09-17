using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    /*DECLARATION START*/ 

    //player bullet types
    public GameObject redBullet;
    public GameObject greenBullet;
    public GameObject blueBullet;

    //bullet and gun names; static const
    [HideInInspector] public const string sRedBul = "RED_BULLET";
    [HideInInspector] public const string sBlueBul = "BLUE_BULLET";
    [HideInInspector] public const string sGreenBul = "GREEN_BULLET";
    [HideInInspector] public const string sRedGun = "RED_GUN";
    [HideInInspector] public const string sBlueGun = "BLUE_GUN";
    [HideInInspector] public const string sGreenGun = "GREEN_GUN";

    //Default for bullet type
    [HideInInspector] public string currentBullet;
    [HideInInspector] public string currentGun;

    //player properties
    public float playerSpeed = 5.0f;
    public float PlayerSpeed
    {
        get { return playerSpeed; }
        private set { playerSpeed = value; }
    }
    [SerializeField] private float playerHealth = 100.0f;
    public float playerMaxHealth = 100.0f;
    public float PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }
    //Players vulnerability to damage
    [HideInInspector]public bool vulnerable = true;
    //Player dead state
    [HideInInspector] public bool isDead = false;

    /*DECLARATION END*/

    void Start()
    {
        //Debug.Log(playerMaxHealth);
        currentBullet = sBlueBul; //default bullet loadout
        currentGun = sBlueGun; //default gun loadout
    }


    void Update()
    {
        
    }
}
