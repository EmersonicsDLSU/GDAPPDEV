using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationControl : MonoBehaviour
{
    DeviceOrientation lastOrientation;

    private void Update()
    {
        lastOrientation = Input.deviceOrientation;
    }
    public void FingerUp()
    {
        //bring back the default rotation
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;

        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    public void FingerDown()
    {
        int orientation = (int)lastOrientation;
        //if phone is facing up/down
        if(orientation > 4)
        {
            orientation = (int)DeviceOrientation.LandscapeLeft;
        }

        //to prevent from auto rotating the phone when a finger is touching
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = (ScreenOrientation)orientation;
    }
}
