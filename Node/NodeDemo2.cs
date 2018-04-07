using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDemo2 : MonoBehaviour {

	void Start(){
		NodeDemo.On ("getData", () => Debug.Log (NodeDemo.instance.secret));
		NodeDemo.Emit ("getData");
	
	}

}
