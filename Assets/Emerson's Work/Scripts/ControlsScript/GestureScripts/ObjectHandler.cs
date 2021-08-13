using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour, IDragged
{
    public float speed = 10;
    private Vector3 TargetPos = Vector3.zero;

    public void OnDrag(DragEventArgs args)
    {
        if(args.HitObject == this.gameObject)
        {
            Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
            Vector3 worldPoint = r.GetPoint(10);

            TargetPos = worldPoint;
            this.transform.position = worldPoint;
            //this.GetComponent<Rigidbody2D>().velocity = 1 * (worldPoint - this.transform.position);
        }
    }
}
