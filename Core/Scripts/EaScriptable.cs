using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

public static class EaScriptable
{

    public static void CreateAsset<T>(string dir, string name = null, bool multipler = false, int offset = 0) where T : ScriptableObject
    {
        string mult = multipler ? (Resources.FindObjectsOfTypeAll<T>().Length + offset).ToString() : "";
        string assetPathAndName = dir + (name == null ? typeof(T).Name : name) + mult + ".asset";
        if (!File.Exists(assetPathAndName))
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            T asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = asset;
        }
        else
        {
            //				UnityEngine.Debug.Log ("File exists");
            Selection.activeObject = Resources.Load<T>(typeof(T).Name);
        }

        //EditorUtility.FocusProjectWindow ();

    }
    [MenuItem("Ea/Documents", false, -1)]
    public static void OpenDocument()
    {
        Application.OpenURL("https://eaunity.wordpress.com/docs");
    }
    public static void CreateInitiator<T>() where T : Component
    {
        var ins = GameObject.FindObjectOfType<T>();
        if (ins == null)
           ins =  new GameObject(typeof(T).Name).AddComponent<T>();
        
        Selection.activeObject = ins;

    }

}
#endif