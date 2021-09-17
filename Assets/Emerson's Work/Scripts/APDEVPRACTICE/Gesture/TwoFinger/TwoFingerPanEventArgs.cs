using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class TwoFingerPanEventArgs : EventArgs
{
    //Data of the first finger
    public Touch Finger1 { get; private set; }
    //Data of the second finger
    public Touch Finger2 { get; private set; }

    public TwoFingerPanEventArgs(Touch finger1, Touch finger2)
    {
        Finger1 = finger1;
        Finger2 = finger2;
    }
}
