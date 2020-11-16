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
            SetUpSceneChanger();

            SceneManager.LoadScene("DummyScene");
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_sceneChangerGameObject);
            Object.Destroy(_sceneChanger);
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
        public IEnumerator TestIdleIsLoadingIsFalse()
        {
            Assert.That(_sceneChanger.IsLoading, Is.False);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestLoadingIsLoadingIsTrue()
        {
            _sceneChanger.ChangeToScene(SceneManifest.TestLevel);
            Assert.That(_sceneChanger.IsLoading, Is.True);
            yield return null;
        }


        [UnityTest]
        public IEnumerator TestLoadsTestLevel()
        {
            _sceneChanger.ChangeToScene(SceneManifest.TestLevel);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("testLevel"),
                        "Fails to load a test level.");
            yield return null;
        }


        private GameObject _sceneChangerGameObject;
        private SceneChanger _sceneChanger;

        private void SetUpSceneChanger()
        {
            _sceneChangerGameObject =
                new GameObject("Scene Changer");
            _sceneChangerGameObject.AddComponent<SceneChanger>();
            _sceneChanger =
                _sceneChangerGameObject.GetComponent<SceneChanger>();
        }
    }
}
