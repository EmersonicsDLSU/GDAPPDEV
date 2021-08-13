using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class TapEventArgs : EventArgs
{
    private Vector2 tapPosition;
    public Vector2 TapPostion
    {
        get { return tapPosition; }
        private set { tapPosition = value; }
    }

    public TapEventArgs(Vector2 pos)
    {
        TapPostion = pos;
    }
}
