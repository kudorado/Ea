using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour {

    public void OpenScene(string scene){
        EaActivity.instance.OpenScene(scene); 
    }
    public void CloseScene(string scene){
        EaActivity.instance.CloseScene(scene);
    }
}