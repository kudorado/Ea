using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FileDemo{
public class FileDemo : MonoBehaviour {
		public class DataDemo : EaSerializable{
			public int gold;
		}
		DataDemo data;
		DataDemo data1;
		void Start(){
			OpenOrCreateData ();
//			data.gold = 100; 
//			data1.gold = 50;
			ReadData ();
		}
		void OpenOrCreateData(){
			data = EaFile.Open<DataDemo> ();
			data1 = EaFile.Open<DataDemo> ("anotherData");
		}
		void ReadData(){
			Debug.Log (data.gold);
			Debug.Log (data1.gold);
		}
	}

}
