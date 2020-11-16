using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class CircleGravity2D : MonoBehaviour
{
    public float mass = 0.0f;

    public float AccelerationAt(Transform position)
    {
        return float.NaN;
    }
}
