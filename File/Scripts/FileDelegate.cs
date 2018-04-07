#define EA_AD_IMPLEMENT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

	public delegate void OnQuit();
	public delegate void OnBack();
	public delegate void OnHide();
	public delegate void OnPause(bool status);
public class FileDelegate : EaMonoSingleton<FileDelegate>{

		public  static event OnPause onPause = delegate{};
		public  static event OnBack onBack = delegate{};
		public  static event OnQuit onQuit = delegate {};

		public static List<string> openedFiles = new List<string> ();

		void OnApplicationQuit(){
			onQuit.Invoke ();
		}
		void OnApplicationPause(bool status){
			onPause.Invoke (status);

		} 
		void Update(){
			if (Input.GetKeyDown (KeyCode.Escape)) {
				onBack.Invoke ();
			}
		}

		public class Leaderboard {

		}

}
