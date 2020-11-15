using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Attach this to a GameObject that has a Button.  When a
/// button is pressed, it will transition to scene.
/// </summary>
[RequireComponent(typeof(Button))]
public class SceneTransitionButton : MonoBehaviour
{
    public SceneManifest scene = SceneManifest.DummyLevel;

    public void LoadScene()
    {
        _sceneChanger.ChangeToScene(scene);
    }


    private Button _button;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadScene);
        GameObject sceneChangerGameObject = GameObject.Find("Scene Changer");
        _sceneChanger = sceneChangerGameObject.GetComponent<SceneChanger>();
    }
}
