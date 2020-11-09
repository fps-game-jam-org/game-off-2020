using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionButton : MonoBehaviour
{
    public virtual void SendForSceneLoad() {}

    public SceneChanger SceneChangerObject {
        get { return _sceneChangerObject; }
    }


    private Button _sceneTransitionButton;
    private SceneChanger _sceneChangerObject;

    private void Awake()
    {
        _sceneTransitionButton = GetComponent<Button>();
        _sceneTransitionButton.onClick.AddListener(SendForSceneLoad);

        GameObject sceneChangerGameObject = GameObject.Find("Scene Changer");
        _sceneChangerObject =
            sceneChangerGameObject?.GetComponent<SceneChanger>();
    }
}
