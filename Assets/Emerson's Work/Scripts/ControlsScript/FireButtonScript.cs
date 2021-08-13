using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FireButtonScript : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IPointerEnterHandler
{
    /*DECLARATION START*/

    private GameObject player;
    private PlayerAnimation playerAnim;
    private PlayerStatistics playerStats;
    private PlayerMovement playerMove;
    private Rigidbody2D rb;
    private GameObject gun;

    //bullet movement
    private float dirX;
    private float dirY;

    //shooting features
    [SerializeField] private float recoilDistance = 10.0f;

    /*DECLARATION END*/
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<PlayerAnimation>();
        playerStats = player.GetComponent<PlayerStatistics>();
        playerMove = player.GetComponent<PlayerMovement>();
        rb = player.GetComponent<Rigidbody2D>();
        gun = GameObject.FindGameObjectWithTag("Gun");
       
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
    private void GunSound() 
    {
        if(playerStats.currentGun == PlayerStatistics.sRedGun)
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

    public void characterAttack()
    {
        if(playerStats.isDead)
        {
            return;
        }
        if (playerStats.ammoCount > 0)
        {
            //perform the attackAnimation
            playerAnim.attackAnimation();
            playerStats.ammoCount -= 1;
            GunSound();

            bulletSpawn();
            shootRecoil();
        }
        

    }

    private void FixedUpdate()
    {
        if(isRecoil)
        {
            recoilTime -= Time.deltaTime;
            if(recoilTime <= 0.0f)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                isRecoil = false;
                recoilTime = 0.25f;
            }
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
                RedLaserScript greenGun = gunHolder.GetComponentInChildren<RedLaserScript>();
                rb.AddForce(new Vector2(-greenGun.dirX, -greenGun.dirY) * recoilDistance, ForceMode2D.Impulse);
                break;
            case PlayerStatistics.sRedGun:
                GreenadeLauncherScript redGun = gunHolder.GetComponentInChildren<GreenadeLauncherScript>();
                rb.AddForce(new Vector2(-redGun.dirX, -redGun.dirY) * recoilDistance, ForceMode2D.Impulse);
                break;
        }

        /*
        if (playerMove.playerFace == PlayerMovement.sRight)
        {
            rb.AddForce(-transform.right * recoilDistance, ForceMode2D.Impulse);
        }
        else if (playerMove.playerFace == PlayerMovement.sLeft)
        {
            rb.AddForce(transform.right * recoilDistance, ForceMode2D.Impulse);
        }
        else if (playerMove.playerFace == PlayerMovement.sUp)
        {
            rb.AddForce(-transform.up * recoilDistance, ForceMode2D.Impulse);
        }
        else if (playerMove.playerFace == PlayerMovement.sDown)
        {
            rb.AddForce(transform.up * recoilDistance, ForceMode2D.Impulse);
        }
        */

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
