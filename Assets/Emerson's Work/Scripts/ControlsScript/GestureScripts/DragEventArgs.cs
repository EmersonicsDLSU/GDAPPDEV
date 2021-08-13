using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class DragEventArgs : EventArgs
{
    //Current state of the finger
    private Touch targetFinger;
    //Hit object
    private GameObject hitObject;

    public DragEventArgs(Touch _targetFinger, GameObject _hitObject)
    {
        this.targetFinger = _targetFinger;
        this.hitObject = _hitObject;
    }

    public Touch TargetFinger
    {
        get { return this.targetFinger; }
    }
    public GameObject HitObject
    {
        get { return this.hitObject; }
    }
}
