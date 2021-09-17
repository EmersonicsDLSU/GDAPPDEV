using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //this is needed to acces the "Path" class

//you can now create bundles for images, prefabs, sprites, etc..
//when importing prefabs in bundles, make sure to pass in the textures of the prefab as well
//after importing all of the needed assets in your bundle, build the bundles now
public class AssetBundleManager : MonoBehaviour
{
    //Root path of the asset bundles
    public string BundleRootPath
    {
        get
        {
            //if were just playing the game in the editor
#if UNITY_EDITOR
            //for android, this path is uneditable
            //Use the streaming asset path when testing via editor
            return Application.streamingAssetsPath;
            //if the game is built
#elif UNITY_ANDROID
            return Application.persistentDataPath;
#endif
        }

    }

    //Dictionary of loaded bundles
    Dictionary<string, AssetBundle> LoadedBundles =
        new Dictionary<string, AssetBundle>();

    public AssetBundle LoadBundle(string bundle_name)
    {
        //Check if bundle is already loaded
        if(LoadedBundles.ContainsKey(bundle_name))
        {
            //Return the bundle
            return LoadedBundles[bundle_name];
        }
        
        //Load the bundle by combining the root path and the bundle name
        AssetBundle ret = AssetBundle.LoadFromFile(Path.Combine(BundleRootPath, bundle_name));
        
        //No bundle found
        if(ret == null)
        {
            Debug.LogError($"Failed to load {bundle_name}");
        }
        else
        {
            //Add the new bundle to the dictionary
            LoadedBundles.Add(bundle_name, ret);
        }

        return ret;
    }

    //Generic type or template
    //Can be- Sprite, Texture, AudioClip.. etc
    public T GetAsset<T>(string bundle_name, string asset)where T : Object
    {
        //Init the return to null
        T ret = null;
        //Get the bundle
        AssetBundle bundle = LoadBundle(bundle_name);

        //Check if you got a bundle
        if(bundle != null)
        {
            //Get asset of type T in Bundle
            //If no asset of that name is found- it'll return to null
            ret = bundle.LoadAsset<T>(asset);
        }

        return ret;
    }
}
