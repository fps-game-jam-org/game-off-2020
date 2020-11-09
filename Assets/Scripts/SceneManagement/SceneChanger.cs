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
        return LoadScene(_titleSceneName);
    }

    public bool LoadLevel(int levelNumber)
    {
        if (_levelSceneNames != null)
        {
            return LoadScene(_levelSceneNames[levelNumber]);
        }
        else
        {
            return false;    
        }
    }

    public bool LoadCredits()
    {
        return LoadScene(_creditsSceneName);
    }

    public void AddTitleScene(string titleSceneName)
    {
        _titleSceneName = titleSceneName;
    }

    public void AddLevelScenes(List<string> levelSceneNames)
    {
        if (levelSceneNames != null)
        {
            _levelSceneNames = new List<string>(levelSceneNames);
        }
        else
        {
            _levelSceneNames = null;    
        }
    }

    public void AddCreditsScene(string creditsSceneName)
    {
        _creditsSceneName = creditsSceneName;
    }

    public bool IsLoading {
        get { return _isLoading; }
    }


    public string _titleSceneName = null;
    public List<string> _levelSceneNames = null;
    public string _creditsSceneName = null;
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
