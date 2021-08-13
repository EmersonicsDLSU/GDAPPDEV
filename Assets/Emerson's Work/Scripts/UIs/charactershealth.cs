using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charactershealth : MonoBehaviour
{
    public enum charType { Player, Enemy};

    //possible scripts to modify
    private PlayerStatistics playerStats;
    private EnemyStatistics enemyStats;

    //the healthbar ui component
    private Slider healthSlider;

    [HideInInspector] public charType characterType;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        //get the possible script
        if(this.transform.parent.parent.GetComponent<PlayerStatistics>())
        {
            playerStats = this.transform.parent.parent.GetComponent<PlayerStatistics>();
            characterType = charType.Player;
        }
        else if (this.transform.parent.parent.GetComponent<EnemyStatistics>())
        {
            enemyStats = this.transform.parent.parent.GetComponent<EnemyStatistics>();
            characterType = charType.Enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(characterType)
        {
            case charType.Player:
                healthSlider.value = playerStats.PlayerHealth / playerStats.playerMaxHealth;
                break;
            case charType.Enemy:
                healthSlider.value = enemyStats.EnemyHealth / enemyStats.enemyMaxHealth;
                break;
        }
    }
}
