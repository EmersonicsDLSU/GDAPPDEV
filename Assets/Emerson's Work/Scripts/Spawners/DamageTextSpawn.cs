using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageTextSpawn : MonoBehaviour
{
    //enemy Properties
    [SerializeField] private int textSizeLimit = 10;
    [SerializeField] private bool isFixedAllocation = true;

    //the type of pool and the originalObj; both are required, set you're preffered values for the constructors(maxSize, isFixAllocation?)
    [SerializeField] private GameObject textGo;
    [HideInInspector] public ObjPools textPool;
    private Transform poolableLocation;
    private string spawnName;

    void Start()
    {
        this.spawnName = this.gameObject.name;
        poolableLocation = GetComponent<Transform>();
        textPool = new ObjPools(textSizeLimit, isFixedAllocation);
        this.textPool.Initialize(ref this.textGo, ref this.poolableLocation, this.spawnName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] private float offsetPoint = 0.3f;

    public void relaseText(int damage, Transform trans, Color color, bool isFacingRight)
    {
        this.textPool.RequestPoolable();
        //change the damage value text and its color (Red - effective, White - normal, Black - weak)
        this.textPool.usedObjects[this.textPool.usedObjects.Count - 1].transform.GetChild(0).
            GetComponent<TextMeshProUGUI>().text = damage.ToString();
        this.textPool.usedObjects[this.textPool.usedObjects.Count - 1].transform.GetChild(0).
            GetComponent<TextMeshProUGUI>().color = color;
        this.textPool.usedObjects[this.textPool.usedObjects.Count - 1].
            transform.parent = trans;
        //craete a random offset
        float offset = Random.Range(-offsetPoint, offsetPoint);
        //assign the position of the text in relevance with the hitObj position
        this.textPool.usedObjects[this.textPool.usedObjects.Count - 1].
            transform.position = new Vector3(trans.position.x + offset, trans.position.y, trans.position.z);
        //flips the canvas if the character is facing to the left

        Flip(isFacingRight);

        StartCoroutine(destroyWait());
    }
    private void Flip(bool isFacingRight)
    {
        if (isFacingRight)
        {
            this.textPool.usedObjects[this.textPool.usedObjects.Count - 1].GetComponent<Animator>().SetBool("isFacingRight", true);
            Debug.LogError("Text Right");
        }
        else if (!isFacingRight)
        {
            this.textPool.usedObjects[this.textPool.usedObjects.Count - 1].GetComponent<Animator>().SetBool("isFacingRight", false);
            Debug.LogError("Text Left");
        }
    }

    [SerializeField] private float text_duration = 10.5f;
    public IEnumerator destroyWait()
    {
        yield return new WaitForSeconds(text_duration);
        destroyText();
    }

    public void destroyText()
    {
        //detroy the enemy
        textPool.ReleasePoolable(0);
        //return it back to the textHandlerSpawner
        this.textPool.availableObjects[this.textPool.availableObjects.Count - 1].
            transform.parent = this.transform;
    }
}
