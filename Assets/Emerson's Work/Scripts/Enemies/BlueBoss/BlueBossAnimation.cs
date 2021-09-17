using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBossAnimation : MonoBehaviour, ICharacterAnimations, IEnemyFunctions, ICharacterSounds
{
    //player Animator
    private Animator enemyAnim;
    private EnemyMovement enemyMove;
    private EnemyStatistics enemyStats;
    
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
    }

    public void deadAnimation()
    {
        //animate the movement of the player
        enemyAnim.SetBool("isDead", enemyStats.isDead);
    }

    public void destroyObj()
    {
        //Destroys the gameObj
        Destroy(this.transform.parent.gameObject, GeneralAnimationProperty.BB_dead_delay);
    }

    public bool getAnimationState(int index)
    {
        return this.enemyAnim.GetCurrentAnimatorStateInfo(index).IsName("BB_attack");
    }

    public void attackSound()
    {
        AudioManagerScript.instance.playSound("BlueBoss");
    }

    public void deadSound()
    {
        AudioManagerScript.instance.playSound("EnemyKill");
    }

    public void refreshEnemy()
    {
        //starts the movement
        EnemyMovement tempMove = GetComponent<EnemyMovement>();
        tempMove.startMovement();
        //full health again
        EnemyStatistics tempStats = GetComponent<EnemyStatistics>();
        tempStats.enemyHealth = tempStats.enemyMaxHealth;
        //set dead statuts to false
        tempStats.isDead = false;
    }
    public void hitSound()
    {
        //AudioManagerScript.instance.playSoundOne("BlueTalk");
    }
}
