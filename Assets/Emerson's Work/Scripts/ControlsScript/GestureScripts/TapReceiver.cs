using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnTap += OnTapHere;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTap -= OnTapHere;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTapHere(object sender, TapEventArgs args)
    {
        Ray r = Camera.main.ScreenPointToRay(args.TapPostion);
        Vector3 pos = r.GetPoint(10);
        SpawnObject(pos);
    }

    public void SpawnObject(Vector3 pos)
    {
        Instantiate(spawn, pos, Quaternion.identity);
    }
}
