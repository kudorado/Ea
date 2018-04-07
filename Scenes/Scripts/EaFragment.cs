using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public delegate void EaFragmentEvent();
public class EaFragment : MonoBehaviour {
    public event EaFragmentEvent onFragmentShow = delegate {
        Debug.Log("Show Fragment");
    };
    public event EaFragmentEvent onFragmentHide = delegate {
        Debug.Log("Hide Fragment");
                
    };

    public string loadedScene { get; set; }
    void Start(){
        DontDestroyOnLoad(gameObject);
        EaActivity.instance.framents.Add(this);
        transform.parent = EaActivity.instance.canvas.transform;
        gameObject.SetActive(false);
    }
    public void Show(){
        gameObject.SetActive(true);
        onFragmentShow();
     

    }
    public void Hide(){
        gameObject.SetActive(false);
        onFragmentHide();
    }
   
}
