using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPools
{
    //transform location of the spawn
    public Transform poolableLocation;
    public string spawnName;

    //max size of the pool and if its size isDynamic
    private int maxPoolSize = 20; //default
    private bool fixedAllocation = true; //default

    //collection of the available and used objects currently in the game
    public List<GameObject> availableObjects = new List<GameObject>();
    public List<GameObject> usedObjects = new List<GameObject>();

    //constructor
    public ObjPools(int maxPoolSize = 20, bool fixedAllocation = true)
    {
        this.maxPoolSize = maxPoolSize;
        this.fixedAllocation = fixedAllocation;
    }

    public int MaxPoolSize
    {
        get { return maxPoolSize; }
    }

    //call this in the awake or start of every spawnType you've created
    public void Initialize(ref GameObject obj, ref Transform poolableLocation, string spawnName)
    {
        this.poolableLocation = poolableLocation;
        this.spawnName = spawnName;
        PoolableObj copy = obj.GetComponent<PoolableObj>();
        for (int i = 0; i < maxPoolSize; i++)
        {
            availableObjects.Add(copy.createCopy(this));
            availableObjects[i].SetActive(false);
        }
    }

    //increase the maxSize of the pool
    public void increaseMaxPoolSize(int addSize)
    {
        if (!fixedAllocation)
        {
            maxPoolSize += addSize;
        }
        else
        {
            Debug.Log("Fixed Allocation is set to True!");
        }
    }

    public bool HasObjectAvailable(int size)
    {
        if (this.availableObjects.Count >= size && size > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RequestPoolable()
    {
        usedObjects.Add(availableObjects[0]);
        availableObjects.RemoveAt(0);
        //poolable now exist in the game
        usedObjects[usedObjects.Count - 1].SetActive(true);
        //sets the onActivate func of the poolable
        usedObjects[usedObjects.Count - 1].GetComponent<PoolableObj>().onActivate();
    }

    public void ReleasePoolable(int index)
    {
        availableObjects.Add(usedObjects[index]);
        usedObjects.RemoveAt(index);
        availableObjects[availableObjects.Count - 1].SetActive(false);
    }
    public void ReleasePoolable(ref GameObject go)
    {
        availableObjects.Add(go);
        foreach (var item in usedObjects)
        {
            if(go.GetInstanceID() == item.GetInstanceID())
            {
                usedObjects.Remove(go);
                go.SetActive(false);
                break;
            }
        }
    }
}
