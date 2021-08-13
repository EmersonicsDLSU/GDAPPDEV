using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UpdateScan : MonoBehaviour
{
    private AstarPath astar;
    void Start()
    {
        astar = GetComponent<AstarPath>();
    }

    // Update is called once per frame
    void Update()
    {
        //astar.Scan();
    }
}
