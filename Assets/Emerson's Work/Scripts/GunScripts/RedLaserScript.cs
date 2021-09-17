using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLaserScript : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMove;
    //bulletDamage
    [SerializeField] public float bulletDamage = 5.0f;
    //speed of the bullet
    [HideInInspector] public float bulletSpeed = 40.0f;
    //fire rate of the gun
    [SerializeField] public float fireRate = 0.50f;
    //magazine of the gun
    [SerializeField] public int magazine = 20;
    [HideInInspector] public int currentAmmoCount = 0;
    [HideInInspector] public int storedAmmoCount = 100;

    //gun movement
    [HideInInspector] public float dirX;
    [HideInInspector] public float dirY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMovement>();
        Initialize();
    }
    private void Initialize()
    {
        currentAmmoCount = magazine;
    }

    float sample = 0.0f;
    // Update is called once per frame
    void Update()
    {
        stickToPlayerPosition();
        gunRotation();
    }

    private float offsetX = 0.1f;
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

        //change orientation based on the direction
        Transform gunRb = this.GetComponent<Transform>();
        float angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;
        gunRb.rotation = Quaternion.Euler(0, 0, angle);
    }

}
