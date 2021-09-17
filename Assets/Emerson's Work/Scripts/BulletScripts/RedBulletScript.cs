using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBulletScript : MonoBehaviour, IGunModel, IBullet
{
    /*DECLARATION START*/

    //time it takes before the bullet got destroyed
    private float bulletDuration = 5.0f;
    private RedLaserScript redGun;

    //bullet movement
    private bool isBulletRight;

    //player movement and gameObject
    private PlayerMovement playerMove;

    //bulletRigidBody
    private Rigidbody2D bulletRb;

    //effect component
    private DamageTextSpawn textEffect;

    /*DECLARATION END*/

    void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        bulletRb = GetComponent<Rigidbody2D>();
        isBulletRight = playerMove.m_FacingRight;
        redGun = GameObject.FindObjectOfType<RedLaserScript>();
        textEffect = GameObject.FindObjectOfType<DamageTextSpawn>();
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
        bulletRb.AddForce(firePoint.right * redGun.bulletSpeed, ForceMode2D.Impulse);
    }

    //spawns the bullet into its selected location
    public void createBullet(ref GameObject bullet)
    {
        Debug.Log("red bullet spawn");
        //get gun
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");

        PlayerMovement tempPlayerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        redGun = GameObject.FindObjectOfType<RedLaserScript>();
        //gets the last direction of the attack
        float dirX = redGun.dirX;
        float dirY = redGun.dirY;

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
        Color color = Color.white; //default color
        //determines the effectiveness of the damage
        if (enemyStats.color_type == EnemyStatistics.sBlueEnemy)
        {
            totalDamage = redGun.bulletDamage * 0.5f;
            color = Color.black;
        }
        if (enemyStats.color_type == EnemyStatistics.sGreenEnemy)
        {
            totalDamage = redGun.bulletDamage * 0.5f;
            color = Color.black;
        }
        if (enemyStats.color_type == EnemyStatistics.sRedEnemy)
        {
            totalDamage = redGun.bulletDamage * 2;
            color = Color.red;
        }
        if (enemyStats.color_type == EnemyStatistics.sNormalnemy)
        {
            totalDamage = redGun.bulletDamage * 1;
            color = Color.white;
        }
        //damage the player
        enemyStats.EnemyHealth -= totalDamage;
        textEffect.relaseText((int)totalDamage, enemyStats.transform.gameObject.GetComponent<Transform>()
            , color, enemyStats.transform.gameObject.GetComponent<EnemyMovement>().m_FacingRight);
        //add the sfx damage numbers
    }

    public void onHit(GameObject enemyGo)
    {
        //destroys the bullet if it was collided with the enemy
        Destroy(this.gameObject, GeneralAnimationProperty.bullet_duration);

        //damage the player
        EnemyStatistics enemyStats = enemyGo.GetComponentInChildren<EnemyStatistics>();
        enemyDamage(ref enemyStats);

        ICharacterSounds enemySounds = enemyGo.GetComponentInChildren<ICharacterSounds>();
        enemySounds.hitSound();


        //checks if the enemy is below 0 health
        if (enemyStats.EnemyHealth <= 0.0f)
        {
            //set dead status to true
            enemyStats.isDead = true;
            //stops the movement of the enemy
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
                addScoreAndMoney(enemyStats);
            }
            //check if its not from the spawn
            else
            {
                EnemySpawn pool = GameObject.Find(enemyStats.spawnerSource).GetComponent<EnemySpawn>();
                pool.enemyPool.ReleasePoolable(ref enemyGo);
                //enemyAnim.destroyObj();
                //add money when the enemy was killed
                addScoreAndMoney(enemyStats);
            }
        }
    }

    private void addScoreAndMoney(EnemyStatistics enemyStats)
    {
        //the bosses
        if (enemyStats.color_type != EnemyStatistics.sNormalnemy)
        {
            GameCredit.addCurrency(500);
            UserAccountSc.Instance.UserGameScore += 3;
        }
        //normal enemies
        else
        {
            GameCredit.addCurrency(100);
            UserAccountSc.Instance.UserGameScore += 1;
        }
    }
}
