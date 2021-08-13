using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public AIPath aiPath;
    private bool m_FacingRight = false;
    private float goalOffset = 2.0f;

    //enemy stats
    private float max_speed = 3.0f;
    private float attack_Timer = 0.0f;
    private bool canAttack = true;
    private bool apply_Damage = false;

    //Player components
    private GameObject player;
    private PlayerStatistics playerStats;
    private PlayerFunctions playerFunc;

    //parent Transform
    private Transform parentTrans;

    //enemy components
    private EnemyStatistics enemyStats;

    //enemy animation script
    ICharacterAnimations enemyAnim;
    private Animator enemyAnimator;

    //for player respawn
    private float spawnTimer = 0.0f;

    private void Start()
    {
        aiPath = this.transform.parent.GetComponent<AIPath>();
        aiPath.endReachedDistance = goalOffset;
        enemyAnim = this.gameObject.transform.parent.GetComponentInChildren<ICharacterAnimations>();
        parentTrans = this.transform.parent.GetComponent<Transform>();
        enemyStats = GetComponent<EnemyStatistics>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStatistics>();
        playerFunc = player.GetComponent<PlayerFunctions>();
        enemyAnimator = this.transform.parent.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //flips the scale of the enemy depending on its move direction
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            //Debug.Log("Flip Right");
            enemyAnim.movementAnimation();
            Flip();
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            //Debug.Log("Flip Left");
            enemyAnim.movementAnimation();
            Flip();
        }

        if(aiPath.reachedDestination && playerStats.vulnerable && canAttack)
        {
            //check if enemy can attack
            enemyAnim.attackAnimation(true);
            canAttack = false;
        }
        else if(!aiPath.reachedDestination || !playerStats.vulnerable || !canAttack)
        {
            IEnemyFunctions enemyFunc = GetComponent<IEnemyFunctions>();
            if(enemyFunc.getAnimationState(0) && aiPath.reachedDestination && !apply_Damage)
            {
                attackPlayer();
            }

            //enemy will not attack
            enemyAnim.attackAnimation(false);
            attack_Timer += Time.deltaTime;
            if(attack_Timer >= GeneralAnimationProperty.enemy_attack_timer)
            {
                attack_Timer = 0.0f;
                canAttack = true;
                apply_Damage = false;
            }
        }

         if(playerStats.isDead)
        {
            spawnTimer += Time.deltaTime;
            if(spawnTimer >= GeneralAnimationProperty.player_dead_delay)
            {
                spawnTimer = 0.0f;
                playerFunc.respawnPlayer();
            }
        }
    }

    private void attackPlayer()
    {
        //produce attack sound
        ICharacterSounds enemyAtkSnd = this.GetComponent<ICharacterSounds>();
        enemyAtkSnd.attackSound();
        //reduces player's health and turns off vulnerability
        playerStats.PlayerHealth -= enemyStats.enemyDamage;
        //playerStats.vulnerable = false;
        apply_Damage = true;

        if (playerStats.PlayerHealth <= 0.0f)
        {
            playerStats.isDead = true;
            playerStats.vulnerable = false;
            ICharacterAnimations playerAnim = player.GetComponent<ICharacterAnimations>();
            playerAnim.deadAnimation();
            enemyAtkSnd.deadSound();
        }
    }

    public void Flip()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            parentTrans.localScale = new Vector3(parentTrans.localScale.x < 0 ? -parentTrans.localScale.x : parentTrans.localScale.x,
                parentTrans.localScale.y, parentTrans.localScale.z);
            // Switch the way the enemy labelled as facing.
            m_FacingRight = true;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            parentTrans.localScale = new Vector3(parentTrans.localScale.x > 0 ? -parentTrans.localScale.x : parentTrans.localScale.x,
                parentTrans.localScale.y, parentTrans.localScale.z);
            // Switch the way the enemy labelled as facing.
            m_FacingRight = false;
        }
    }

    public void stopMovement()
    {
        //stops the movement of the enemy
        this.aiPath.maxSpeed = 0.0f;
    }

}
