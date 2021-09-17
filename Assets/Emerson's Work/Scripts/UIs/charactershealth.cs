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
    private GameObject healthFill;
    private GameObject healthBorder;

    [HideInInspector] public charType characterType;

    // Start is called before the first frame update
    void Start()
    {
        healthFill = this.transform.GetChild(0).gameObject;
        healthBorder = this.transform.GetChild(1).gameObject;
        //set them to false first
        healthFill.SetActive(false);
        healthBorder.SetActive(false);
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

    [SerializeField] private float display_duration = 1.5f;
    private float display_ticks = 0.0f;

    // Update is called once per frame
    void Update()
    {
        updateHealth();

        displayHealthBar();
    }

    private float prev_health = -Mathf.Infinity;
    private float curr_health = Mathf.Infinity;
    private bool isHit = false;
    private void updateHealth()
    {
        isHit = false;
        switch (characterType)
        {
            case charType.Player:
                healthSlider.value = playerStats.PlayerHealth / playerStats.playerMaxHealth;
                curr_health = playerStats.PlayerHealth;
                //check if the health was lessen
                if(curr_health < prev_health)
                {
                    this.isHit = true;
                }
                prev_health = curr_health;
                break;
            case charType.Enemy:
                healthSlider.value = enemyStats.EnemyHealth / enemyStats.enemyMaxHealth;
                curr_health = enemyStats.EnemyHealth;
                //check if the health was lessen
                if (curr_health < prev_health)
                {
                    this.isHit = true;
                }
                prev_health = curr_health;
                break;
        }
    }

    private void displayHealthBar()
    {
        display_ticks -= Time.deltaTime;
        //check if the character was hit
        if (isHit)
        {
            display_ticks = display_duration;
        }
        //displays the healthbar when character was hit
        if (display_ticks > 0.0f &&
            (healthBorder.activeSelf == false && healthFill.activeSelf == false))
        {
            healthBorder.SetActive(true);
            healthFill.SetActive(true);
        }
        //removes the healthbar temporarily when character was hit
        else if (display_ticks <= 0.0f &&
            (healthBorder.activeSelf == true && healthFill.activeSelf == true))
        {
            healthBorder.SetActive(false);
            healthFill.SetActive(false);
        }
    }
}
