using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DragProperty
{
    [Tooltip("How long must the finger be on screen before we sent the drag event")]
    public float bufferTime = 0.8f;
}
