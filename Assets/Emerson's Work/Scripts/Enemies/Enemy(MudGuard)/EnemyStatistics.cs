using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatistics : MonoBehaviour
{
    [HideInInspector] public string spawnerSource = null;

    //bullet and gun names; static const
    [HideInInspector] public const string sRedEnemy = "RED_ENEMY";
    [HideInInspector] public const string sBlueEnemy = "BLUE_ENEMY";
    [HideInInspector] public const string sGreenEnemy = "GREEN_ENEMY";
    [HideInInspector] public const string sNormalnemy = "NORMAL_ENEMY";

    //enemy properties
    [SerializeField] private float enemySpeed = 5.0f;
    public float enemyDamage = 5.0f;
    public string color_type;
    public float EnemySpeed
    {
        get { return enemySpeed; }
        set { enemySpeed = value; }
    }
    public float enemyHealth = 10.0f;
    public float enemyMaxHealth = 10.0f;
    public float EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }
    //Enemy's vulnerability to damage
    public bool vulnerable = true;
    [HideInInspector] public bool isDead = false;

    /*DECLARATION END*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
