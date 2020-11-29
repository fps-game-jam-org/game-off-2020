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

#if UNITY_EDITOR
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Unity wants to generates garbage in MY game >:(
            AssetDatabase.MoveAssetToTrash("Assets/InitTestScene*");
        }
#endif

        [UnityTest]
        public IEnumerator TestLoadsTestLevel()
        {
            SceneChanger.ChangeToScene(SceneManifest.DummyScene1);
            LoadingStatus status = new LoadingStatus();
            SceneChanger.LoadFinished += status.MakeIsLoadedTrue;
            while (!status.isLoaded)
                yield return null;
            SceneChanger.LoadFinished -= status.MakeIsLoadedTrue;
            Scene currentScene = SceneManager.GetActiveScene();
            Assert.That(currentScene.name, 
                        Is.EqualTo(SceneManifestTranslator
                                   .Translate(SceneManifest.DummyScene1)),
                        "Fails to load a test level.");
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
