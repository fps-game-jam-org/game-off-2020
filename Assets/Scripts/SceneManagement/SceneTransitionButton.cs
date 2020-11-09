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
    /// Loads the title screen
    /// </summar>
    public void LoadTitle()
    {
        _sceneChanger.LoadTitle();
    }

    /// <summary>
    /// Loads the Credits scene
    /// </summary>
    public void LoadCredits()
    {
        _sceneChanger.LoadCredits();
    }

    /// <summary>
    /// Loads the index-th level in the Scene Changer GameObject's
    /// level list.
    /// </summary>
    public void LoadLevel(int index)
    {
        _sceneChanger.LoadLevel(index);
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
