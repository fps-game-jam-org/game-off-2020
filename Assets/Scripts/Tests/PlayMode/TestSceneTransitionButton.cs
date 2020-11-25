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
            //WaitFrames(10);
            SetUpSceneTransitionButton();
        }

        [TearDown]
        public void TearDown()
        {
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
            LoadingStatus status = new LoadingStatus();
            SceneChanger.LoadFinished += status.MakeIsLoadedTrue;
            while (!status.isLoaded)
                yield return null;
            SceneChanger.LoadFinished -= status.MakeIsLoadedTrue;
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name,
                        Is.EqualTo(SceneManifestTranslator
                                   .Translate(SceneManifest.TestLevel)),
                        "Fails to load a test level on button press.");
            yield return null;
        }


        private GameObject _sceneTransitionButtonObject;
        private SceneTransitionButton _sceneTransitionButton;

        private void SetUpSceneTransitionButton()
        {
            _sceneTransitionButtonObject =
                new GameObject(
                    "SceneTransitionButton",
                    new System.Type[] {typeof(SceneTransitionButton)});
            _sceneTransitionButton =
                _sceneTransitionButtonObject
                .GetComponent<SceneTransitionButton>();
        }

        private IEnumerator WaitFrames(int n)
        {
            for (int i = 0; i < n; ++i)
                yield return null;
        }

        private class LoadingStatus
        {
            public bool isLoaded;

            public LoadingStatus()
            {
                isLoaded = false;
            }

            public void MakeIsLoadedTrue(object sender, System.EventArgs e)
            {
                isLoaded = true;
            }
        }
    }
}
