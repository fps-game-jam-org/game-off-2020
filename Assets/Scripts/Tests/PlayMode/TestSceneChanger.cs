using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Tests
{
    public class TestSceneChanger
    {
        [SetUp]
        public void SetUp()
        {
            SetUpScene();
        }

        [TearDown]
        public void TearDown()
        {
            TearDownScene();
        }

#if UNITY_EDITOR
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Unity wants to generates garbage in MY game >:(
            AssetDatabase.MoveAssetToTrash("Assets/InitTestScene*");
        }
#endif

        // [UnityTest]
        // public IEnumerator TestWhileIdleIsLoadingIsFalse()
        // {
        //     Assert.That(_sceneChanger.IsLoading, Is.False,
        //                 "Fails to negatively identify of its loading.");
        //     yield return null;
        // }

        // [UnityTest]
        // public IEnumerator TestWhileLoadingIsLoadingIsTrue()
        // {
        //     _sceneChanger.ChangeToScene(SceneManifest.DummyScene1);
        //     Assert.That(_sceneChanger.IsLoading, Is.True,
        //                 "Fails to positively identify of it's loading.");
        //     yield return null;
        // }


        [UnityTest]
        public IEnumerator TestLoadsTestLevel()
        {
            SceneChanger.ChangeToScene(SceneManifest.DummyScene1);
            bool eventHasOccured = false;
            SceneChanger.LoadFinished +=
                (object sender, System.EventArgs e) => eventHasOccured = true;
            while (!eventHasOccured)
                yield return null;
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, 
                        Is.EqualTo(SceneManifestTranslator
                                   .Translate(SceneManifest.DummyScene1)),
                        "Fails to load a test level.");
            yield return null;
        }


        // private GameObject _sceneChangerObject;
        // private SceneChanger _sceneChanger;

        private void SetUpScene()
        {
            // SceneManager.LoadScene(
            //     SceneManifestTranslator.Translate(SceneManifest.DummyScene0));
            // _sceneChangerObject =
            //     new GameObject("Scene Changer",
            //                    new System.Type[] {typeof(SceneChanger)});
            // _sceneChanger =
            //     _sceneChangerObject.GetComponent<SceneChanger>();
        }

        private void TearDownScene()
        {
            // Object.Destroy(_sceneChanger);
            // Object.Destroy(_sceneChangerObject);
        }
    }
}
