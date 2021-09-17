using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public enum SwipeDirections
{
    RIGHT = 0, LEFT, UP, DOWN
}

public class SwipeEventArgs : EventArgs
{
    //Position of the swipe- usually the start position
    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;
    //General direction of the swipe
    private SwipeDirections swipeDirectiom;
    //Raw direction of the swipe
    private Vector2 swipeVector;
    //Hit object at start
    private GameObject hitObject;


    public SwipeEventArgs(Vector2 _swipeStartPos, Vector2 _swipeEndPos, SwipeDirections _swipeDir,
        Vector2 _swipeVector, GameObject _hitObject)
    {
        this.swipeStartPos = _swipeStartPos;
        this.swipeEndPos = _swipeEndPos;
        this.swipeDirectiom = _swipeDir;
        this.swipeVector = _swipeVector;
        this.hitObject = _hitObject;
    }

    public Vector2 SwipeStartPos
    {
        get { return this.swipeStartPos; }
    }
    public Vector2 SwipeEndPos
    {
        get { return this.swipeEndPos; }
    }
    public SwipeDirections SwipeDirection
    {
        get { return this.swipeDirectiom; }
    }
    public Vector2 SwipeVector
    {
        get { return this.swipeVector; }
    }
    public GameObject HitObject
    {
        get { return this.hitObject; }
    }
}
