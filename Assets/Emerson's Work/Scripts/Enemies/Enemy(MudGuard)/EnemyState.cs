using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyState : MonoBehaviour
{
    private EnemyStatistics enemyStats;
    private AIDestinationSetter aiEnemy;

    private void Start()
    {
        enemyStats = this.GetComponentInChildren<EnemyStatistics>();
        aiEnemy = GetComponent<AIDestinationSetter>();
        aiEnemy.target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "RedBullet" || collision.tag == "BlueBullet" || collision.tag == "GreenBullet" && !enemyStats.isDead)
        {
            //calls the corresponding bullet funtions
            IBullet collidedBullet = collision.GetComponent<IBullet>();
            collidedBullet.onHit(this.gameObject);
        }
    }
}
