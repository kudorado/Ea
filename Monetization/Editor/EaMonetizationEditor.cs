using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
[CustomEditor(typeof(EaMonetization))]
public class EaMonetizationEditor : Editor
{
    int selection;
    bool isAdmobInstalled {get{return  Directory.Exists(getPath("GoogleMobileAds"));}}
    bool isUnityInstalled { get {return Directory.Exists(getPath("Ea/Monetization/Installed/UnityAds")); } }


    public readonly string [] toolbar = { "Settings", "AdMob", "UnityAds" };
    public override void OnInspectorGUI()
    {

        selection = GUILayout.Toolbar(selection, toolbar);
        switch(selection){
            case 0:
                base.OnInspectorGUI();
                break;
            case 1:
                DrawAdMob();
                break;
            case 2:
                DrawUnityAds();
                break;
        }
    }



    void DrawUnityAds(){
        DrawChoosen(isUnityInstalled,ref unityInstallPath);
        DrawInstallButton(isUnityInstalled,installUnityText, uninstallUnityText, unityInstallPath, UninstallUnityAds);
    }

    void DrawAdMob(){
        DrawChoosen(isAdmobInstalled, ref admobInstallPath);
        DrawInstallButton(isAdmobInstalled,installAdMobText, uninstallAdMobText,admobInstallPath,UninstallAdmob);

      
    }
    void DrawInstallButton(bool installed,string installText,string uninstallText,string installPath,System.Action Uninstall){
        var buttonText = installed ? uninstallText : installText;
        if (GUILayout.Button(buttonText))
        {
            switch (installed)
            {
                case false:
                    AssetDatabase.ImportPackage(installPath, false);
                    break;
                case true:
                    Uninstall();
                    break;


            }

        }

    }
    void DrawChoosen(bool installed,ref string installPath)
    {
        switch (installed)
        {
            case true:
                EditorGUILayout.HelpBox(string.Format("{0} was installed.",installPath) ,MessageType.Info);

                break;
            case false:
                string tempInstallPath = installPath;
                EaGUI.Horizontal(() =>
                {
                    EditorGUILayout.LabelField("Install File", EditorStyles.helpBox, GUILayout.MinWidth(50));
                    EditorGUILayout.LabelField(tempInstallPath, EditorStyles.helpBox, GUILayout.MinWidth(150));


                });
                if (GUILayout.Button("Change Install File"))
                {
                    var selectedPath = EditorUtility.OpenFilePanel("Select packages", installPath, "unitypackage");
                    if(!string.IsNullOrEmpty(selectedPath))
                        installPath = selectedPath;

                    
                }
                break;
        }

    }

    const string installAdMobText = "Install AdMob", uninstallAdMobText = "Uninstall AdMob",installUnityText = "Install UnityAds", uninstallUnityText = "Uninstall UnityAds";
    public static string[] adMobFolder = {"PlayServicesResolver","Ea/Monetization/Installed/AdMob","GoogleMobileAds", "Plugins/iOS/AdMobIOS", "Plugins/Android/GoogleMobileAdsPlugin", "Plugins/Android/libs" };
    public static string [] unityAdsFolder = {"Ea/Monetization/Installed/UnityAds" };
    public readonly string googlePlayResolver = "PlayServicesResolver";
    public readonly string googlePlayGames = "GooglePlayGames";
    public const string defaultInstallPath = "Assets/Ea/Monetization/Packages/";
    public const string defaultAdMobPackage = "EaAdMob.unitypackage",defaultUnityPackage = "EaUnityAds.unitypackage";

    public static string admobInstallPath = defaultInstallPath + defaultAdMobPackage;
    public static string unityInstallPath = defaultInstallPath + defaultUnityPackage;
    public static string getPath(string n)
    {
        return Application.dataPath + "/" + n;
    }

    void Uninstall(string [] assetPaths,bool isDone = false){
        float progress = 0;
        for (int i = 0; i < assetPaths.Length; i++)
        {
            //Debug.Log(assetPaths[i]);
            if (!Directory.Exists(getPath(assetPaths[i])))
                continue;

            FileUtil.DeleteFileOrDirectory(getPath(assetPaths[i]));
            progress += 1f / (float)assetPaths.Length;
            EditorUtility.DisplayProgressBar("Assets Deleting","Deleting " + assetPaths[i], progress);

        }
        if (isDone)
        {
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
    }
    void UninstallUnityAds(){
        Uninstall(unityAdsFolder,true);
    }
    void UninstallAdmob()
    {

        float progress = 0;
        Uninstall(adMobFolder);

        if (Directory.Exists(getPath(googlePlayResolver))){
            if (Directory.Exists(getPath(googlePlayGames)))
                return;
            FileUtil.DeleteFileOrDirectory(getPath(googlePlayResolver));
        }

        string plugin = "Plugins";

        string[] plugins = Directory.Exists(getPath(plugin)) ?  Directory.GetDirectories(getPath(plugin)) : null;
        if (plugins == null)
            goto FINISH;

        for (int i = 0; i < plugins.Length; i++)
        {
            string trunc = plugins[i].Replace(getPath("Plugins/"), "");
            string match = trunc.ToLower() == "android" ? "/Android" : trunc.ToLower() == "ios" ? "/iOS" : string.Empty;
            if (match != string.Empty)
            {
                //check if it have no file or directory
                string currentPath = getPath(plugin + match);
                ClearAndroidLibrary(plugins[i], match, ref progress);

                var subFiles = Directory.GetFiles(currentPath);
                var subFolders = Directory.GetDirectories(currentPath);

                if (subFolders.Length == 0)
                {
                    bool isExistAnyFiles = false;
                    if (subFiles.Length > 0)
                    {
                        foreach (var f in subFiles)
                        {
                            if (!f.Contains(".meta"))
                            {
                                isExistAnyFiles = true;
                                break;
                            }
                        }
                    }
                    if (!isExistAnyFiles)
                    {
                        EditorUtility.DisplayProgressBar("Deleting Modules", "Deleting " + trunc, 1);
                        FileUtil.DeleteFileOrDirectory(plugins[i]);
                    }

                }
            }
        }

        plugins = Directory.GetDirectories(getPath(plugin));
        if (plugins.Length == 0)
        {
            EditorUtility.DisplayProgressBar("Deleting Plugins", "Finish cleaning, updating assets", 1);
            FileUtil.DeleteFileOrDirectory(getPath(plugin));

        }
    

        FINISH:

        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();

    }
    void ClearAndroidLibrary(string path, string match,ref float progress)
    {
        string[] pattern = { ".aar", ".jar" };
        List<string> toRemoves = new List<string>();
        if (match == "/Android")
        {
            var subFiles = Directory.GetFiles(path);
            int length = subFiles.Length;

            for (int i = 0; i < length; i++)
            {
                for (int p = 0; p < pattern.Length; p++)
                {
                    if (subFiles[i].Contains(pattern[p]))
                    {
                        toRemoves.Add(subFiles[i]);
                    }

                }
            }
            length = toRemoves.Count;
            for (int rm = 0; rm < length; rm ++){
                FileUtil.DeleteFileOrDirectory(toRemoves[rm]);
                progress += (0.8f / (float)length);
                EditorUtility.DisplayProgressBar("Deleting Library", "Cleaning " + toRemoves[rm], progress);
            }
            

        }
    }

}