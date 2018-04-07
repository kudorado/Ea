using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text.RegularExpressions;
public static class EaGUI
{
    static public void Horizontal(System.Action content)
    {
        EditorGUILayout.BeginHorizontal();
        content.InvokeSafe();
        EditorGUILayout.EndHorizontal();
    }

}
[CustomEditor(typeof(EaString))]
public class EaStringEditor : Editor
{
    public GUIStyle style = new GUIStyle();
    private void OnEnable()
    {
        style.normal.textColor = Color.white;
    }
    private string key, value;

    public override void OnInspectorGUI()
    {
        EaGUI.Horizontal(() =>
        {
            EditorGUILayout.LabelField("Key", GUILayout.MaxWidth(100));
            key = EditorGUILayout.TextField(key);
        });
        EaGUI.Horizontal(() =>
        {
            EditorGUILayout.LabelField("Value", GUILayout.MaxWidth(100));
            value = EditorGUILayout.TextField(value);

        });
        bool disabled = key == null || key == string.Empty ? true : EaString.instance.ContainsKey(key);
        EditorGUI.BeginDisabledGroup(disabled);
        if (GUILayout.Button("Add"))
        {
            EaString.instance.globalString.Add(new EditableString(key, value));
        }
        EditorGUI.EndDisabledGroup();


        //draw list editor
        int leng = EaString.instance.globalString.Count;
        //Debug.Log("fa");
        bool breakLoop = false;
        for (int i = 0; i < leng; i++)
        {
            if (breakLoop)
                break;
            EaGUI.Horizontal(() =>
            {
                string convertedKey = EaString.instance.globalString[i].key.ToLower();
                EditorGUILayout.LabelField(convertedKey, GUILayout.MaxWidth(100));
                GUI.backgroundColor = Color.cyan;
                EaString.instance.globalString[i].value = EditorGUILayout.TextField(EaString.instance.globalString[i].value);

                if (GUILayout.Button("x", style, GUILayout.MaxHeight(15), GUILayout.MaxWidth(18)))
                {
                    EaString.instance.globalString.Remove(EaString.instance.globalString[i]);
                    breakLoop = true;//redraw
                }
            });
        }
    }

}
[CustomEditor(typeof(EaGlobalText))]
public class EaGlobalTextEditor : Editor
{
    int current;
    List<string> keys;
    EaGlobalText instance;
    private void OnEnable()
    {
        instance = target as EaGlobalText;
    }
    public override void OnInspectorGUI()
    {

        keys = EaString.instance.allKeys.ToList();
        keys.Add(EaString.primaryKey);

        switch(keys.Count){
            case 1:
                keys.Clear();
                keys.AddRange(new string[] { "No ☭ found", "⚒ Create"});
                current = EditorGUILayout.Popup(current, keys.ToArray());
                if (current == 1)
                    EaString.CreateText();

                break;

            default:
                EaGUI.Horizontal(() =>
                {
                    EditorGUILayout.LabelField("Key", GUILayout.MaxWidth(100));
                    current = EditorGUILayout.Popup(current, keys.ToArray());
                });
                break;
        }

    
        if (current == EaString.instance.allKeys.Length) {
            current = 0;
            EaString.CreateText();
            return;
        }
            string key = EaString.instance.allKeys[current];
            instance.textKey = key;
            instance.text.text = EaString.instance.GetValue(key);
            EaGUI.Horizontal(() =>
            {
                EditorGUILayout.LabelField("Value", GUILayout.MaxWidth(100));
                EaString.instance.globalString[current].value = EditorGUILayout.TextField(EaString.instance.GetValue(key));
            });



    }
}