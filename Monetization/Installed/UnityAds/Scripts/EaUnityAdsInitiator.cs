using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class EaUnityAdsInitiator : EaAdsInitiator<EaUnityAdsInitiator>
{

    // Use this for initialization
    public override void Init()
    {
        EaUnityAds.Init();
    }
	private void Update()
	{
        EaMonetization.ShowVideo();
	}


}
