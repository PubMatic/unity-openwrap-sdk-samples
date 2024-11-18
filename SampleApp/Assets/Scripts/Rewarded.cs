#if UNITY_IOS || UNITY_ANDROID
/*
* PubMatic Inc. ("PubMatic") CONFIDENTIAL
* Unpublished Copyright (c) 2006-2022 PubMatic, All Rights Reserved.
*
* NOTICE:  All information contained herein is, and remains the property of PubMatic. The intellectual and technical concepts contained
* herein are proprietary to PubMatic and may be covered by U.S. and Foreign Patents, patents in process, and are protected by trade secret or copyright law.
* Dissemination of this information or reproduction of this material is strictly forbidden unless prior written permission is obtained
* from PubMatic.  Access to the source code contained herein is hereby forbidden to anyone except current PubMatic employees, managers or contractors who have executed
* Confidentiality and Non-disclosure agreements explicitly covering such access or to such other persons whom are directly authorized by PubMatic to access the source code and are subject to confidentiality and nondisclosure obligations with respect to the source code.
*
* The copyright notice above does not evidence any actual or intended publication or disclosure  of  this source code, which includes
* information that is confidential and/or proprietary, and is a trade secret, of  PubMatic.   ANY REPRODUCTION, MODIFICATION, DISTRIBUTION, PUBLIC  PERFORMANCE,
* OR PUBLIC DISPLAY OF OR THROUGH USE  OF THIS  SOURCE CODE  WITHOUT  THE EXPRESS WRITTEN CONSENT OF PUBMATIC IS STRICTLY PROHIBITED, AND IN VIOLATION OF APPLICABLE
* LAWS AND INTERNATIONAL TREATIES.  THE RECEIPT OR POSSESSION OF  THIS SOURCE CODE AND/OR RELATED INFORMATION DOES NOT CONVEY OR IMPLY ANY RIGHTS
* TO REPRODUCE, DISCLOSE OR DISTRIBUTE ITS CONTENTS, OR TO MANUFACTURE, USE, OR SELL ANYTHING THAT IT  MAY DESCRIBE, IN WHOLE OR IN PART.
*/
using UnityEngine;
using OpenWrapSDK;
using System;

// Displays Rewarded ad and handles it's callbacks.
public class Rewarded : MonoBehaviour
{
    private readonly string OW_AD_UNIT = "OpenWrapRewardedAdUnit";
    private readonly string PUB_ID = "156276";
    private readonly int PROFILE_ID = 1757;

    [SerializeField] private GameObject showButton;

    private POBRewardedAd rewardedAd;

    private void OnEnable()
    {
        showButton.SetActive(false);

        // Create a Rewarded object
        // For test IDs refer - https://community.pubmatic.com/display/IOPO/Test+and+debug+your+integration
        rewardedAd = POBRewardedAd.GetRewardedAd(PUB_ID, PROFILE_ID, OW_AD_UNIT);

        // Register delegate for ad event handlers
        rewardedAd.OnAdLoaded += OnAdLoaded;
        rewardedAd.OnAdFailedToLoad += OnAdFailedToLoad;
        rewardedAd.OnAdFailedToShow += OnAdFailedToShow;
        rewardedAd.OnAdOpened += OnAdOpened;
        rewardedAd.OnAdClosed += OnAdClosed;
        rewardedAd.OnAdClicked += OnAdClicked;
        rewardedAd.OnAppLeaving += OnAppLeaving;
        rewardedAd.OnAdExpired += OnAdExpired;
        rewardedAd.OnReceiveReward += OnReceiveReward;
        rewardedAd.OnAdImpression += OnAdImpression;
    }

    private void OnDisable()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
    }

    public void LoadRewarded()
    {
        // Load Ad
        rewardedAd.LoadAd();
    }

    public void ShowRewarded()
    {
        if (rewardedAd.IsReady())
        {
            // Show Rewarded ad
            rewardedAd.ShowAd();
        }
    }

    // Callback method notifies that an ad has been received successfully.
    private void OnAdLoaded(object sender, EventArgs e)
    {
        showButton.SetActive(true);
        Debug.Log($"Rewarded : Ad Received");
    }

    // Callback method notifies an error encountered while loading or rendering an ad.
    private void OnAdFailedToLoad(object sender, POBErrorEventArgs error)
    {
        Debug.Log($"Rewarded : Ad failed to load with error - {error}");
    }

    // Callback method notifies an error encountered while showing an ad.
    private void OnAdFailedToShow(object sender, POBErrorEventArgs error)
    {
        Debug.Log($"Rewarded : Ad failed to show with error - {error}");
    }

    // Callback method notifies that the rewarded ad will be presented as a modal on top of the current view controller.
    private void OnAdOpened(object sender, EventArgs e)
    {
        Debug.Log($"Rewarded : Ad Opened");
    }

    // Callback method notifies that the rewarded ad has been animated off the screen.
    private void OnAdClosed(object sender, EventArgs e)
    {
        Debug.Log($"Rewarded : Ad Closed");
    }

    // Callback method notifies that the rewarded ad has been clicked.
    private void OnAdClicked(object sender, EventArgs e)
    {
        Debug.Log($"Rewarded : Ad Clicked");
    }

    // Callback method notifies that a user interaction will open another app (for example, App Store), leaving the current app.
    private void OnAppLeaving(object sender, EventArgs e)
    {
        Debug.Log($"Rewarded : App Leaving");
    }

    // Callback method notifies that the rewarded ad has expired.
    private void OnAdExpired(object sender, EventArgs e)
    {
        Debug.Log($"Rewarded : Ad Expired");
    }

    // Callback method notifies when an Ad has completed the minimum required viewing, and user should be rewarded.
    private void OnReceiveReward(object sender, POBRewardEventArgs rewardEventArgs)
    {
        Debug.Log($"Rewarded : Received Reward - {rewardEventArgs.reward.GetAmount()}({rewardEventArgs.reward.GetCurrencyType()})");
    }

    // Callback method notifies that the rewarded ad has recorded an impression.
    private void OnAdImpression(object sender, EventArgs e)
    {
        Debug.Log($"Rewarded : Ad Impression");
    }
}
#endif