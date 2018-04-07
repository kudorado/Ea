using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDemo3 : MonoBehaviour {

	void Start(){
		NodeDemo.On ("hello", () => Debug.Log ("SAY HELLO NODE DEMO FROM NODE 3"));
		NodeDemo.Emit ("hello");
	
	}


}
