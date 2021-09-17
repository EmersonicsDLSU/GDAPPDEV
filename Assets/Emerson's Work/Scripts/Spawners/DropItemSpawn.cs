using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSpawn : MonoBehaviour
{
    //enemy Properties
    [SerializeField] private int itemSizeLimit = 50;
    [SerializeField] private bool isFixedAllocation = true;

    //the type of pool and the originalObj; both are required, set you're preffered values for the constructors(maxSize, isFixAllocation?)
    [SerializeField] private GameObject [] itemGOs = null;
    [HideInInspector] public ObjPools [] itemPools = null;
    private Transform poolableLocation;
    private string spawnName;

    void Start()
    {
        this.spawnName = this.gameObject.name;
        poolableLocation = GetComponent<Transform>();
        itemPools = new ObjPools[itemGOs.Length];
        for (int i = 0; i < itemGOs.Length; i++)
        {
            itemPools[i] = new ObjPools(itemSizeLimit, isFixedAllocation);
            this.itemPools[i].Initialize(ref this.itemGOs[i], ref this.poolableLocation, this.spawnName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Droppable items and properties
    [SerializeField] private int minDrop = 3, maxDrop = 5;

    public void dropItemsFunction(Vector3 parentPos)
    {
        int objSpawnNum = Random.Range(minDrop, maxDrop + 1);
        while (objSpawnNum-- > 0)
        {
            int randObj = Random.Range(0, itemGOs.Length);
            //spawn the rand obj
            this.itemPools[randObj].RequestPoolable();
            //access the recently added obj
            this.itemPools[randObj].usedObjects[this.itemPools[randObj].usedObjects.Count - 1].GetComponent<Transform>().position
                = parentPos;
        }
    }

    public void destroyItem(ref GameObject go, int item_index)
    {
        //destroys the item
        foreach (var item in itemPools[item_index].usedObjects)
        {
            EnemyStatistics stats = item.GetComponentInChildren<EnemyStatistics>();
            if (item.GetInstanceID() == go.GetInstanceID())
            {
                //Debug.Log("Found the item object");
                itemPools[item_index].ReleasePoolable(ref go);
                break;
            }
        }
    }

}