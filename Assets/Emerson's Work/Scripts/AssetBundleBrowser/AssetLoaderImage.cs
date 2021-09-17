using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//place this script on the UI image you want to have its image to change getting its source from the assetManager
public class AssetLoaderImage : MonoBehaviour
{
    //Reference to the AssetBundleManager
    public AssetBundleManager assetManager;
    //RImage to hold the loaded sprite
    public Image imageHolder;

    // Start is called before the first frame update
    void Start()
    {
        //Load the sprite named <sprite name>
        //From the textures bundle
        Sprite bg = assetManager.GetAsset<Sprite>("<bundle_name>", "image/png_name");
        
        //Assign the sprite
        imageHolder.sprite = bg;
    }

}
