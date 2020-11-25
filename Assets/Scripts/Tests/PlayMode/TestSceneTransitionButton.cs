using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Tests
{
    public class TestSceneTransitionButton
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene(
                SceneManifestTranslator.Translate(SceneManifest.DummyScene0));
            WaitFrames(10);
            SetUpSceneTransitionButton();
        }

        [TearDown]
        public void TearDown()
        {
            Debug.Log("Destroy");
            Object.Destroy(_sceneChangerCreatorGameObject);
            Object.Destroy(_sceneTransitionButtonObject);
        }

#if UNITY_EDITOR
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Unity wants to generates garbage in MY game >:(
            AssetDatabase.MoveAssetToTrash("Assets/InitTestScene*");
        }
#endif

        [UnityTest]
        public IEnumerator TestLoadsSceneOnButtonPress()
        {
            _sceneTransitionButton.scene = SceneManifest.TestLevel;
            _sceneTransitionButton.LoadScene();
            bool isLoaded = false;
            SceneChanger.LoadFinished += 
                (object sender, System.EventArgs e) => isLoaded = true;
            while (!isLoaded)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name,
                        Is.EqualTo(SceneManifestTranslator
                                   .Translate(SceneManifest.TestLevel)),
                        "Fails to load a test level on button press.");
            yield return null;
        }


        private GameObject _sceneChangerCreatorGameObject;
        private SceneChanger _sceneChanger;
        private GameObject _sceneTransitionButtonObject;
        private SceneTransitionButton _sceneTransitionButton;

        private void SetUpSceneTransitionButton()
        {
            _sceneChangerCreatorGameObject =
                new GameObject(
                    "SceneChangerCreator",
                    new System.Type[] {typeof(SceneChangerCreator)});
            Debug.Log("SetUp");
            _sceneTransitionButtonObject =
                new GameObject(
                    "SceneTransitionButton",
                    new System.Type[] {typeof(SceneTransitionButton)});
            _sceneTransitionButton =
                _sceneTransitionButtonObject
                .GetComponent<SceneTransitionButton>();
            GameObject[] obs = SceneManager.GetActiveScene().GetRootGameObjects();
            Debug.Log(SceneManager.GetActiveScene().name);
            for (int i = 0; i < obs.Length; ++i)
                Debug.Log(obs[i].name);
            Debug.Log("SetUp finished");
        }

        private IEnumerator WaitFrames(int n)
        {
            for (int i = 0; i < n; ++i)
                yield return null;
        }

        // private static void InvokeEvent(object onMe,
        //                                 string invokeMe,
        //                                 params object[] eventParams)
        // {
        //     System.MulticastDelegate eventDelagate =
        //         (System.MulticastDelegate)
        //         onMe.GetType()
        //             .GetField(invokeMe,
        //                       (System.Reflection.BindingFlags.Instance
        //                        | System.Reflection.BindingFlags.Public))
        //             .GetValue(onMe);

        //     System.Delegate[] delegates = eventDelagate.GetInvocationList();

        //     foreach (System.Delegate dlg in delegates)
        //     {
        //        dlg.Method.Invoke(dlg.Target, eventParams);
        //     }
        // } 
    }
}
