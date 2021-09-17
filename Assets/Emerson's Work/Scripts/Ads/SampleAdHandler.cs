using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class SampleAdHandler:  MonoBehaviour
{
    
    //Ads Manager reference
    public AdvertismentManager adsManager;
    
    private void Start()
    {
        adsManager.OnAdDone += OnAdDone;
       
    }

    public void OnAdDone(object sender, AdFinishEventArgs args)
    {
        if(args.PlacementID == AdvertismentManager.SampleRewarded)
        {
            switch(args.AdResult)
            {
                case ShowResult.Failed: Debug.Log("Ad Fail"); break;
                case ShowResult.Skipped: Debug.Log("Ad Skipped"); break;
                case ShowResult.Finished: 
                    Debug.Log("Ad Finished");
                    GameCredit.addCurrency(1000);
                    break;
            }
           
        }
    }

    private void Update()
    {

    }
}
