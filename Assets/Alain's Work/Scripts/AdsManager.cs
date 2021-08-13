using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_ANDROID
    string gameId = "4251515";
#else
    string gameId = "4251514";
#endif
    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }
    public void PlayAd()
    {
        if (Advertisement.IsReady("Apdev_InterstitialAd"))
        {
            Advertisement.Show("Apdev_InterstitialAd");
        }
    }

    public void PlayRewardAd()
    {
        if (Advertisement.IsReady("Appdev_RewardAd"))
        {
            Advertisement.Show("Appdev_RewardAd");
        }
        else
        {
            Debug.Log("Reward ad not ready");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("ads ready");
    }
    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("bidyo started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "Appdev_RewardAd" && showResult ==  ShowResult.Finished){
            Debug.Log("reward claimed");
            //Dito ilalagay Reward sa ads
            GameCredit.addCurrency(GameCredit.gameMoney);
        }
    }
}