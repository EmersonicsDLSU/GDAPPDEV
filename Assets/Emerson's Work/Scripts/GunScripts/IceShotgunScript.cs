using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShotgunScript : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMove;
    //bulletDamage
    [HideInInspector] public float bulletDamage = 5.0f;
    //speed of the bullet
    [HideInInspector] public float bulletSpeed = 40.0f;
    //fire rate of the gun
    [HideInInspector] public float fireRate = 40.0f;
    //magazine of the gun
    [HideInInspector] public int magazine = 8;

    //gun movement
    public float dirX;
    public float dirY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMovement>();
    }
    float sample = 0.0f;
    // Update is called once per frame
    void Update()
    {
        stickToPlayerPosition();
        gunRotation();
    }

    private float offsetX = 0.0f;
    private float offsetY = -0.15f;
    private void stickToPlayerPosition()
    {
        Vector3 playerPos = player.transform.position;
        this.transform.parent.transform.position = new Vector3(playerPos.x + offsetX,
            playerPos.y + offsetY, playerPos.z);
    }

    private void gunRotation()
    {
        //gets the last direction of the attack
        if (playerMove.MovementX != 0 && playerMove.MovementY != 0)
        {
            dirX = playerMove.MovementX;
            dirY = playerMove.MovementY;
        }

        /*
        //gun position when idle facing right
        if (dirX == 0.0f && dirY == 0.0f && playerMove.playerFace == PlayerMovement.sRight)
        {
            dirX = 1.0f; dirY = 0.0f;
        }
        //gun position when idle facing left
        else if (dirX == 0.0f && dirY == 0.0f && playerMove.playerFace == PlayerMovement.sLeft)
        {
            dirX = -1.0f; dirY = 0.0f;
        }
        */

        //change orientation based on the direction
        Transform gunRb = this.GetComponent<Transform>();
        float angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;
        gunRb.rotation = Quaternion.Euler(0, 0, angle);


        /*
        //gun direction; shoots to the left; when idle facing left
        if (playerMove.playerFace == PlayerMovement.sLeft && playerMove.MovementX == 0.0f)
        {
            gunRb.rotation = Quaternion.Euler(0, 0, -180.0f);
        }
        */
    }

}
