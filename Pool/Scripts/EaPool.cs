using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class EaPool : EditorSingleton<EaPool> {
	#if UNITY_EDITOR
	[MenuItem("Ea/Pool")]
	public static void PoolSetting(){
		EaScriptable.CreateAsset<EaPool> ("Assets/Ea/Pool/Resources/");
	}
	#endif
	public   PoolData []  initData;
	private  Dictionary<string,int> poolHolder = new Dictionary<string, int>();
	private  List<GameObject[]> poolList = new List<GameObject[]> ();
	private  EaPool _instance;
	 static GameObject inst;
//	 private bool initialized = false;
	public void Init(){

		
		int length = initData.Length;
		for (int i = 0; i < length; i++) {
			poolHolder.Add (initData [i].key, i);
			poolList.Add(new GameObject[initData[i].poolAmount]);

			for (int init = 0; init < initData [i].poolAmount; init++) {
				inst = Instantiate (initData [i].source);
				inst.gameObject.SetActive (false);
				poolList[i][init] = inst;

			}
		}
//		Debug.Log (poolList[0].Length);
	}
	public T GetPool<T>(string key,bool actived = true){
		int index = poolHolder [key];
//		Debug.Log(index);
		int length = poolList [index].Length;
		for (int i = 0; i < length; i++) {
			if (!poolList [index] [i].activeSelf) {
				poolList [index] [i].SetActive (actived);
				return poolList [index] [i].GetComponent<T>();
			}
		}
		return default(T);
	}
}
[Serializable]
public class PoolData {
	public string key;
	public int poolAmount;
	public GameObject source;

}