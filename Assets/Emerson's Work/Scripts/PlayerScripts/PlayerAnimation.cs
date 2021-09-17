using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, ICharacterAnimations
{
    /*DECLARATION START*/

    //player Animator
    private Animator playerAnim;

    //player Movement class
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    //Timers for idle2 and waking animation
    private float idleTimer = 10.0f;
    private bool is_Wake = false;

    //playerShoot
    private bool isShoot = false;
    private float shootAnimDuration = 0.25f;

    //player statistics
    private PlayerStatistics playerStats;

    /*DECLARATION END*/
    public void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStatistics>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (isShoot)
        {
            shootAnimDuration -= Time.deltaTime;
            if (shootAnimDuration <= 0.0f)
            {
                //resets the idle timer
                playerAnim.SetBool("Shoot", false);
                shootAnimDuration = 0.25f;
                isShoot = false;
                ResetAnimation();
            }
        }

        //Checks if the player is idling
        if (playerMovement.MovementX == 0 || playerMovement.MovementY == 0)
        {
            idleAnimation();
        }
        else
        {
            ResetAnimation();
        }

        //turns off dash animation
        if (isDash)
        {
            if (dashNow)
            {
                dashNow = !dashNow;
                //dashEffect();
            }
            if (dashAnimationDuration <= 0.0f)
            {
                //resets the dash timer
                playerAnim.SetBool("Dash", false);
                dashAnimationDuration = 0.25f;
                isDash = false;
                rb.velocity = new Vector2(0.0f, 0.0f);
                rb.rotation = 0.0f;
                ResetAnimation();
            }
            dashAnimationDuration -= Time.deltaTime;
        }
    }

    public void movementAnimation()
    {
        //animate the movement of the player
        playerAnim.SetFloat("Speed", Mathf.Abs(playerMovement.MovementX));
        playerAnim.SetFloat("SpeedUp", Mathf.Abs(playerMovement.MovementY));
        //turns off the idle animation if ever it was preceeded by it
        if (Mathf.Abs(playerMovement.MovementX) > 0.01f)
        {
            ResetAnimation();
        }
    }

    public void idleAnimation()
    {
        //timer it takes for the player to do idle animation
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0.0f)
        {
            idleTimer = 10.0f;
            is_Wake = !is_Wake;
            playerAnim.SetBool("Idle2", is_Wake);
        }
    }

    public void moveUpAnimation()
    {
        //flips the player up or down
        if (Mathf.Abs(playerMovement.MovementY) > 0.5f)
        {
            playerAnim.SetBool("GoingUp", playerMovement.m_FacingUp);
        }
    }

    public void attackAnimation(bool attack = true)
    {
        //set the attack animation; ammo is always sufficient when called
        playerAnim.SetBool("Shoot", true);

        //get the status of the gun ammoCount
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");
        int ammoCount = 0;
        switch (playerStats.currentGun)
        {
            case PlayerStatistics.sBlueGun:
                IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
                ammoCount = blueGun.currentAmmoCount;
                break;
            case PlayerStatistics.sGreenGun:
                GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
                ammoCount = greenGun.currentAmmoCount;
                break;
            case PlayerStatistics.sRedGun:
                RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
                ammoCount = redGun.currentAmmoCount;
                break;
        }
        playerAnim.SetInteger("Ammos", ammoCount);
        isShoot = true;
    }

    //dash properties
    private float dashDistance = 2.5f;
    private float dashAnimationDuration = 0.1f;
    private bool isDash = false;
    private bool isDashRight = false;
    private bool dashNow = false;

    public void dashAnimation(bool isRight)
    {
        //set the attack animation
        playerAnim.SetBool("Dash", true);
        isDash = true;
        isDashRight = isRight;
        dashNow = true;
    }

    private void dashEffect()
    {
        AudioManagerScript.instance.playSound("Dash");
        //translate the player
        if (isDashRight)
        {
            if (playerMovement.playerFace == PlayerMovement.sLeft)
            {
                playerMovement.playerFace = PlayerMovement.sRight;
                playerMovement.Flip();
            }
            rb.AddForce(transform.right * dashDistance, ForceMode2D.Impulse);
        }
        else
        {
            if (playerMovement.playerFace == PlayerMovement.sRight)
            {
                playerMovement.playerFace = PlayerMovement.sLeft;
                playerMovement.Flip();
            }
            rb.AddForce(-transform.right * dashDistance, ForceMode2D.Impulse);
        }
    }

    public void ResetAnimation()
    {
        //resets all animation
        playerAnim.SetBool("Idle2", false);
        //resets the idle timer
        idleTimer = 10.0f;
    }

    public void deadAnimation()
    {
        //animate the movement of the player
        //Debug.Log("Dead: " + playerStats.isDead); 
        playerAnim.SetBool("isDead", playerStats.isDead);
    }

    public void destroyObj()
    {
        Destroy(this.gameObject, GeneralAnimationProperty.player_dead_delay);
    }
}
