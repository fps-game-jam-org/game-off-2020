using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>Use this to handle scene changing.</summary>
public class SceneChanger
{
    /// <summary>
    /// This event is invoked when a scene has finished loading.
    /// You can subscribe functions with this handle to it.
    ///     f(object sender, System.EventArgs e)
    /// </summary>
    public static event System.EventHandler LoadFinished;

    /// <summary>
    /// Changes to the scene specified.  This also unloads the current
    /// scene.
    /// </summary>
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
