using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Attach this to a GameObject that has a Button.  Every time the
/// button is clicked, it will call OnButtonPress
/// </summary>
[RequireComponent(typeof(Button))]
public class SceneTransitionButton : MonoBehaviour
{
    public SceneManifest scene;

    /// <summary>
    /// Loads the scene specified in the inspector.
    /// </summar>
    public void LoadScene()
    {
        _sceneChanger.ChangeToScene(scene);
    }


    private Button _sceneTransitionButton;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _sceneTransitionButton = GetComponent<Button>();
        _sceneTransitionButton?.onClick.AddListener(LoadScene);

        GameObject sceneChangerGameObject = GameObject.Find("Scene Changer");
        _sceneChanger = sceneChangerGameObject?.GetComponent<SceneChanger>();
    }
}
