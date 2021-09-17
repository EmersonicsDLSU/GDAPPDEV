using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class SpreadEventArgs : EventArgs
{
    //First Finger
    public Touch Finger1 { get; private set; }
    //Second Finger
    public Touch Finger2 { get; private set; }
    //Diff. in distance between the prev and current position of the fingers
    public float DistanceDelta { get; private set; } = 0;
    //Spreading / Pinching on something?
    public GameObject HitObject { get; private set; } = null;

    public SpreadEventArgs(Touch finger1, Touch finger2, float distanceDelta, GameObject hitObject)
    {
        Finger1 = finger1;
        Finger1 = finger2;
        DistanceDelta = distanceDelta;
        HitObject = hitObject;
    }
}
