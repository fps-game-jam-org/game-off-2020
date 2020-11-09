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
        private GameObject _sceneChangerGameObject;
        private SceneChanger _sceneChanger;

        private string _titleScene = "Title";
        private string _levelScene = "testLevel";
        private string _creditsScene = "Credits";

        [SetUp]
        public void SetUp()
        {
            _sceneChangerGameObject =
                new GameObject("Scene Changer");
            _sceneChangerGameObject.AddComponent<SceneChanger>();
            _sceneChanger =
                _sceneChangerGameObject.GetComponent<SceneChanger>();
            _sceneChanger.AddTitleScene(_titleScene);
            _sceneChanger.AddLevelScenes(
                new List<string>(new string[] {_levelScene}));
            _sceneChanger.AddCreditsScene(_creditsScene);

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
        public IEnumerator TestInitiallyLoadsToTitle()
        {
            Assert.That(_sceneChanger.LoadFirstScene(), Is.True);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("Title"),
                        "Fails to initially load to title.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestLoadsTitle()
        {
            Assert.That(_sceneChanger.LoadTitle(), Is.True);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("Title"),
                        "Fails to load title screen.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestLoadsLevel()
        {
            Assert.That(_sceneChanger.LoadLevel(0), Is.True);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("testLevel"),
                        "Fails to load the level.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestLoadsCredits()
        {
            Assert.That(_sceneChanger.LoadCredits(), Is.True);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("Credits"),
                        "Fails to load the credits.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTitleDoesntLoadWhenNotSet()
        {
            _sceneChanger.AddTitleScene(null);
            Assert.That(_sceneChanger.LoadTitle(), Is.False);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("DummyScene"),
                        "Fails to do nothing when there isn't a title "
                        + "scene set");
        }

        [UnityTest]
        public IEnumerator TestCreditsDontLoadWhenNotSet()
        {
            _sceneChanger.AddCreditsScene(null);
            Assert.That(_sceneChanger.LoadCredits(), Is.False);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("DummyScene"),
                        "Fails to do nothing when there isn't a "
                        + "credits scene set");
        }

        [UnityTest]
        public IEnumerator TestLevelDoesntLoadWhenNotSet()
        {
            _sceneChanger.AddLevelScenes(null);
            Assert.That(_sceneChanger.LoadLevel(0), Is.False);
            while (_sceneChanger.IsLoading)
            {
                yield return null;
            }
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, Is.EqualTo("DummyScene"),
                        "Fails to do nothing when there isn't a level "
                        + "scene set");
        }
    }
}
