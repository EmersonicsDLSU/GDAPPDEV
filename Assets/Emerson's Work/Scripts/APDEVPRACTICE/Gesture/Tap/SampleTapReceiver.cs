using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is for the array of pics
public class SampleTapReceiver : MonoBehaviour
{
    public GameObject[] spawn;
    public int count = 0;
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
        Instantiate(spawn[count++ % 2], pos, Quaternion.identity);
    }
}
