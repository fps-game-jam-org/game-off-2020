using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionButton : MonoBehaviour
{
    public virtual void OnButtonPress() {}

    public void LoadTitle()
    {
        _sceneChanger.LoadTitle();
    }

    public void LoadCredits()
    {
        _sceneChanger.LoadCredits();
    }

    public void LoadLevel(int index)
    {
        _sceneChanger.LoadLevel(index);
    }


    private Button _sceneTransitionButton;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _sceneTransitionButton = GetComponent<Button>();
        _sceneTransitionButton.onClick.AddListener(OnButtonPress);

        GameObject sceneChangerGameObject = GameObject.Find("Scene Changer");
        _sceneChanger = sceneChangerGameObject?.GetComponent<SceneChanger>();
    }
}
