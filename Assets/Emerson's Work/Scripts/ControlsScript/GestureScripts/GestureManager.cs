using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class GestureManager : MonoBehaviour
{
    public EventHandler<TapEventArgs> OnTap;
    public EventHandler<DragEventArgs> OnDrag;
    public EventHandler<SwipeEventArgs> OnSwipe;
    public EventHandler<TwoFingerPanEventArgs> OnTwoFingerPan;
    public EventHandler<SpreadEventArgs> OnSpread;
    public EventHandler<RotateEventArgs> OnRotate;

    [HideInInspector] public static GestureManager Instance;

    //Gets the properties for the Tap Gesture
    public TapProperty _tapProperty;
    //Gets the properties for the Swipe Gesture
    public SwipeProperty _swipeProperty;
    //Gets the properties for the Drag Gesture
    public DragProperty _dragProperty;
    //Gets the properties for the Two finger pan Gesture
    public TwoFingerPanProperty _twoFingerPanProperty;
    //Gets the properties for the Spread / Pinch Gesture
    public SpreadProperty _spreadProperty;
    //Gets the properties for the Two finger rotate Gesture
    public RotateProperty _rotateProperty;

    //player movement script
    private PlayerMovement playerMove;

    // Start is called before the first frame update
    public void Awake()
    {
        //assigns the one instance
        if(Instance == null)
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
    //Track the second finger
    private Touch trackedFinger2;
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
        dash_coolDown = original_dash_coolDown;
    }

    void Update()
    {
        //check cooldown for dash
        checkDashCD();
        //Assign the tracked finger
        if (Input.touchCount > 0)
        {
            if(Input.touchCount == 1)
            {
                //Move the previous code for single finger checks here
                checkSingleFingerGestures();
            }
            else if(Input.touchCount > 1)
            {
                //Assign the finger being tracked
                trackedFinger1 = Input.GetTouch(0);
                trackedFinger2 = Input.GetTouch(1);

                //Checked first if the 2 fingers have moved
                if ((trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Moved)
                    && Vector2.Distance(trackedFinger1.position, trackedFinger2.position) <= (Screen.dpi * _twoFingerPanProperty.maxDistance))
                {
                    //FireTwoFingerPanEvent();
                }

                //Check if either finger moved
                if (trackedFinger1.phase == TouchPhase.Moved || trackedFinger2.phase == TouchPhase.Moved)
                {
                    //Get the positions of the finger in the previous frame
                    Vector2 prevPoint1 = GetPreviousPOint(trackedFinger1);
                    Vector2 prevPoint2 = GetPreviousPOint(trackedFinger2);

                    //Get the distances
                    float currDistance = Vector2.Distance(trackedFinger1.position, trackedFinger2.position);
                    float prevDistance = Vector2.Distance(prevPoint1, prevPoint2);

                    if (Mathf.Abs(currDistance - prevDistance) >= (_spreadProperty.MinDistanceChange * Screen.dpi))
                    {
                        //FingerSpreadEvent(currDistance - prevDistance);
                    }
                }

                //Checked if either finger moved
                if ((trackedFinger1.phase == TouchPhase.Moved || 
                    trackedFinger2.phase == TouchPhase.Moved) &&
                    //Check if the distance bet the 2 fingers is greater than the set minimum
                    Vector2.Distance(trackedFinger1.position, trackedFinger2.position) >= 
                    (Screen.dpi * _rotateProperty.minDistance))
                {
                    //Get the positions of the finger in the previous frame
                    Vector2 prevPoint1 = GetPreviousPOint(trackedFinger1);
                    Vector2 prevPoint2 = GetPreviousPOint(trackedFinger2);

                    //Get the vectors of the fingers and their respective prev. position
                    Vector2 diffVector = trackedFinger1.position - trackedFinger2.position;
                    Vector2 prevDiffVector = prevPoint1 - prevPoint2;

                    float angle = Vector2.Angle(prevDiffVector, diffVector);

                    //Check if angle is more than the required amount
                    if(angle >= _rotateProperty.minChange)
                    {
                        //FireRotateEvent(diffVector, prevDiffVector, angle);
                    }
                }
            }
        }
    }

    public float original_dash_coolDown = 5.0f;
    [HideInInspector]public float dash_coolDown = 5.0f;
    private float dash_cooldown_ticks = 0.0f;
    private bool onDashCD = false;
    //the healthbar ui component
    [SerializeField] private Slider dashSlider;

    private void checkDashCD()
    {
        //timer for dash cooldown
        if (onDashCD)
        {
            dash_cooldown_ticks += Time.deltaTime;
            dashSlider.value = dash_cooldown_ticks / dash_coolDown;
            noDash = true;
            Debug.Log($"Time: dash_cooldown_ticks");
            if (dash_cooldown_ticks >= dash_coolDown)
            {
                dash_cooldown_ticks = 0.0f;
                noDash = false;
                onDashCD = false;
            }
        }
        else
        {
            dashSlider.value = 1;
        }
    }

    private void checkSingleFingerGestures()
    {
        trackedFinger1 = Input.GetTouch(0);

        //can't dash while shooting or moving
        if (ButtonStateManager.Instance.onFireButton || ButtonStateManager.Instance.onJoystick)
        {
            noDash = true;
        }
        //can dash again if not touching anything on the screen and not on dashCooldown
        if (!onDashCD && playerMove.MovementX == 0.0f && playerMove.MovementY == 0.0f &&
            !ButtonStateManager.Instance.onFireButton && !ButtonStateManager.Instance.onJoystick)
        {
            noDash = false;
        }

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
            //if total gesture time is below max
            if (gestureTime <= _tapProperty.tapTime &&
                //And if covered screen distance is below max- allowance for shakey fingers etc...
                Vector2.Distance(startPoint, endPoint) < (Screen.dpi * _tapProperty.tapMaxDistance))
            {
                //FireTapEvent(startPoint);
            }
            //for the dragevent; release the obj
            stillHoldObj = null;
            if(fromHold)
            {
                fromHold = false;
                //scans the new location
                GameObject.Find("A*").GetComponent<AstarPath>().Scan();
            }
        }
        //finger is on hold
        else
        {
            gestureTime += Time.deltaTime;
            //If the finger is on screen long enough
            if (gestureTime >= _dragProperty.bufferTime && !ButtonStateManager.Instance.onJoystick && !ButtonStateManager.Instance.onFireButton)
            {
                FireDragEvet();
            }
        }
        //Debug.Log("Joystick" + ButtonStateManager.Instance.onJoystick);
        //Debug.Log("Fire" + ButtonStateManager.Instance.onFireButton);
    }

    private bool fromHold = false;

    private void FireRotateEvent(Vector2 diffVector, Vector2 prevDiffVector, float angle)
    {
        Vector3 cross = Vector3.Cross(prevDiffVector, diffVector);
        RotationDirections rotDir = RotationDirections.CW;
        if (cross.z > 0)
        {
            Debug.Log($"Rotate CCW {angle}");
            rotDir = RotationDirections.CCW;
        }
        else
        {
            Debug.Log($"Rotate CW {angle}");
            rotDir = RotationDirections.CW;
        }

        Vector2 mid = Midpoint(trackedFinger1.position, trackedFinger2.position);

        Vector2 r = Camera.main.ScreenToWorldPoint(mid);
        RaycastHit2D hit = Physics2D.Raycast(r, -Vector2.up, Mathf.Infinity);
        GameObject hitObj = null;

        if (hit.collider.gameObject != null)
        {
            hitObj = hit.collider.gameObject;
        }

        RotateEventArgs args = new RotateEventArgs(trackedFinger1, trackedFinger2, angle,
                                                    rotDir, hitObj);

        if (OnRotate != null)
        {
            OnRotate(this, args);
        }

        if (hitObj != null)
        {
            IRotate rotate = hitObj.GetComponent<IRotate>();
            if (rotate != null)
            {
                rotate.OnRotate(args);
            }
        }
    }

    private void FingerSpreadEvent(float dist)
    {
        Debug.Log("Pinch / Spread");
        //If change is positve, then Spread
        //Pinch if otherwise
        if(dist > 0)
        {
            Debug.Log("Spread");
        }
        else
        {
            Debug.Log("Pinch");
        }

        Vector2 mid = Midpoint(trackedFinger1.position, trackedFinger2.position);

        Vector2 r = Camera.main.ScreenToWorldPoint(mid);
        RaycastHit2D hit = Physics2D.Raycast(r, -Vector2.up, Mathf.Infinity);
        GameObject hitObj = null;

        if (hit.collider.gameObject != null)
        {
            hitObj = hit.collider.gameObject;
        }

        SpreadEventArgs args = new SpreadEventArgs(trackedFinger1, trackedFinger2, dist, hitObj);

        if(OnSpread != null)
        {
            OnSpread(this, args);
        }

        if (hitObj != null)
        {
            ISpread spread = hitObj.GetComponent<ISpread>();
            if(spread != null)
            {
                spread.OnSpread(args);
            }
        }
    }

    private Vector2 Midpoint(Vector2 p1, Vector2 p2)
    {
        return (p1 + p2) / 2;
    }



    private Vector2 GetPreviousPOint(Touch finger)
    {
        //Subtract with delta to get the prev. position
        return finger.position - finger.deltaPosition;
    }

    private void FireTwoFingerPanEvent()
    {
        //Debug.Log("Two finger pan!");

        TwoFingerPanEventArgs args = new TwoFingerPanEventArgs(trackedFinger1, trackedFinger2);

        if(OnTwoFingerPan != null)
        {
            OnTwoFingerPan(this, args);
        }
    }

    private GameObject stillHoldObj = null;
    //drag event
    private void FireDragEvet()
    {
        Vector2 r = Camera.main.ScreenToWorldPoint(trackedFinger1.position);
        RaycastHit2D hit = Physics2D.Raycast(r, -Vector2.up, Mathf.Infinity);
        GameObject hitObj = null;

        if(hit.collider != null)
        {
            //different movable objects wont try swapping when a drag event is happening
            if(stillHoldObj != null && 
                hit.collider.gameObject.GetComponent<ObjectHandler>() != null && 
                hit.collider.gameObject.GetInstanceID() != stillHoldObj.GetInstanceID())
            {
                //Debug.Log("still holding");
                hitObj = stillHoldObj;
            }
            else
            {
                //Debug.Log("still pointing");
                hitObj = hit.collider.gameObject;
                stillHoldObj = hitObj;
            }
        }

        if(hitObj != null)
        {
            //Debug.Log(hitObj.name);
        }
        else
        {
            //Debug.Log("no object");
        }

        DragEventArgs args = new DragEventArgs(trackedFinger1, hitObj);
        if(OnDrag != null)
        {
            OnDrag(this, args);
        }

        if(hitObj != null)
        {
            //Debug.Log("Object found");
            IDragged drag = hitObj.GetComponent<IDragged>();
            if(drag != null)
            {
                drag.OnDrag(args);
                fromHold = true;
            }
            else
            {
                //Debug.Log("No IDragged Component!");
            }
        }
    }

    //swipe event
    private void isDash(Vector2 start, Vector2 end)
    {
        //Debug.Log("Swipe!");
        Vector2 dir = end - start;

        GameObject hitObj = null;
        Vector2 r = Camera.main.ScreenToWorldPoint(startPoint);
        RaycastHit2D hit = Physics2D.Raycast(r, -Vector2.up, Mathf.Infinity);
        if (hit.collider.gameObject != null)
            hitObj = hit.collider.gameObject;

        if(hit.collider.gameObject != null) 
        {
            hitObj = hit.collider.gameObject;
        }

        float distance = Vector2.Distance(start, end);
        if (distance >= (_swipeProperty.minSwipeDistance * Screen.dpi) && gestureTime <= _swipeProperty.swipeTime)
        {
            //can't dash after because of the coolDown
            onDashCD = true;
            noDash = true;
            //Debug.Log("Fire: " + ButtonStateManager.Instance.onFireButton);
            //Debug.Log("Joystick: " + ButtonStateManager.Instance.onJoystick);
            SwipeDirections swipeDir = SwipeDirections.RIGHT; //default

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0)
                {
                    //Debug.Log("Right");
                    playerAnim.dashAnimation(true);
                    swipeDir = SwipeDirections.RIGHT;
                }
                else
                {
                    //Debug.Log("Left");
                    playerAnim.dashAnimation(false);
                    swipeDir = SwipeDirections.LEFT;
                }
            }
            else
            {
                if (dir.y > 0)
                {
                    //Debug.Log("Up");
                    playerAnim.dashAnimation(false);
                    swipeDir = SwipeDirections.UP;
                }
                else
                {
                    //Debug.Log("Down");
                    playerAnim.dashAnimation(false);
                    swipeDir = SwipeDirections.DOWN;
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

            SwipeEventArgs args = new SwipeEventArgs(start, end, swipeDir, dir, hitObj);
            if(OnSwipe != null)
            {
                //Fire the OnSwipe event
                OnSwipe(this, args);
            }

            //checks if the go has a script component with ISwipped; can swipe anywhere

            ISwipped swipe = GameObject.FindGameObjectWithTag("Player").GetComponent<ISwipped>();
            if (swipe != null)
            {
                swipe.OnSwipe(args);
            }
        }
    }

    //tap event
    private void FireTapEvent(Vector2 pos)
    {
        GameObject hitObj = null;
        Vector2 r = Camera.main.ScreenToWorldPoint(trackedFinger1.position);
        RaycastHit2D hit = Physics2D.Raycast(r, -Vector2.up, Mathf.Infinity);
        if (hit.collider.gameObject != null)
            hitObj = hit.collider.gameObject;

        //Debug.Log("Tap");
        if (OnTap != null)
        {
            TapEventArgs args = new TapEventArgs(pos, hitObj);
            OnTap(this, args);
        }

        if(hitObj != null)
        {
            //assuming all objects to be tap have this script
            ITap handler = hitObj.GetComponent<ITap>();
            //If the hit object has the SpawnedHandler script
            //Call its OnTap func
            if (handler != null)
                handler.OnTap();
        }
    }

    private void OnDrawGizmos()
    {
        if(Input.touchCount > 0)
        {
            Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
            Gizmos.DrawIcon(r.GetPoint(10), "Nezuko");

            //Only draw the other when there is more than 1 finger onscreen
            if(Input.touchCount > 1)
            {
                Ray r2 = Camera.main.ScreenPointToRay(trackedFinger2.position);
                Gizmos.DrawIcon(r2.GetPoint(10), "Tanjiro");
            }
        }
    }
}
