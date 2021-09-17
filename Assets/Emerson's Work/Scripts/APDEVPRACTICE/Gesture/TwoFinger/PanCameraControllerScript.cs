using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place this script as a component for the camera
//Move the camera based on how the 2 fingers moveed
public class PanCameraControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnTwoFingerPan += OnTwoFingerPan;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTwoFingerPan -= OnTwoFingerPan;
    }

    public float speed = 10;
    private void OnTwoFingerPan(object sender, TwoFingerPanEventArgs args)
    {
        //Assign the variables for easy access
        Vector2 delta1 = args.Finger1.deltaPosition;
        Vector2 delta2 = args.Finger2.deltaPosition;
        //Get the average change between the two fingers
        Vector2 ave = new Vector2(
                                   (delta1.x + delta2.x) / 2,
                                   (delta1.y + delta2.y) / 2);
        //Since the average is in pixels- conver it to a lower value
        ave = ave / Screen.dpi;
        //Compute the change in position
        Vector3 change = (Vector3)ave * speed;
        //Assign he position of the camera
        transform.position += change;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
