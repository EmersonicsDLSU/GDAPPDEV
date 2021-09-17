using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add this script component for object you want to delete on tap
public class SpawnedHandler : MonoBehaviour, ITap, ISwipped, IDragged
{
    public float speed = 10;
    private Vector3 TargetPos = Vector3.zero;

    private void OnEnable()
    {
        TargetPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
    }
    public void OnDrag(DragEventArgs args)
    {
        if(args.HitObject == gameObject)
        {
            Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
            Vector3 worldPoint = r.GetPoint(10);

            TargetPos = worldPoint;
            transform.position = worldPoint;
        }
    }

    public void OnSwipe(SwipeEventArgs args)
    {
        Vector3 dir = Vector3.zero;

        switch(args.SwipeDirection)
        {
            case SwipeDirections.UP: dir.y = 1; break;
            case SwipeDirections.DOWN: dir.y = -1; break;
            case SwipeDirections.LEFT: dir.x = -1; break;
            case SwipeDirections.RIGHT: dir.x = 1; break;
        }

        TargetPos += (dir * 5);
    }

    public void OnTap()
    {
        Debug.Log($"Tapped on {this.gameObject.name }");
        //Destroy this object on Tap
        Destroy(this.gameObject);
    }
}
