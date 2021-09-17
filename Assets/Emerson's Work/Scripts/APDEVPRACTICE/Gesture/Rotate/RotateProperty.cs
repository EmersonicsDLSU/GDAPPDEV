using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotateProperty
{
    [Tooltip("Minimmum distance between between the fingers")]
    public float minDistance = 0.75f;
    [Tooltip("Minimmum change of the axis / angle")]
    public float minChange = 0.4f;
}
