using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public enum RotationDirections
{
    CW = 0, CCW
}

public class RotateEventArgs : EventArgs
{
    //First Finger
    public Touch Finger1 { get; private set; }
    //Second Finger
    public Touch Finger2 { get; private set; }
    //Change in angle
    public float Angle { get; private set; }
    //Rotation Direction
    public RotationDirections RotationDirection { get; private set; } = RotationDirections.CW;
    //Rotating on something?
    public GameObject HitObject { get; private set; } = null;

    public RotateEventArgs(Touch finger1, Touch finger2, float angle, RotationDirections rotDir, GameObject hitObj)
    {
        this.Finger1 = finger1;
        this.Finger2 = finger2;
        this.Angle = angle;
        this.RotationDirection = rotDir;
        this.HitObject = hitObj;
    }

}
