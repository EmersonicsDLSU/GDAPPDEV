using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBulletScript : MonoBehaviour, IGunModel, IBullet
{
    /*DECLARATION START*/

    //time it takes before the bullet got destroyed
    private float bulletDuration = 5.0f;
    private IceShotgunScript iceShotgun;

    //bullet movement
    private bool isBulletRight;

    //player movement
    private PlayerMovement playerMove;

    //bulletRigidBody
    private Rigidbody2D bulletRb;

    /*DECLARATION END*/

    void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        bulletRb = GetComponent<Rigidbody2D>();
        isBulletRight = playerMove.m_FacingRight;
        iceShotgun = GameObject.FindObjectOfType<IceShotgunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletDuration -= Time.deltaTime;
        if (bulletDuration <= 0.0f)
        {
            Destroy(this.gameObject);
        }

        bulletDirection();
    }

    //adds force to the bullet
    public void bulletDirection()
    {
        Transform firePoint = this.transform;
        bulletRb.AddForce(firePoint.right * iceShotgun.bulletSpeed, ForceMode2D.Impulse);
    }

    //spawns the bullet into its selected location
    public void createBullet(ref GameObject bullet)
    {
        Debug.Log("Blue bullet spawn");
        //get gun
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");

        PlayerMovement tempPlayerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        iceShotgun = GameObject.FindObjectOfType<IceShotgunScript>();
        //gets the last direction of the attack
        float dirX = iceShotgun.dirX;
        float dirY = iceShotgun.dirY;

        //spawns the bullet
        GameObject go = Instantiate(bullet,
            gun.transform.position + new Vector3(1.0f * dirX, 1.0f * dirY, 0),
            Quaternion.identity);

        //change orientation based on the direction
        Rigidbody2D bulletRb = go.GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;
        bulletRb.rotation = angle;

        /*//bullet direction; shoots to the left; when idle facing left
        if (tempPlayerMove.playerFace == PlayerMovement.sLeft && tempPlayerMove.MovementX == 0.0f)
        {
            go.transform.localEulerAngles = transform.forward * 180;
        }*/
    }

    private void enemyDamage(ref EnemyStatistics enemyStats)
    {
        float totalDamage = 0;
        //determines the effectiveness of the damage
        if(enemyStats.color_type == EnemyStatistics.sBlueEnemy)
        {
            totalDamage = iceShotgun.bulletDamage * 2;
        }
        if (enemyStats.color_type == EnemyStatistics.sGreenEnemy)
        {
            totalDamage = iceShotgun.bulletDamage * 0.5f;
        }
        if (enemyStats.color_type == EnemyStatistics.sRedEnemy)
        {
            totalDamage = iceShotgun.bulletDamage * 0.5f;
        }
        if (enemyStats.color_type == EnemyStatistics.sNormalnemy)
        {
            totalDamage = iceShotgun.bulletDamage * 1;
        }
        //damage the player
        enemyStats.EnemyHealth -= totalDamage;
        //add the sfx damage numbers
    }

    public void onHit(GameObject enemyGo)
    {
        //Debug.Log("Hit");
        //destroys the bullet if it was collided with the enemy
        Destroy(this.gameObject, GeneralAnimationProperty.bullet_duration);

        //damage the player
        EnemyStatistics enemyStats = enemyGo.GetComponentInChildren<EnemyStatistics>();
        enemyDamage(ref enemyStats);

        //checks if the enemy is below 0 health
        if(enemyStats.EnemyHealth <= 0.0f && !enemyStats.isDead)
        {
            //set dead statuts to true
            enemyStats.isDead = true;
            //stops the movement
            EnemyMovement enemyMove = enemyGo.GetComponentInChildren<EnemyMovement>();
            enemyMove.stopMovement();
            //set the dead animation to true
            ICharacterAnimations enemyAnim = enemyGo.GetComponentInChildren<ICharacterAnimations>();
            enemyAnim.deadAnimation();
            //check if its from the spawn; spawners have gameObject pooling
            if (enemyGo.transform.parent != null)
            {
                EnemySpawn hasSpawn = enemyGo.transform.parent.GetComponent<EnemySpawn>();
                hasSpawn.destroyEnemy(ref enemyGo);
            }
            //check if its not from the spawn
            else
            {
                enemyAnim.destroyObj();
                //add money when the enemy was killed
                GameCredit.addCurrency(100);
            }
        }
    }
}
