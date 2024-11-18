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
using System.Collections;
using OpenWrapSDK;
using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class NoAdServerMenu : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_IOS
        // Check the user's consent status.
        // If the status is undetermined, display the request:
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif
    }

    public void InitOWSDK()
    {
         POBOpenWrapSDKInitListener initListener = new POBOpenWrapSDKInitListener();
         string pubId = "156276";
         List<int> profileIdsList = new List<int> { 1757, 1165 };

         Debug.Log("Initializing OpenWrap SDK.");
      
        OpenWrapSDKConfig config = new OpenWrapSDKConfig(pubId, profileIdsList);
        initListener.OnSuccess += OnSDKInitSuccess;
        initListener.OnFailure += OnSDKInitFailed;

        POBOpenWrapSDK.Initialize(config, initListener);   
    }

    public void OnSDKInitSuccess(object sender, EventArgs e)
    {
        Debug.Log("OpenWrap SDK initialization successful");
    }

    public void OnSDKInitFailed(object sender, POBErrorEventArgs errorArgs)
    {
        Debug.Log($"OpenWrap SDK initialization failed with error : {errorArgs.Message}");
    }

    private IEnumerator Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation) && !Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        else
        {
            Debug.Log("FineLocation and CoarseLocation permissions are already granted");
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        else
        {
            Debug.Log("External Storage Write permission already granted");
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
        else
        {
            Debug.Log("External Storage Read permission already granted");
        }
#endif
        InitOWSDK();
        POBOpenWrapSDK.SetLogLevel(POBSDKLogLevel.All);
        POBOpenWrapSDK.SetSSLEnabled(false);

        POBApplicationInfo appInfo = new POBApplicationInfo();
        appInfo.StoreURL = new Uri("https://itunes.apple.com/us/app/pubmatic-sdk-app/id1175273098?mt=8");

        POBOpenWrapSDK.SetApplicationInfo(appInfo);

        // Reference: https://docs.unity3d.com/ScriptReference/LocationService.Start.html
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }
}
#endif