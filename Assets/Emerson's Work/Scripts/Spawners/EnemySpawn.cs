using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //enemy Properties
    [SerializeField] private int enemySizeLimit = 10;
    [SerializeField] private bool isFixedAllocation = true;
    [SerializeField] private float spawnDuration = 150.0f;

    //the type of pool and the originalObj; both are required, set you're preffered values for the constructors(maxSize, isFixAllocation?)
    [SerializeField] private GameObject enemyGO;
    [HideInInspector] public ObjPools enemyPool;
    private Transform poolableLocation;
    private string spawnName;

    //time count and time interval for the spawning
    private float ticks = 0.0f;
    private float spawn_interval = 2.0f;

    void Start()
    {
        this.spawnName = this.gameObject.name;
        poolableLocation = GetComponent<Transform>();
        enemyPool = new ObjPools(enemySizeLimit, isFixedAllocation);
        this.enemyPool.Initialize(ref this.enemyGO, ref this.poolableLocation, this.spawnName);
    }

    // Update is called once per frame
    void Update()
    {
        spawnDuration -= Time.deltaTime;
        if(spawnDuration > 0.0f)
        {

            if (this.ticks < spawn_interval)
            {
                this.ticks += Time.deltaTime;
            }

            else if (this.enemyPool.HasObjectAvailable(1))
            {
                this.ticks = 0.0f;

                this.enemyPool.RequestPoolable();
                relocateEnemy();

                this.spawn_interval = Random.Range(2.0f, 5.0f);
            }
        }
    }

    [SerializeField] private List<Transform> locationList;

    private void relocateEnemy()
    {
        //refreshes the new batch of enemies
        IEnemyFunctions enmyFunc = this.enemyPool.usedObjects[this.enemyPool.usedObjects.Count - 1].GetComponentInChildren<IEnemyFunctions>();
        enmyFunc.refreshEnemy();

        //randomly place the enemies to a position
        int randIndex = 0;
        if (locationList != null)
        {
            randIndex = Random.Range(0, locationList.Count);
        }
        //this.enemyPool.usedObjects[this.enemyPool.usedObjects.Count - 1].transform.parent = this.locationList[randIndex];
        this.enemyPool.usedObjects[this.enemyPool.usedObjects.Count - 1].transform.position = this.locationList[randIndex].position;
    }

    public void destroyEnemy(ref GameObject go)
    {
        //destroys the enemy
        foreach (var item in enemyPool.usedObjects)
        {
            EnemyStatistics stats = item.GetComponentInChildren<EnemyStatistics>();
            if (item.GetInstanceID() == go.GetInstanceID() && stats.isDead)
            {
                //Debug.Log("Found the dead object");

                //drop the collectible items
                DropItemSpawn dis = GameObject.Find("DropObjectsManager").GetComponent<DropItemSpawn>();
                dis.dropItemsFunction(go.GetComponent<Transform>().transform.position);
                //detroy the enemy
                StartCoroutine(releaseEnemy(go));
                //relocate it back to the enemy storage
                this.enemyPool.availableObjects[this.enemyPool.availableObjects.Count - 1].transform.parent = this.poolableLocation;
                this.enemyPool.availableObjects[this.enemyPool.availableObjects.Count - 1].transform.position = this.poolableLocation.position;
                //enemyList.Remove(item);
                //ICharacterAnimations enemyAnim = item.GetComponentInChildren<ICharacterAnimations>();
                //enemyAnim.destroyObj();
                //add money when the enemy was killed
                break;
            }
        }
    }
    IEnumerator releaseEnemy(GameObject go)
    {
        yield return new WaitForSeconds(GeneralAnimationProperty.mud_dead_delay);
        enemyPool.ReleasePoolable(ref go);
    }
}


/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawn : MonoBehaviour
{
    //enemy Properties
    [SerializeField] private GameObject enemyGO;
    [SerializeField] private int enemySizeLimit = 10;
    public List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private float spawnDuration = 150.0f;
    [SerializeField] private float spawnTimer = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnDuration -= Time.deltaTime;
        if(spawnDuration > 0.0f)
        {
            //spawns only if the enemysize is lower than the limit
            if (enemyList.Count < enemySizeLimit)
            {
                //starts the timer for the spawn
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0.0f)
                {
                    //resets the spawnTimer
                    spawnTimer = 2.0f;
                    Random rnd = new Random(); //needs to instantiate from the Random class
                    float spawnLoc = rnd.Next(-5, 5); //this will randomly generate from 1-5

                    //randomly spawn the object relatively to the spawnPosition range
                    GameObject newEnemy = Instantiate(enemyGO,
                        transform.position + (transform.right * spawnLoc),
                        Quaternion.identity) as GameObject;
                    newEnemy.transform.parent = this.transform;
                    enemyList.Add(newEnemy);
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void destroyEnemy(ref GameObject go)
    {
        //destroys the enemy
        foreach (var item in enemyList)
        {
            EnemyStatistics stats = item.GetComponentInChildren<EnemyStatistics>();
            if (item.GetInstanceID() == go.GetInstanceID() && stats.isDead)
            {
                //Debug.Log("Found the dead object");
                enemyList.Remove(item);
                ICharacterAnimations enemyAnim = item.GetComponentInChildren<ICharacterAnimations>();
                enemyAnim.destroyObj();
                //add money when the enemy was killed
                GameCredit.addCurrency(100);
                Debug.Log("Add money");
                break;
            }
        }
    }
}
 */
