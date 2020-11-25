using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static event System.EventHandler LoadFinished;

    public static void ChangeToScene(SceneManifest scene)
    {
        SceneManager.LoadScene(
            SceneManifestTranslator.Translate(scene));
        FlagLoading();
    }


    private static void OnLoadFinished(Scene scene, LoadSceneMode mode)
    {
        System.EventHandler loadFinished = LoadFinished;
        if (loadFinished != null)
        {
            loadFinished(typeof(SceneChanger), System.EventArgs.Empty);
        }
    }


    private static void FlagLoading() 
    {
        SceneManager.sceneLoaded += OnLoadFinished;
    }
}
