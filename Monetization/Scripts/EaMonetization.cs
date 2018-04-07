#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
public interface IEaAd
{
    void Init();
}


public enum AdType
{
    BANNER_TOP,
    BANNER_BOTTOM,
    INTERSTITIAL,
}
public enum EaAdSize
{
    BANNER,
    SMART_BANNER,
    IAB_BANNER,
    LEADERBOARD,
    MEDIUM_RECTANGLE

}
public delegate void eaAdmobDelegate(params AdType[] type);
public delegate void eaUnityAdsDelegate(Action onCompleted, Action onSkipped, Action onCancelled);
#if UNITY_ADS
public delegate void eaUnityResultDelegate(Action<ShowResult> result);
#endif
public class EaMonetization : EditorSingleton<EaMonetization>
{
#if UNITY_EDITOR
    [MenuItem("Ea/Monetization/Settings")]
    public static void Advetisement()
    {
        EaScriptable.CreateAsset<EaMonetization>("Assets/Ea/Monetization/Resources/");
    }
#endif




#if UNITY_ADS
    public static event eaUnityResultDelegate showUnityRewardAdsResult = delegate { };
#endif

    public static event eaAdmobDelegate showAdmob = delegate { };
    public static event eaAdmobDelegate hideAdmob = delegate { };
    public static event Action showUnityAds = delegate { };
    public static event eaUnityAdsDelegate showUnityRewardAds = delegate { };

    public static event Action onAdInitialize = delegate { };
    public bool showDebug, showDummyClient;
    public static bool initialized;





    public static void Init()
    {
        if (initialized)
            return;
        if (instance.showDebug) Debug.Log("EaMonetization initialized!".color("0000FF"));
        initialized = true;
        onAdInitialize();



    }
    public static void Show(params AdType[] types)
    {
        showAdmob(types);
    }
    public static void Hide(params AdType[] types)
    {
        hideAdmob(types);
    }
    public static void ShowVideo()
    {
        //Debug.Log("SV");
        showUnityAds();
    }
    public static void ShowRewardVideo(Action onCompleted, Action onSkipped = null, Action onCancelled = null)
    {
        showUnityRewardAds(onCompleted, onSkipped, onCancelled);
    }

#if UNITY_ADS
    public static void ShowRewardVideo(Action<ShowResult> result){
        showUnityRewardAdsResult(result);
    }
#endif

}




