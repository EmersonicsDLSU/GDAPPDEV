using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickMove : MonoBehaviour
{
    public Joystick joystick;
    [Range(0, 1.0f)] [SerializeField] private float jsSensitivity = 0.2f;
    [Range(0, 0.1f)] [SerializeField] private float jsMovementSensitivity = 0.01f;
    private float borderOffset = 0.3f;
    private float screenHeight = Screen.height / 100;
    private float screenWidth = Screen.width / 100;


    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;


        if(horizontal >= jsSensitivity || horizontal <= -jsSensitivity || vertical >= jsSensitivity || vertical <= -jsSensitivity)
        {
            Vector2 direction = new Vector2(horizontal, vertical).normalized;

            /* //raw translation
            Vector2 currDirection = new Vector2(transform.position.x, transform.position.y);
            Vector2 test = new Vector2(1.0f, 1.0f);
            transform.position = 0.01f * test * direction + currDirection;
            */

            transform.Translate(direction * jsMovementSensitivity, Space.World);

            float originX = Camera.main.transform.position.x;
            float originY = Camera.main.transform.position.y;

            if (transform.position.x <= (-screenWidth + originX) + borderOffset)
            {
                transform.position = new Vector2((-screenWidth + originX) + borderOffset, transform.position.y);
            }
            if (transform.position.x >= (screenWidth + originX) - borderOffset)
            {
                transform.position = new Vector2((screenWidth + originX) - borderOffset, transform.position.y);
            }
            if (transform.position.y <= (-screenHeight + originY) + borderOffset)
            {
                transform.position = new Vector2(transform.position.x, (-screenHeight + originY) + borderOffset);
            }
            if (transform.position.y >= (screenHeight + originY) + borderOffset)
            {
                transform.position = new Vector2(transform.position.x, (screenHeight + originY) + borderOffset);
            }
        }
    }
}
