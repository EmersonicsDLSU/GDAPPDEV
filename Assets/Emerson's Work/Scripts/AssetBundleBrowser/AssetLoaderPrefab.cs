using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//place this script on the gameObject you want to have its prefab renderer to change getting its source from the assetManager
public class AssetLoaderPrefab : MonoBehaviour
{
    //Reference to the AssetBundleManager
    public AssetBundleManager assetManager;
    //RImage to hold the loaded sprite
    public Image imageHolder;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        //Load the whole bundle of texture to access the sprites for the prefab
        //We just need to load the textures bundle first
        //We dont need to load the sprite itself
        AssetBundle textures = assetManager.LoadBundle("textures");

        //Load the game object Player
        //From the objects bundle
        GameObject player = assetManager.GetAsset<GameObject>("objects", "Player");

        //Check if object exist
        if(player != null)
        {
            Instantiate(player);
        }
    }
}
