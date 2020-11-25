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
    public SceneManifest scene = SceneManifest.DummyScene0;

    public void LoadScene()
    {
        SceneChanger.ChangeToScene(scene);
    }


    private Button _button;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        Debug.Log("Awake");
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadScene);
    }

    private void Start()
    {
        // Debug.Log("Start");
        // GameObject creatorObject = GameObject.Find("SceneChangerCreator");
        // SceneChangerCreator creator =
        //     creatorObject.GetComponent<SceneChangerCreator>();
        // creator.LoadFinished += GetSceneChanger;
    }

    private void GetSceneChanger(object sender, System.EventArgs e)
    {
        // GameObject[] obs = SceneManager.GetActiveScene().GetRootGameObjects();
        // Debug.Log(SceneManager.GetActiveScene().name);
        // for (int i = 0; i < obs.Length; ++i)
        //     Debug.Log(obs[i].name);
        // GameObject sceneChangerObject =
        //     GameObject.FindWithTag("SceneChanger");
        // Debug.Log(gameObject.name);
        // Debug.Log($"scene changer is null: {sceneChangerObject == null}");
        // _sceneChanger = sceneChangerObject.GetComponent<SceneChanger>();
    }
}
