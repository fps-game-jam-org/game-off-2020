using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToScene(SceneManifest scene)
    {
        SceneManager.LoadScene(
            SceneManifestTranslator.Translate(scene));
        FlagLoading();
    }

    public bool IsLoading {
        get { return _isLoading; }
    }


    private bool _isLoading = false;

    private void FlagLoading() 
    {
        _isLoading = true;
        SceneManager.sceneLoaded += SceneLoadDone;
    }

    private void SceneLoadDone(Scene scene, LoadSceneMode mode)
    {
        _isLoading = false;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
