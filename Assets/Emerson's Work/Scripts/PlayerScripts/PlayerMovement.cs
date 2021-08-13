using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*DECLARATION START*/

    //Joystick
    private Joystick joystick;
    [Range(0, 1.0f)] [SerializeField] private float jsSensitivity = 0.2f;

    //for palyer movement coordinates
    private float movementX = 0.0f;
    private float movementY = 0.0f;
    public float MovementX
    {
        get { return movementX; }
        private set { movementX = value; }
    }
    public float MovementY
    {
        get { return movementY; }
        private set { movementY = value; }
    }

    //for players phase
    public bool m_FacingRight = true;
    public bool m_FacingUp = true;

    //player Animation class
    private PlayerAnimation playerAnimation;
    //player Statistics class
    private PlayerStatistics playerStats;

    //the player's facing
    public const string sUp = "LOOKING_UP";
    public const string sDown = "LOOKING_DOWN";
    public const string sLeft = "LOOKING_LEFT";
    public const string sRight = "LOOKING_RIGHT";
    [HideInInspector] public string playerFace;

    //player's rigidBody
    private Rigidbody2D playerRb;

    /*DECLARATION END*/

    public void Start()
    {
    }

    public void Initialize()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerStats = GetComponent<PlayerStatistics>();
        playerFace = sRight; //default face
        playerRb = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();

        this.transform.position = GameObject.FindGameObjectWithTag("RespawnPoint").GetComponent<Transform>().position;
    }

    public void playerMove()
    {
        Debug.Log("Move");
        //gets the raw float value of the player's movement
        MovementX = joystick.Horizontal;
        MovementY = joystick.Vertical;

        //playerAnimation
        playerAnimation.movementAnimation();

        //joystick input; player moving
        if(moveJoyStick())
        {
            //translates the character position depending on the raw values
            //slower speed == lesser bugs
            this.transform.Translate(MovementX * Time.deltaTime * playerStats.PlayerSpeed,
                MovementY * Time.deltaTime * playerStats.PlayerSpeed,
                0.0f); // smooth movement

            /*Vector2 playerMOveForce = new Vector2(MovementX * Time.deltaTime * playerStats.PlayerSpeed,
                MovementY * Time.deltaTime * playerStats.PlayerSpeed);
            playerRb.AddForce(playerMOveForce);*/
        }

        playerFaceCheck();
    }

    private void playerFaceCheck()
    {
        // If the input is moving the player right and the player is facing left
        if (MovementX > 0 && !m_FacingRight)
        {
            //set the player's facing direction
            playerFace = sRight;
            //flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right
        else if (MovementX < 0 && m_FacingRight)
        {
            //set the player's facing direction
            playerFace = sLeft;
            //flip the player.
            Flip();
        }
        // ensuring the animation setting
        else if (MovementX == 0.0f && m_FacingRight)
        {
            //set the player's facing direction
            playerFace = sRight;
        }
        // ensuring the animation setting
        else if (MovementX == 0.0f && !m_FacingRight)
        {
            //set the player's facing direction
            playerFace = sLeft;
        }

        // If the input is moving the player up
        if (MovementY > 0.5f)
        {
            //set the player's facing direction
            playerFace = sUp;
            m_FacingUp = true;
            //flip the player.
            playerAnimation.moveUpAnimation();
        }
        // Otherwise if the input is moving the player down
        else if (MovementY < -0.5f)
        {
            //set the player's facing direction
            playerFace = sDown;
            m_FacingUp = false;
            //flip the player.
            playerAnimation.moveUpAnimation();
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool moveJoyStick()
    {
        //apply sentitivity to the joystick
        if (MovementX >= jsSensitivity || MovementX <= -jsSensitivity ||
            MovementY >= jsSensitivity || MovementY <= -jsSensitivity)
            return true;

        return false;

    }

}
