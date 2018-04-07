using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Node<T> : EaSingleton<T>  where T : MonoBehaviour{
	private static Dictionary<string,List<Action>> events = new Dictionary<string,List<Action>>();
	public static void Emit(string eventKey){
		if(events.ContainsKey(eventKey))
			for(int i = 0 ; i < events[eventKey].Count; i++)
				events [eventKey][i].InvokeSafe ();
	}

	public static void On(string eventKey,Action callback){
		if (!events.ContainsKey (eventKey)) 
			events.Add (eventKey, new List<Action> ());

	
		events[eventKey].Add (callback);
	}
	public static void On(string [] eventKeys,Action callback){


		for (int i = 0; i < eventKeys.Length; i++) {
			if (!events.ContainsKey (eventKeys[i])) 
				events.Add (eventKeys[i], new List<Action> ());

			events[eventKeys[i]].Add (callback);
//			Debug.Log ("added event:" + eventKeys [i]);

		}
	}
	public static void On(Action callback, params string [] eventKeys){
		

		for (int i = 0; i < eventKeys.Length; i++) {
			if (!events.ContainsKey (eventKeys[i])) 
				events.Add (eventKeys[i], new List<Action> ());
			
			events[eventKeys[i]].Add (callback);

		}
	}
}
