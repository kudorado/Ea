using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EaUnityAds))]
public class EaUnityAdsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        #if UNITY_ADS
        base.OnInspectorGUI();
        #else
        EditorGUILayout.HelpBox("Error loading ads, services ads extension not found!", MessageType.Error);
        #endif
    }
}
