using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Tests
{
    class TestCircleGravity2D
    {
        [SetUp]
        public void SetUp()
        {
            SetUpScene();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_gravity);
            Object.Destroy(_gravityPlanet);
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
        public IEnumerator TestTransformNotInRegionHasNoGravity()
        {
            _testObject.transform.position =
                new Vector3(100.0f, 100.0f, 0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(Vector2.zero),
                        "Fails to give 0 gravity to transforms not in "
                        + "the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformCloseToRegionHasNoGravity()
        {
            _testObject.transform.position =
                new Vector3(23.0f, 23.0f, 0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(Vector2.zero),
                        "Fails to give 0 gravity to transforms that "
                        + " are very close to but not in the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformInRegionDoesHaveGravity()
        {
            float centerDistance = 20.0f;
            _testObject.transform.position =
                new Vector3(1.0f, 1.0f + centerDistance, 0.0f);
            Vector2 a = Vector2.down
                            * _gravity.gravitationalConstant * _gravity.mass
                            / Mathf.Pow(centerDistance, 2);
            Debug.Log($"Desired: {a.x}, {a.y}");
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(a),
                        "Fails to give correct gravity to transforms "
                        + "in the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformAtOffAxisPointHasGravity()
        {
            float centerDistance = 20.0f;
            _testObject.transform.position =
                new Vector3(1.0f + centerDistance / Mathf.Sqrt(2),
                            1.0f + centerDistance / Mathf.Sqrt(2),
                            0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(
                           _gravity.gravitationalConstant * _gravity.mass
                           / Mathf.Pow(centerDistance, 2)
                           * new Vector2(-1/Mathf.Sqrt(2), -1/Mathf.Sqrt(2))),
                        "Fails to give correct gravity to transforms "
                        + "in the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformDifferentDistanceHasGravity()
        {
            float centerDistance = 10.0f;
            _testObject.transform.position =
                new Vector3(1.0f + centerDistance / Mathf.Sqrt(2),
                            1.0f + centerDistance / Mathf.Sqrt(2),
                            0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(
                           _gravity.gravitationalConstant * _gravity.mass
                           / Mathf.Pow(centerDistance, 2)
                           * new Vector2(-1/Mathf.Sqrt(2), -1/Mathf.Sqrt(2))),
                        "Fails to give correct gravity to transforms "
                        + "in the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformAtCenter()
        {
            _testObject.transform.position =
                new Vector3(1.0f, 1.0f, 0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(Vector2.zero),
                        "Fails to give 0 gravity to transforms at the "
                        + "center of the region.");
            yield return null;
        }


        private GameObject _testObject;
        private GameObject _gravityPlanet;
        private CircleGravity2D _gravity;
        private Vector2 _planetPosition = new Vector2(1.0f, 1.0f);

        private void SetUpScene()
        {
            _testObject = new GameObject("Test Object");

            _gravityPlanet =
                new GameObject("Planet",
                               new System.Type[] {typeof(CircleCollider2D),
                                                  typeof(CircleGravity2D)});
            _gravityPlanet.transform.position = (Vector3) _planetPosition;
            CircleCollider2D _gravityRegion =
                _gravityPlanet.GetComponent<CircleCollider2D>();
            _gravityRegion.radius = 30.0f;
            _gravity = _gravityPlanet.GetComponent<CircleGravity2D>();
            _gravity.mass = 1.0f;
            _gravity.gravitationalConstant = 1.0f;
        }
    }
}
