using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
        #if UNITY_ADS
using UnityEngine.Advertisements;
        #endif
        #if UNITY_EDITOR
        using UnityEditor;
        #endif

        public class EaUnityAds : EditorSingleton<EaUnityAds>
        {
        #if UNITY_EDITOR

            [MenuItem("Ea/Monetization/Installed/Unity/Ads/Init")]
            public static void CreateInitiator()
            {
                EaScriptable.CreateInitiator<EaUnityAdsInitiator>();
            }

            [MenuItem("Ea/Monetization/Installed/Unity/Ads/Settings")]
            public static void Advetisement()
            {
                EaScriptable.CreateAsset<EaUnityAds>("Assets/Ea/Monetization/Installed/UnityAds/Resources/");
            }
          
        #endif
            //public bool usingServiceAds = true;
            public string unityIdAndroid = "1747849", unityIdIos ="1747850";

        #if UNITY_ADS
                public static bool isRewardVideoAvaiable { get { return Advertisement.IsReady(rewardPlacementId); } }
                public static bool isVideoAvaiable { get { return Advertisement.IsReady(videoPlacementId); } }
        #endif

            public const string rewardPlacementId = "rewardedVideo";
            public const string videoPlacementId = "video";

            public static void Init()
            {
                if (!EaMonetization.initialized)
                    EaMonetization.onAdInitialize += OnAdInitialize;

            }
            private static void OnAdInitialize()
            {

                  Debug.Log("EaUnityAds Initialized.".color(Color.blue));


        try
        {
#if UNITY_ADS
            EaMonetization.showUnityRewardAdsResult += ShowVideo;
#endif

            EaMonetization.showUnityRewardAds += ShowVideo;
            EaMonetization.showUnityAds += ShowVideo;
#if UNITY_ADS

            string uid = "";
#if UNITY_ANDROID
            uid = instance.unityIdAndroid;
#endif
#if UNITY_IOS
            uid = instance.unityIdIos;
#endif
                                Advertisement.Initialize(uid);

                            //if (EaMonetization.instance.showDebug) Debug.Log("Unity ads status: " + (Advertisement.isInitialized ? " Initialized." : "Not ready."));
#endif

        }
                catch (Exception e)
                {
                    Debug.Log("Unity ads service is not ready, please turn on unity ads then try again.\n " + e);
                }

            }
            public static void ShowVideo()
            {
        #if UNITY_ADS

                    if (Advertisement.IsReady(videoPlacementId))
                        Advertisement.Show();
        #endif
            }
            private static void ShowVideo(Action onFinished, Action onFailed = null, Action onSkipped = null)
            {
        #if UNITY_ADS

                    if (Advertisement.IsReady(rewardPlacementId))
                    {
                        ShowOptions options = new ShowOptions();
                        options.resultCallback = result =>
                        {
                            switch (result)
                            {
                                case ShowResult.Finished:
                                    onFinished.InvokeSafe();
                                    break;
                                case ShowResult.Failed:
                                    onFailed.InvokeSafe();
                                    break;
                                case ShowResult.Skipped:
                                    onSkipped.InvokeSafe();
                                    break;
                            }
                        };

                        Advertisement.Show(rewardPlacementId, options);
                    }
                    else if (EaMonetization.instance.showDebug)
                        Debug.Log("VIDEO is not avaiable!".color("00FFFF"));
        #endif

            }
        #if UNITY_ADS
                private static void ShowVideo(Action<ShowResult> resultCallback)
                {
                    if (Advertisement.IsReady(rewardPlacementId))
                    {
                        ShowOptions options = new ShowOptions();
                        options.resultCallback = resultCallback;
                        Advertisement.Show(rewardPlacementId, options);
                    }
                    else if (EaMonetization.instance.showDebug)
                        Debug.Log("VIDEO is not avaiable".color("00FFFF"));

                }
        #endif

        }
