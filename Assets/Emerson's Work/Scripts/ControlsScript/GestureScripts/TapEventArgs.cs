using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class TapEventArgs : EventArgs
{
    private Vector2 _tapPosition;
    private GameObject _tapObject;
    public Vector2 TapPostion
    {
        get { return _tapPosition; }
        private set { _tapPosition = value; }
    }

    public TapEventArgs(Vector2 pos, GameObject obj = null)
    {
        TapPostion = pos;
        _tapObject = obj;
    }
}
