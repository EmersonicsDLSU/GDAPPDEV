using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler2 : MonoBehaviour, ITap, ISpread, IRotate
{
    public float rotateSpeed = 1;
    public void OnRotate(RotateEventArgs args)
    {
        float angle = args.Angle * rotateSpeed;
        
        if(args.RotationDirection == RotationDirections.CW)
        {
            //Reverse the rotation on Clockwise
            angle *= -1;
        }

        transform.Rotate(0, 0, angle);
    }

    public float resizeSpeed = 5;
    public void OnSpread(SpreadEventArgs args)
    {
        //Get how much to scale the object
        //Remember to convert the pixels to DPI first
        float scale = (args.DistanceDelta / Screen.dpi) * resizeSpeed;
        Vector3 scaleVector = new Vector3(scale, scale, scale);
        //Modfigy the current scale
        transform.localScale += scaleVector;
    }

    public void OnTap()
    {
        Debug.Log($"Tapped on {this.gameObject.name }");
        //Destroy this object on Tap
        Destroy(this.gameObject);
    }
}
