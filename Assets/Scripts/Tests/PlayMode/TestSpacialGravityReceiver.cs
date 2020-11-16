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
    class TestSpacialGravityReceiver
    {
        [SetUp]
        public void SetUp()
        {
            SetUpScene();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_gravityReceiverObject);
            Object.Destroy(_gravityPlanet0);
            Object.Destroy(_gravityPlanet1);
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
        public IEnumerator TestDoesntGetVelocityInZeroGravity()
        {
            _rigidbody.MovePosition(new Vector2(0.0f, 100.0f));
            _rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < 3; ++i)
                yield return null;
            Assert.That(_rigidbody.velocity.normalized,
                        Is.EqualTo(Vector2.zero),
                        "Moves the GameObject even though it's in zero "
                        + "gravity.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestMovesTowardCenterOfRegion()
        {
            _rigidbody.MovePosition(_planet0Position
                                    + new Vector2(0, 20.0f));
            _rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < 5; ++i)
                yield return null;
            Assert.That(_rigidbody.velocity.normalized,
                        Is.EqualTo(Vector2.down),
                        "Fails to make the GameObject move toward the "
                        + "center of the gravity region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestMovesTowardCenterOfRegionOffAxis()
        {
            float distanceFromCenter = 20.0f;
            Vector2 displacementDirection =
                new Vector2(-1.0f / Mathf.Sqrt(2),
                            1.0f / Mathf.Sqrt(2));
            _rigidbody.MovePosition(
                _planet0Position
                + distanceFromCenter * displacementDirection);
            _rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < 3; ++i)
                yield return null;
            Assert.That(
                (_rigidbody.velocity.normalized
                 + displacementDirection).magnitude,
                Is.LessThan(0.0001),
                "Fails to make the GameObject move toward the center "
                + "of the gravity region when off axis.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestDoesntMoveWhenBarelyOutside()
        {
            // The receiver's collider intersects with the gravity
            // region, but not the transform.
            float distanceFromCenter = 30.5f;
            Vector2 displacementDirection =
                new Vector2(-1.0f / Mathf.Sqrt(2),
                            1.0f / Mathf.Sqrt(2));
            _rigidbody.MovePosition(
                _planet0Position
                + distanceFromCenter * displacementDirection);
            _rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < 3; ++i)
                yield return null;
            Assert.That(_rigidbody.velocity.normalized,
                        Is.EqualTo(Vector2.zero),
                        "Fails to not move the GameObject when its "
                        + "barely not in the gravity region.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestDoesntMoveWithEqualForceFromBothPlanets()
        {
            _rigidbody.MovePosition(Vector2.zero);
            _rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < 3; ++i)
                yield return null;
            Assert.That(_rigidbody.velocity.normalized,
                        Is.EqualTo(Vector2.zero),
                        "Moves the GameObject when it's at an "
                        + "equilibrium position.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestMovesDownWhenAboveEquilibrium()
        {
            _rigidbody.MovePosition(new Vector2(0.0f, 22.0f));
            _rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < 3; ++i)
                yield return null;
            Assert.That(_rigidbody.velocity.normalized,
                        Is.EqualTo(Vector2.down),
                        "Fails to move the GameObject down when it's "
                        + "above the equilibrium point.");
            yield return null;
        }


        private GameObject _gravityReceiverObject;
        private Rigidbody2D _rigidbody;
        private GameObject _gravityPlanet0;
        private Vector2 _planet0Position = new Vector2(-20.0f, 0.0f);
        private GameObject _gravityPlanet1;
        private Vector2 _planet1Position = new Vector2(20.0f, 0.0f);

        private void SetUpScene()
        {
            _gravityReceiverObject =
                new GameObject(
                    "Gravity Receiver",
                    new System.Type[] {typeof(Rigidbody2D),
                                       typeof(CircleCollider2D),
                                       typeof(SpacialGravityReceiver)});
            _gravityReceiverObject.transform.position = new Vector3(0.0f,
                                                                    100.0f,
                                                                    0.0f);
            _rigidbody = _gravityReceiverObject.GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0.0f;
            _rigidbody.drag = 0.0f;
            _rigidbody.mass = 1.0f;
            CircleCollider2D circleCollider = 
                _gravityReceiverObject.GetComponent<CircleCollider2D>();
            circleCollider.radius = 1.0f;
            SpacialGravityReceiver gravityReceiver =
                _gravityReceiverObject.GetComponent<SpacialGravityReceiver>();

            _gravityPlanet0 =
                new GameObject("Planet 0",
                               new System.Type[] {typeof(CircleCollider2D),
                                                  typeof(CircleGravity2D)});
            _gravityPlanet0.transform.position = (Vector3) _planet0Position;
            CircleCollider2D gravityRegion0 =
                _gravityPlanet0.GetComponent<CircleCollider2D>();
            gravityRegion0.radius = 30.0f;
            CircleGravity2D gravity0 =
                _gravityPlanet0.GetComponent<CircleGravity2D>();
            gravity0.mass = 1.0f;
            gravity0.gravitationalConstant = 1.0f;

            _gravityPlanet1 =
                new GameObject("Planet 1",
                               new System.Type[] {typeof(CircleCollider2D),
                                                  typeof(CircleGravity2D)});
            _gravityPlanet1.transform.position = (Vector3) _planet1Position;
            CircleCollider2D gravityRegion1 =
                _gravityPlanet1.GetComponent<CircleCollider2D>();
            gravityRegion1.radius = 30.0f;
            CircleGravity2D gravity1 =
                _gravityPlanet1.GetComponent<CircleGravity2D>();
            gravity1.mass = 1.0f;
            gravity1.gravitationalConstant = 1.0f;
        }
    }
}
