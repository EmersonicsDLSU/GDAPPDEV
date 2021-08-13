using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private PlayerStatistics playerStats;
    private PlayerAnimation plyAnim;

    private void Start()
    {
        playerStats = GetComponent<PlayerStatistics>();
        plyAnim = GetComponent<PlayerAnimation>(); ;
    }

    public void respawnPlayer()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").GetComponent<Transform>();
        //respawns the player and relcate it to the recorded spawnPoint
        this.transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, 0.0f);
        playerStats.isDead = false;
        playerStats.vulnerable = true;
        //Debug.Log(playerStats.playerMaxHealth);
        playerStats.PlayerHealth = playerStats.playerMaxHealth;
        plyAnim.deadAnimation();
    }

    public void refreshPlayer()
    {
        playerStats.isDead = false;
        plyAnim.deadAnimation();
        playerStats.vulnerable = true;
        playerStats.PlayerHealth = playerStats.playerMaxHealth;
        playerStats.ammoCount = 100;
    }

    private float vulnerable_timer = 0.0f;
    public void vulnerabilityTimer()
    {
        //create a timer for the invulnerability of the character
        if(!playerStats.vulnerable)
        {
            vulnerable_timer += Time.deltaTime;
            if(vulnerable_timer >= GeneralAnimationProperty.player_vulnerable_duration)
            {
                vulnerable_timer = 0.0f;
                playerStats.vulnerable = true;
            }
        }
    }
}
