using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBossAnimation : MonoBehaviour, ICharacterAnimations, IEnemyFunctions
{
    //player Animator
    private Animator enemyAnim;
    private EnemyMovement enemyMove;
    private EnemyStatistics enemyStats;
    [HideInInspector] public float dead_delay = 0.5f;
   

    private void Start()
    {
        enemyAnim = transform.parent.GetComponentInChildren<Animator>();
        enemyMove = GetComponentInChildren<EnemyMovement>();
        enemyStats = GetComponent<EnemyStatistics>();
    }

    public void movementAnimation()
    {
        //animate the movement of the player
        enemyAnim.SetFloat("Speed", enemyMove.aiPath.desiredVelocity.x);
    }
    public void idleAnimation()
    {

    }
    public void moveUpAnimation()
    {

    }
    public void attackAnimation(bool attack = true)
    {
        //animate the movement of the player
        enemyAnim.SetBool("isAttack", attack);
        if (attack == true)
            AudioManagerScript.instance.playSound("RedBoss");

    }

    public void deadAnimation()
    {
        //animate the movement of the player
        enemyAnim.SetBool("isDead", enemyStats.isDead);
        AudioManagerScript.instance.playSound("EnemyKill");

    }
    public void destroyObj()
    {
        //Destroys the gameObj
        Destroy(this.transform.parent.gameObject, GeneralAnimationProperty.RB_dead_delay);
    }
    public bool getAnimationState(int index)
    {
        return this.enemyAnim.GetCurrentAnimatorStateInfo(index).IsName("RB_attack");
    }
}
