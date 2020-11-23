using UnityEngine;


public class ShootMissileOnClick : MonoBehaviour
{
    public GameObject missile;
    public float initialSpeed = 4.0f;


    private Camera _camera;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && missile != null)
        {
            GameObject newMissile = Instantiate(
                missile,
                _camera.ScreenToWorldPoint(Input.mousePosition)
                    - _camera.transform.position.z * Vector3.forward,
                Quaternion.identity);
            newMissile.GetComponent<Rigidbody2D>().velocity =
                initialSpeed * Vector2.up;
        }
    }

    private void Awake()
    {
        _camera = Camera.main;
    }
}
