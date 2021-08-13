using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwipeProperty
{
    [Tooltip("Maximum allowable time until its not a tap anymore")]
    public float swipeTime = 0.7f;
    [Tooltip("Maximum allowable distance until its not a tap anymore")]
    public float minSwipeDistance = 2.0f;
}
