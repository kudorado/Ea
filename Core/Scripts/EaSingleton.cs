using UnityEngine;
using System.Collections;

public class EaSingleton<T> : MonoBehaviour {

	public static T instance;
	protected virtual void Awake(){
		instance = GetComponent<T> ();	
		//Debug.Log ("singleton call");

	}

}
public class EaManagerSingleton<T> : MonoBehaviour where T : MonoBehaviour{
    public static T instance;
    protected virtual void Awake()
	{
        if (instance == null)
        {
            instance = gameObject.GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}
}
public class EaMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour{
	public static T _instance;
	public static T instance{
		get
		{
			return _instance ?? (_instance =  new GameObject (typeof(T).Name).AddComponent<T> ());
		}
	}
	public static void Initialize(){
		if (_instance == null) 
			Debug.Log (string.Format ("{0} Initialized!", instance.GetType ().Name).color (Color.red));
	
		
	}
} 

//thean1995	 thean1994
public class EditorSingleton<T> : ScriptableObject  where T : ScriptableObject{
	public static  string resourceName { get { return typeof(T).Name;}}
	private static T _instance;
	public static T instance{ get { return _instance ?? (_instance = Resources.Load<T> (resourceName)); } }


}