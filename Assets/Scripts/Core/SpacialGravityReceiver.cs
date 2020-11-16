using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class SpacialGravityReceiver : MonoBehaviour
{
    private Dictionary<string, CircleGravity2D> _activeGravityRegions;
    private Rigidbody2D _rigidbody;

    private Vector2 CurrentAcceleration()
    {
        Vector2 acceleration = Vector2.zero;
        foreach (KeyValuePair<string, CircleGravity2D> gravityRegion
                 in _activeGravityRegions)
        {
            acceleration =
                acceleration + gravityRegion.Value.AccelerationAt(transform);
        }
        return acceleration;
    }

    private void Awake()
    {
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
        if (gravityRegion != null)
        {
            _activeGravityRegions.Add(other.gameObject.tag, gravityRegion);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CircleGravity2D>() != null)
        {
            _activeGravityRegions.Remove(other.gameObject.tag);
        }
    }
}
