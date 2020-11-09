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
    /// <summary>
    /// This is called every time the attached Button is clicked.
    /// override it to define where the destination ought to be and any
    /// extra behaviour.
    /// </summary>
    public virtual void OnButtonPress() {}

    /// <summary>
    /// Loads the title screen.  Returns true if the scene changed.
    /// </summar>
    public bool LoadTitle()
    {
        return _sceneChanger.LoadTitle();
    }

    /// <summary>
    /// Loads the Credits scene.  Returns true if the scene changed.
    /// </summary>
    public bool LoadCredits()
    {
        return _sceneChanger.LoadCredits();
    }

    /// <summary>
    /// Loads the index-th level in the Scene Changer GameObject's
    /// level list.  Returns true if the scene changed.
    /// </summary>
    public bool LoadLevel(int index)
    {
        return _sceneChanger.LoadLevel(index);
    }


    private Button _sceneTransitionButton;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _sceneTransitionButton = GetComponent<Button>();
        _sceneTransitionButton?.onClick.AddListener(OnButtonPress);

        GameObject sceneChangerGameObject = GameObject.Find("Scene Changer");
        _sceneChanger = sceneChangerGameObject?.GetComponent<SceneChanger>();
    }
}
