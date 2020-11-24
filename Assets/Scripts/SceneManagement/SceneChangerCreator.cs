using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangerCreator : MonoBehaviour
{
    public event System.EventHandler LoadFinished;


    protected virtual void OnLoadFinished(Scene scene, LoadSceneMode mode)
    {
        System.EventHandler loadFinished = LoadFinished;
        if (loadFinished != null)
        {
            loadFinished(this, null);
        }
    }


    private bool _isLoading = true;

    private string sceneChangerScene =
        SceneManifestTranslator.Translate(SceneManifest.SceneChanger);

    private void Awake()
    {
        if (!SceneChangerIsLoaded())
        {
            SceneManager.LoadSceneAsync(sceneChangerScene,
                                        LoadSceneMode.Additive);
            SceneManager.sceneLoaded += OnLoadFinished;
        }
    }

    private bool SceneChangerIsLoaded()
    {
        bool isLoaded = false;
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            isLoaded =
                isLoaded
                || (SceneManager.GetSceneAt(i).name == sceneChangerScene);
        }

        return isLoaded;
    }

    private void SceneLoadDone(Scene scene, LoadSceneMode mode)
    {
        _isLoading = false;
    }
}
