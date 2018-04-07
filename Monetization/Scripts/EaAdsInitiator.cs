using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EaAdsInitiator<T>: EaManagerSingleton<T> where T : MonoBehaviour
{

    protected override void Awake()
	{
        base.Awake();
        Init();
       
	}
	private void OnEnable()
	{
        EaMonetization.Init();
	}

	public abstract void Init();
}
