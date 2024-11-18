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

// Displays In Banner Video ad and handles it's callbacks.
public class InBannerVideo : MonoBehaviour
{
    private readonly string OW_AD_UNIT = "OpenWrapBannerAdUnit";
    private readonly string PUB_ID = "156276";
    private readonly int PROFILE_ID = 1757;

    private POBBannerView bannerView;

    private void OnEnable()
    {
        // Create a banner view
        // For test IDs refer - https://community.pubmatic.com/display/IOPO/Test+and+debug+your+integration
        bannerView = new POBBannerView(PUB_ID, PROFILE_ID, OW_AD_UNIT, POBAdSize.Banner300x250, POBBannerPosition.BottomCenter);

        // Register delegate for ad event handlers
        bannerView.OnAdLoaded += OnAdLoaded;
        bannerView.OnAdFailedToLoad += OnAdFailedToLoad;
        bannerView.OnAppLeaving += OnAppLeaving;
        bannerView.OnAdOpened += OnAdOpened;
        bannerView.OnAdClosed += OnAdClosed;
        bannerView.OnAdClicked += OnAdClicked;
        bannerView.OnAdImpression += OnAdImpression;

        // Load Ad
        bannerView.LoadAd();
    }

    private void OnDisable()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    // Callback method notifies that an ad has been loaded successfully.
    private void OnAdLoaded(object sender, EventArgs e)
    {
        Debug.Log($"In Banner Video : Ad Received");
    }

    // Callback method notifies an error encountered while loading or rendering an ad.
    private void OnAdFailedToLoad(object sender, POBErrorEventArgs error)
    {
        Debug.Log($"In Banner Video : Ad failed to load with error - {error}");
    }

    // Callback method notifies that a user interaction will open another app (for example, App Store), leaving the current app.
    private void OnAppLeaving(object sender, EventArgs e)
    {
        Debug.Log("In Banner Video : App Leaving");
    }

    // Callback method notifies that the banner ad will be presented as a modal on top
    private void OnAdOpened(object sender, EventArgs e)
    {
        Debug.Log("In Banner Video : Ad Opened");
    }

    // Callback method notifies that the banner ad has been animated off the screen.
    private void OnAdClosed(object sender, EventArgs e)
    {
        Debug.Log("In Banner Video : Ad Closed");
    }

    // Callback method notifies that the banner ad has been clicked.
    private void OnAdClicked(object sender, EventArgs e)
    {
        Debug.Log("In Banner Video : Ad Clicked");
    }

    // Callback method notifies that the banner ad has recorded an impression.
    private void OnAdImpression(object sender, EventArgs e)
    {
        Debug.Log("In Banner Video : Ad Impression");
    }
}
#endif
