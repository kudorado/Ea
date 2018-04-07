using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EaActivity : EaManagerSingleton<EaActivity>
{
    public List<EaFragment> framents = new List<EaFragment>();
    //public LayerMask loadingMask,loadedMask;
    public Camera renderCamera { get; set; }
    public Canvas canvas { get; set; }
    IEnumerator Start()
    {
        renderCamera = GetComponentInChildren<Camera>();
        canvas = GetComponentInChildren<Canvas>();

        //renderCamera.cullingMask = loadingMask;
        var scenes = SceneManager.sceneCountInBuildSettings;
        //Debug.Log(scenes);
        for (int i = 1; i < scenes; i++)
        {
            var scene = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            while (!scene.isDone)
            {
                yield return null;
            }
            Debug.LogFormat("Scene {0} was loaded.", SceneManager.GetSceneAt(i).name);
        }
        int sc = 0;
        framents.ForEach(fr =>
        {
            sc++;
            fr.loadedScene = SceneManager.GetSceneAt(sc).name;
            SceneManager.UnloadSceneAsync(sc);

        });
        OpenScene("Game");
    }

    private void GetScenes(string scene,System.Action<EaFragment> callback){
        int sceneCount = framents.Count;
        for (int i = 0; i < sceneCount;i++){
    
            if(framents[i].loadedScene.ToLower() == scene.ToLower()){
                callback(framents[i]);
                return;
            } 

        }  
    } 
    public void OpenScene(string scene){
        GetScenes(scene, fr =>
        {

            fr.Show();
        });
    }
    public void CloseScene(string scene)
    {
        GetScenes(scene, fr =>
        {
            fr.Hide();
        });
    }

}
