using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class AdController : MonoBehaviour
{
    private string idReward;
    private RewardedAd rewardedAd;
    //ca-app-pub-2923048171857012/1778241058
    public void Start()
    {
        idReward = "ca-app-pub-3940256099942544/5224354917";
        
        this.rewardedAd = new RewardedAd(idReward);
        //CreateAndLoadRewardedAd();
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
            DoublePairController.Instance.ShowAd();
        }
    }

}
