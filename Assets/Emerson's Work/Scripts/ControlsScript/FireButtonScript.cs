using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FireButtonScript : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IPointerEnterHandler
{
    /*DECLARATION START*/

    //player compoennts
    private GameObject player;
    private PlayerAnimation playerAnim;
    private PlayerStatistics playerStats;
    private PlayerMovement playerMove;
    private Rigidbody2D rb;

    //gun parent
    private GameObject gun;

    //bullet movement
    private float dirX;
    private float dirY;

    //shooting features
    private float recoilDistance = 5.0f;

    /*DECLARATION END*/
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<PlayerAnimation>();
        playerStats = player.GetComponent<PlayerStatistics>();
        playerMove = player.GetComponent<PlayerMovement>();
        rb = player.GetComponent<Rigidbody2D>();
        gun = GameObject.FindGameObjectWithTag("Gun");
        gunFRSc = FindObjectOfType<GunFireRateSc>();
    }
    private void FixedUpdate()
    {
        if (isRecoil)
        {
            recoilTime -= Time.deltaTime;
            //removes the velocity after the force was added to the object
            if (recoilTime <= 0.0f)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                isRecoil = false;
                recoilTime = 0.25f;
            }
        }
    }

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("The cursor entered the selectable UI element.");
        ButtonStateManager.Instance.onFireButton = true;
    }
    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " Was Clicked.");
        ButtonStateManager.Instance.onFireButton = true;
    }
    //Do this when the cursor exits the rect area of this selectable UI object.
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("The cursor exited the selectable UI element.");
        ButtonStateManager.Instance.onFireButton = false;
    }
    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("The mouse click was released");
        ButtonStateManager.Instance.onFireButton = false;
    }

    public void characterAttack()
    {
        if(playerStats.isDead)
        {
            Debug.Log("Player is Dead!");
            return;
        }
        else if(!checkAmmoCount())
        {
            Debug.Log("No Ammo, RELOAD!");
            return;
        }
        else if (!checkFireRate())
        {
            Debug.Log("FireRate Ongoing");
            return;
        }
        else if (checkAmmoCount())
        {
            Debug.Log("Shoot");
            //perform all the functions for this fireEvent
            playerAnim.attackAnimation();
            
            reduceAmmoCount();

            GunSound();

            bulletSpawn();

            shootRecoil();
        }
    }

    //gunfirerate component script
    GunFireRateSc gunFRSc;
    private bool checkFireRate()
    {
        //check if in cooldown
        if (playerStats.currentGun == PlayerStatistics.sBlueGun && !gunFRSc.isBlueShoot)
        {
            return false;
        }
        else if (playerStats.currentGun == PlayerStatistics.sGreenGun && !gunFRSc.isGreenShoot)
        {
            return false;
        }
        else if (playerStats.currentGun == PlayerStatistics.sRedGun && !gunFRSc.isRedShoot)
        {
            return false;
        }
        return true;
    }

    private void reduceAmmoCount()
    {
        //get the status of the gun ammoCount
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");
        switch (playerStats.currentGun)
        {
            case PlayerStatistics.sBlueGun:
                IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
                blueGun.currentAmmoCount -= 1;
                gunFRSc.isBlueShoot = false;
                break;
            case PlayerStatistics.sGreenGun:
                GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
                greenGun.currentAmmoCount -= 1;
                gunFRSc.isGreenShoot = false;
                break;
            case PlayerStatistics.sRedGun:
                RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
                redGun.currentAmmoCount -= 1;
                gunFRSc.isRedShoot = false;
                break;
        }
    }

    private bool checkAmmoCount()
    {
        int currAmmo = 0;
        //get the status of the gun ammoCount
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");
        switch (playerStats.currentGun)
        {
            case PlayerStatistics.sBlueGun:
                IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
                currAmmo = blueGun.currentAmmoCount;
                break;
            case PlayerStatistics.sGreenGun:
                GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
                currAmmo = greenGun.currentAmmoCount;
                break;
            case PlayerStatistics.sRedGun:
                RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
                currAmmo = redGun.currentAmmoCount;
                break;
        }

        if(currAmmo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GunSound()
    {
        if (playerStats.currentGun == PlayerStatistics.sRedGun)
        {
            AudioManagerScript.instance.playSound("RedSound");
        }
        else if (playerStats.currentGun == PlayerStatistics.sBlueGun)
        {
            AudioManagerScript.instance.playSound("BlueSound");
        }
        else if (playerStats.currentGun == PlayerStatistics.sGreenGun)
        {
            AudioManagerScript.instance.playSound("GreenSound");
        }
    }

    //recoil effect timer
    private bool isRecoil = false;
    private float recoilTime = 0.25f;

    //performs a recoil transform to the player
    private void shootRecoil()
    {
        GameObject gunHolder = GameObject.FindGameObjectWithTag("Gun");
        switch(playerStats.currentGun)
        {
            case PlayerStatistics.sBlueGun:
                IceShotgunScript blueGun = gunHolder.GetComponentInChildren<IceShotgunScript>();
                rb.AddForce(new Vector2(-blueGun.dirX, -blueGun.dirY) * recoilDistance, ForceMode2D.Impulse);
                break;
            case PlayerStatistics.sGreenGun:
                GreenadeLauncherScript greenGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
                rb.AddForce(new Vector2(-greenGun.dirX, -greenGun.dirY) * recoilDistance, ForceMode2D.Impulse);
                break;
            case PlayerStatistics.sRedGun:
                RedLaserScript redGun = gunHolder.GetComponentInChildren<RedLaserScript>();
                rb.AddForce(new Vector2(-redGun.dirX, -redGun.dirY) * recoilDistance, ForceMode2D.Impulse);
                break;
        }
        isRecoil = true;
    }

    private void bulletSpawn()
    {
        //spawn a bullet depending on the current bullet used
        if (playerStats.currentBullet == PlayerStatistics.sRedBul && playerStats.currentGun == PlayerStatistics.sRedGun)
        {
            bulletSpawn2(ref playerStats.redBullet);
        }
        else if (playerStats.currentBullet == PlayerStatistics.sGreenBul && playerStats.currentGun == PlayerStatistics.sGreenGun)
        {
            bulletSpawn2(ref playerStats.greenBullet);
        }
        else if (playerStats.currentBullet == PlayerStatistics.sBlueBul && playerStats.currentGun == PlayerStatistics.sBlueGun)
        {
            bulletSpawn2(ref playerStats.blueBullet);
        }
    }

    private void bulletSpawn2(ref GameObject bulletGO)
    {
        //gets the last direction of the attack
        dirX = playerMove.MovementX;
        dirY = playerMove.MovementY;
        //bullet position when idle facing right
        if (dirX == 0.0f && dirY == 0.0f && playerMove.playerFace == PlayerMovement.sRight)
        {
            dirX = 1.0f; dirY = 0.0f;
        }
        //bullet position when idle facing left
        else if (dirX == 0.0f && dirY == 0.0f && playerMove.playerFace == PlayerMovement.sLeft)
        {
            dirX = -1.0f; dirY = 0.0f;
        }

        //spawns the bullet
        IBullet bulletHandler = bulletGO.GetComponent<IBullet>();
        bulletHandler.createBullet(ref bulletGO);
    }

    //if you want up,down,left,right bullet orientation
    //bulletOrientation(ref bulletRb);
    //this is only used if you want your shots to be up,down,left, and right directions only
    private void bulletOrientation(ref Rigidbody2D bulletRb)
    {
        float angle = 0.0f;
        switch (playerMove.playerFace)
        {
            case PlayerMovement.sLeft:
                angle = 180;
                break;
            case PlayerMovement.sRight:
                angle = 0;
                break;
            case PlayerMovement.sUp:
                angle = 90;
                break;
            case PlayerMovement.sDown:
                angle = -90;
                break;
        }
        bulletRb.rotation = angle;
    }

}
