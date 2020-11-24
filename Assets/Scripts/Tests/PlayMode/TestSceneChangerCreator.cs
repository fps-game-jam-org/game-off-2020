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
    public class TestSceneChangerCreator
    {
        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene(
                SceneManifestTranslator.Translate(SceneManifest.DummyScene0));
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
        public IEnumerator TestLoadsSceneChanger()
        {
            GenerateCreator();
            for (int i = 0; i < 5; ++i)
                yield return null;  // wait for stuff to load

            List<string> sceneNames = new List<string>();
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                sceneNames.Add(SceneManager.GetSceneAt(i).name);
            }
            Assert.That(sceneNames,
                        Has.Member(SceneManifestTranslator
                                   .Translate(SceneManifest.SceneChanger)),
                        "Fails to load SceneChanger scene on startup.");
        }

        [UnityTest]
        public IEnumerator TestLoadedSceneHasSceneChangerObject()
        {
            GenerateCreator();
            for (int i = 0; i < 5; ++i)
                yield return null;  // wait for stuff to load
            GameObject sceneChangerObject =
                GameObject.FindWithTag("Scene Changer");
            Assert.That(sceneChangerObject, Is.Not.Null,
                        "SceneChanger GameObject cannot be found.");
        }

        [UnityTest]
        public IEnumerator TestLoadedSceneHasSceneChangerComponent()
        {
            GenerateCreator();
            for (int i = 0; i < 5; ++i)
                yield return null;  // wait for stuff to load
            GameObject sceneChangerObject =
                GameObject.FindWithTag("Scene Changer");
            SceneChanger sceneChanger =
                sceneChangerObject.GetComponent<SceneChanger>();
            Assert.That(sceneChanger, Is.Not.Null,
                        "SceneChanger GameObject does not have a "
                        + "SceneChanger component.");
        }

        [UnityTest]
        public IEnumerator TestLoadingEvent()
        {
            GenerateCreator();
            bool eventHasOccured = false;
            _creator.LoadFinished +=
                (object sender, System.EventArgs e) => eventHasOccured = true;
            for (int i = 0; i < 5; ++i)
                yield return null;
            Assert.That(eventHasOccured, Is.True,
                        "Fails to notify that the scene creator is "
                        + "loading.");
            yield return null;
        }


        GameObject _creatorObject;
        SceneChangerCreator _creator;

        private void GenerateCreator()
        {
            _creatorObject =
                new GameObject(
                    "Scene Changer Creator",
                    new System.Type[] {typeof(SceneChangerCreator)});
            _creator = _creatorObject.GetComponent<SceneChangerCreator>();
        }

        private IEnumerator TearDownScenes()
        {
            Object.Destroy(_creator);
            bool scenesAreUnloaded = false;
            SceneManager.sceneUnloaded += (Scene _) => scenesAreUnloaded = true;
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                SceneManager.UnloadSceneAsync(
                    SceneManager.GetSceneAt(i).name);
            }
            while (!scenesAreUnloaded)
                yield return null;
        }
    }
}
