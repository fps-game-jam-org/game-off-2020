using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach this to any GameObject you want to experience spacial
/// gravity from a CircleCollider2D.  The gravity acts as a force
/// applied to the RigidBody2D this component is a sibling of.  Note
/// that the force experienced will scaled linearly with the mass of
/// the rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SpacialGravityReceiver : MonoBehaviour
{
    private Dictionary<int, CircleGravity2D> _activeGravityRegions;
    private Rigidbody2D _rigidbody;

    private Vector2 CurrentAcceleration()
    {
        Vector2 acceleration = Vector2.zero;
        foreach (KeyValuePair<int, CircleGravity2D> gravityRegion
                 in _activeGravityRegions)
        {
            acceleration =
                acceleration + gravityRegion.Value.AccelerationAt(transform);
        }
        return acceleration;
    }

    private void Awake()
    {
        _activeGravityRegions = new Dictionary<int, CircleGravity2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_rigidbody.mass * CurrentAcceleration());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CircleGravity2D gravityRegion =
            other.gameObject.GetComponent<CircleGravity2D>();
        if (gravityRegion != null
            && !_activeGravityRegions.ContainsKey(
                other.gameObject.GetInstanceID()))
        {
            _activeGravityRegions.Add(other.gameObject.GetInstanceID(),
                                      gravityRegion);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CircleGravity2D>() != null
            && _activeGravityRegions.ContainsKey(
                other.gameObject.GetInstanceID()))
        {
            _activeGravityRegions.Remove(other.gameObject.GetInstanceID());
        }
    }
}
