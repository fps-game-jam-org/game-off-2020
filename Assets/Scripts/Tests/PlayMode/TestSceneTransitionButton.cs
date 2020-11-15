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
            SetUpSceneTransitionButton();

            SceneManager.LoadScene("DummyScene");
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_sceneChangerGameObject);
            Object.Destroy(_sceneTransitionButton);
            Object.Destroy(_sceneButton);
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
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("testLevel"),
                        "Fails to load a test level on button press.");
            yield return null;
        }


        private GameObject _sceneChangerGameObject;
        private SceneChanger _sceneChanger;
        private SceneTransitionButton _sceneTransitionButton;
        private Button _sceneButton;

        private void SetUpSceneTransitionButton()
        {
            _sceneChangerGameObject =
                new GameObject("Scene Changer");
            _sceneChangerGameObject.AddComponent<SceneChanger>();
            _sceneChanger =
                _sceneChangerGameObject.GetComponent<SceneChanger>();

            GameObject sceneTransitionButtonGameObject =
                new GameObject("Scene Transition Button");
            sceneTransitionButtonGameObject
                .AddComponent<SceneTransitionButton>();
            _sceneTransitionButton =
                sceneTransitionButtonGameObject
                .GetComponent<SceneTransitionButton>();
            _sceneButton =
                sceneTransitionButtonGameObject.GetComponent<Button>();
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
