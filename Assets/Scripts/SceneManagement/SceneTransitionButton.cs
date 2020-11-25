using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Attach this to a GameObject that has a Button.  When a
/// button is pressed, it will transition to scene.
/// </summary>
[RequireComponent(typeof(Button))]
public class SceneTransitionButton : MonoBehaviour
{
    [Tooltip("The scene the game transitions to when button is pressed.")]
    public SceneManifest scene = SceneManifest.DummyScene0;

    /// <summary>
    /// When the button is pressed, this function gets called and the
    /// game is transitioned to the scene specified by scene.  This
    /// function is exposed primarily for testing purposes, so don't
    /// call is yourself.
    /// </summary>
    public void LoadScene()
    {
        SceneChanger.ChangeToScene(scene);
    }


    private Button _button;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadScene);
    }
}
