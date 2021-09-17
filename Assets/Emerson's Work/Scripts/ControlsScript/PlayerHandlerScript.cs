using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add this script component for object you want to delete on tap
public class PlayerHandlerScript : MonoBehaviour, ITap, ISwipped, IDragged
{
    public float speed = 10;
    private Vector3 TargetPos = Vector3.zero;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //TargetPos = transform.position;
    }

    private void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
    }

    public void OnDrag(DragEventArgs args)
    {
        
        /*
        if (args.HitObject == gameObject)
        {
            Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
            Vector3 worldPoint = r.GetPoint(10);

            TargetPos = worldPoint;
            transform.position = worldPoint;
        }
        */
    }

    private float dashDistance = 40.0f;

    public void OnSwipe(SwipeEventArgs args)
    {
        AudioManagerScript.instance.playSound("Dash");

        Vector3 norm = (new Vector3(args.SwipeVector.x, args.SwipeVector.y, 0)).normalized;

        float angle;
        //translate the player
        if (norm.x > 0.0f && playerMovement.playerFace == PlayerMovement.sLeft)
        {
            playerMovement.playerFace = PlayerMovement.sRight;
            playerMovement.Flip();
        }
        else if (norm.x < 0.0f && playerMovement.playerFace == PlayerMovement.sRight)
        {
            playerMovement.playerFace = PlayerMovement.sLeft;
            playerMovement.Flip();
        }
        if(playerMovement.playerFace == PlayerMovement.sLeft)
        {
            angle = Mathf.Atan2(args.SwipeVector.y, args.SwipeVector.x) * Mathf.Rad2Deg - 180f;
            rb.rotation = angle;
        }
        else if(playerMovement.playerFace == PlayerMovement.sRight)
        {
            angle = Mathf.Atan2(args.SwipeVector.y, args.SwipeVector.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        rb.AddForce(new Vector2(norm.x * dashDistance, norm.y * dashDistance), ForceMode2D.Impulse);
        /*
        if (args.SwipeDirection == SwipeDirections.RIGHT)
        {
            if (playerMovement.playerFace == PlayerMovement.sLeft)
            {
                playerMovement.playerFace = PlayerMovement.sRight;
                playerMovement.Flip();
            }
            rb.AddForce(transform.right * dashDistance, ForceMode2D.Impulse);
        }
        else if(args.SwipeDirection == SwipeDirections.LEFT)
        {
            if (playerMovement.playerFace == PlayerMovement.sRight)
            {
                playerMovement.playerFace = PlayerMovement.sLeft;
                playerMovement.Flip();
            }
            rb.AddForce(-transform.right * dashDistance, ForceMode2D.Impulse);
        }
        */
    }


    /*
    private void dashEffect()
    {
        AudioManagerScript.instance.playSound("Dash");
        //translate the player
        if (args)
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
    */

    public void OnTap()
    {
        /*
        Debug.Log($"Tapped on {this.gameObject.name }");
        //Destroy this object on Tap
        Destroy(this.gameObject);
        */
    }
}
