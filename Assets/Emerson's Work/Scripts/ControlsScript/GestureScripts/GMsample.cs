using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GMsample : MonoBehaviour
{
    public EventHandler<TapEventArgs> OnTap;

    [HideInInspector] public static GMsample Instance;

    //Gets the properties for the Tap Gesture
    public TapProperty _tapProperty;
    //Gets the properties for the Swipe Gesture
    public SwipeProperty _swipeProperty;

    //player movement script
    private PlayerMovement playerMove;

    // Start is called before the first frame update
    private void Awake()
    {
        //assigns the one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //destroys the duplicate gameObject 
            Destroy(gameObject);
        }
    }

    //Track the first finger
    private Touch trackedFinger1;
    //Start point of the finger gesture
    private Vector2 startPoint = Vector2.zero;
    //End point of the finger gesture
    private Vector2 endPoint = Vector2.zero;
    //Total time of the gesture
    private float gestureTime = 0;

    //PlayerMovement script
    private PlayerAnimation playerAnim;

    //for the dashing properties
    private float minSwipe = 150.0f;
    private bool noDash = false;

    private void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Assign the tracked finger
        if (Input.touchCount > 0)
        {
            trackedFinger1 = Input.GetTouch(0);

            if (trackedFinger1.phase == TouchPhase.Began)
            {
                gestureTime = 0;
                //assign first point position
                startPoint = trackedFinger1.position;
            }
            else if (trackedFinger1.phase == TouchPhase.Ended)
            {
                //assign end point position
                endPoint = trackedFinger1.position;
                //check if the player swipes the screen
                if (!noDash)
                {
                    isDash(startPoint, endPoint);
                }
                noDash = false;
                //if total gesture time is below max
                if (gestureTime <= _tapProperty.tapTime &&
                    //And if covered screen distance is below max- allowance for shakey fingers etc...
                    Vector2.Distance(startPoint, endPoint) < (Screen.dpi * _tapProperty.tapMaxDistance))
                {
                    //FireTapEvent(startPoint);
                }
            }
            //finger is on hold
            else
            {
                gestureTime += Time.deltaTime;
            }
            if (ButtonStateManager.Instance.onFireButton || ButtonStateManager.Instance.onJoystick)
            {
                noDash = true;
            }
            if (playerMove.MovementX == 0.0f && playerMove.MovementY == 0.0f &&
                !ButtonStateManager.Instance.onJoystick && !ButtonStateManager.Instance.onFireButton)
            {
                noDash = false;
            }
            Debug.Log("Joystick" + ButtonStateManager.Instance.onJoystick);
            Debug.Log("Fire" + ButtonStateManager.Instance.onFireButton);
        }
    }

    private void isDash(Vector2 start, Vector2 end)
    {

        float distance = Vector2.Distance(startPoint, endPoint);
        if (distance >= (_swipeProperty.minSwipeDistance * Screen.dpi) && gestureTime <= _swipeProperty.swipeTime)
        {
            if (!ButtonStateManager.Instance.onFireButton && !ButtonStateManager.Instance.onJoystick)
            {
                Vector2 dir = end - start;
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    if (dir.x > 0)
                    {
                        Debug.Log("Right");
                    }
                    else
                    {
                        Debug.Log("Left");
                    }
                }
                else
                {
                    if (dir.y > 0)
                    {
                        Debug.Log("Up");
                    }
                    else
                    {
                        Debug.Log("Down");
                    }
                }
                //this is the original
                /*if ((start.x - end.x) < -_swipeProperty.minSwipeDistance)
                {
                    playerAnim.dashAnimation(true);
                }
                else if ((start.x - end.x) > _swipeProperty.minSwipeDistance)
                {
                    playerAnim.dashAnimation(false);
                }*/
            }
        }
    }

    private void FireTapEvent(Vector2 pos)
    {
        Debug.Log("Tap");
        if (OnTap != null)
        {
            TapEventArgs args = new TapEventArgs(pos);
            OnTap(this, args);
        }
    }

    private void OnDrawGizmos()
    {
        if (Input.touchCount > 0)
        {
            Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
            Gizmos.DrawIcon(r.GetPoint(10), "Nezuko");
        }
    }
}
