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
                        Is.EqualTo(0.0f).Within(1).Ulps,
                        "Fails to give 0 gravity to transforms not in "
                        + "the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformInRegionDoesHaveGravity()
        {
            float centerDistance = 20.0f;
            _testObject.transform.position =
                new Vector3(0.0f, centerDistance, 0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(6.674e-11*_gravity.mass
                                   / Mathf.Pow(centerDistance, 2))
                          .Within(1).Ulps,
                        "Fails to give correct gravity to transforms "
                        + "in the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformAtOffAxisPointHasGravity()
        {
            float centerDistance = 20.0f;
            _testObject.transform.position =
                new Vector3(Mathf.Sqrt(centerDistance),
                            Mathf.Sqrt(centerDistance),
                            0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(_GRAVITATIONAL_CONSTANT*_gravity.mass
                                   / Mathf.Pow(centerDistance, 2))
                          .Within(1).Ulps,
                        "Fails to give correct gravity to transforms "
                        + "in the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformDifferentDistanceHasGravity()
        {
            float centerDistance = 10.0f;
            _testObject.transform.position =
                new Vector3(Mathf.Sqrt(centerDistance),
                            Mathf.Sqrt(centerDistance),
                            0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(_GRAVITATIONAL_CONSTANT*_gravity.mass
                                   / Mathf.Pow(centerDistance, 2))
                          .Within(1).Ulps,
                        "Fails to give correct gravity to transforms "
                        + "in the region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTransformAtCenter()
        {
            _testObject.transform.position =
                new Vector3(0.0f, 0.0f, 0.0f);
            Assert.That(_gravity.AccelerationAt(_testObject.transform),
                        Is.EqualTo(0.0f).Within(1).Ulps,
                        "Fails to give 0 gravity to transforms at the "
                        + "center of the region.");
            yield return null;
        }        



        private const float _GRAVITATIONAL_CONSTANT = 6.674e-11f;
        private GameObject _testObject;
        private GameObject _gravityPlanet;
        private CircleGravity2D _gravity;

        private void SetUpScene()
        {
            _testObject = new GameObject("Test Object");

            _gravityPlanet = new GameObject("Planet 0");
            _gravityPlanet.transform.position =
                new Vector3(0.0f, 0.0f, 0.0f);
            _gravityPlanet.AddComponent<CircleCollider2D>();
            CircleCollider2D _gravityRegion =
                _gravityPlanet.GetComponent<CircleCollider2D>();
            _gravityRegion.radius = 30.0f;
            _gravityPlanet.AddComponent<CircleGravity2D>();
            _gravity = _gravityPlanet.GetComponent<CircleGravity2D>();
            _gravity.mass = 1.0f;
        }
    }
}
