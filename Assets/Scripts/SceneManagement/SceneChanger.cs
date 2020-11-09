using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool LoadFirstScene()
    {
        SceneManager.LoadScene(0);
        WaitForLoad();
        return true;
    }

    public bool LoadTitle()
    {
        return LoadScene(titleSceneName);
    }

    public bool LoadLevel(int levelNumber)
    {
        if (levelSceneNames != null)
        {
            return LoadScene(levelSceneNames[levelNumber]);
        }
        else
        {
            return false;    
        }
    }

    public bool LoadCredits()
    {
        return LoadScene(creditsSceneName);
    }

    public void AddTitleScene(string titleSceneName)
    {
        titleSceneName = titleSceneName;
    }

    public void AddLevelScenes(List<string> levelSceneNames)
    {
        if (levelSceneNames != null)
        {
            levelSceneNames = new List<string>(levelSceneNames);
        }
        else
        {
            levelSceneNames = null;    
        }
    }

    public void AddCreditsScene(string creditsSceneName)
    {
        creditsSceneName = creditsSceneName;
    }

    public bool IsLoading {
        get { return _isLoading; }
    }


    public string titleSceneName = null;
    public List<string> levelSceneNames = null;
    public string creditsSceneName = null;
    private bool _isLoading = false;

    private void WaitForLoad() 
    {
        _isLoading = true;
        SceneManager.sceneLoaded += SceneLoadDone;
    }

    private void SceneLoadDone(Scene scene, LoadSceneMode mode)
    {
        _isLoading = false;
    }

    private bool LoadScene(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
            WaitForLoad();
            Debug.Log(levelSceneNames[0]);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
