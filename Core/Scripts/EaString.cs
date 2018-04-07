using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif
[System.Serializable]
public class EditableString{
	public string key,value;
	public EditableString(string key ="",string value = ""){
		this.key = key;
		this.value = value;
	}
}
public class EaString : EditorSingleton<EaString> {
    public const string primaryKey = "☰ Setting";
	#if UNITY_EDITOR
	[MenuItem("Ea/Global/Text")]
	public static void CreateText(){
		EaScriptable.CreateAsset<EaString> ("Assets/Ea/Global/Resources/");

	}
#endif
    private List<EditableString> _globalString;
    public List<EditableString> globalString { 
        get { 
            return _globalString ?? (_globalString = new List<EditableString>());
        }
    }
    public string [] allKeys{
        get{
            return globalString.Select(s=> s.key.ToLower()).ToArray();
        }
    }
	public string GetValue(string key){
        int leng = instance.globalString.Count;
		for (int i = 0; i < leng; i++) {
            if (instance.globalString [i].key.ToLower() == key.ToLower())
                return instance.globalString [i].value;
		}
		EditableString newES = new EditableString ();
		newES.key = key;
        instance.globalString.Add (newES);
		return newES.value;
	}
	public bool ContainsKey(string key){
        int leng = instance.globalString.Count;
        for (int i = 0; i < leng; i++){
            if (instance.globalString[i].key.ToLower() == key.ToLower() || key.ToLower() == primaryKey.ToLower())
                return true;
            //Debug.Log(instance.globalString[i].key);
        }
        

		return false;
	}
  
  

}
