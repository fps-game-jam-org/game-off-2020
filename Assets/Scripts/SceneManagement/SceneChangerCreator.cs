using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerCreator : MonoBehaviour
{
    private const string sceneChangerScene =
        SceneManifestTranslator.Translate(SceneManifest.SceneChanger);

    private void Awake()
    {
        if (!SceneChangerIsLoaded())
        {
            SceneManager.LoadSceneAsync(sceneChangerScene,
                                        LoadSceneMode.Additive);
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
}
