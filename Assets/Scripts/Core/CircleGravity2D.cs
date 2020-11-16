using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class CircleGravity2D : MonoBehaviour
{
    public float mass = 1.0f;

    public Vector2 AccelerationAt(Transform otherTransform)
    {
        if (_gravityRegion.OverlapPoint((Vector2) otherTransform.position))
        {
            Vector2 otherLocalPosition =
                (Vector2) transform.InverseTransformPoint(
                    otherTransform.position);
            if (otherLocalPosition != _center)
            {
                return (_center - otherLocalPosition).normalized * mass
                    / Mathf.Pow(Vector2.Distance(_center, otherLocalPosition),
                                2);
            }
            else
            {
                return Vector2.zero;    
            }
        }
        else
        {
            return Vector2.zero;
        }
    }


    private CircleCollider2D _gravityRegion;
    private float _radius;
    private Vector2 _center;

    private void Awake()
    {
        _gravityRegion = GetComponent<CircleCollider2D>();
        _gravityRegion.isTrigger = true;
        _radius = _gravityRegion.radius;
        _center = _gravityRegion.offset;
    }
}
