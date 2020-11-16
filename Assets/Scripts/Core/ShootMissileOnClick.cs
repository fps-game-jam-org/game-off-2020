using UnityEngine;


public class ShootMissileOnClick : MonoBehaviour
{
    public GameObject missile;
    public Camera camera;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && missile != null)
        {
            GameObject newMissile = Instantiate(
                missile,
                camera.ScreenToWorldPoint(Input.mousePosition)
                    - camera.transform.position.z*Vector3.forward,
                Quaternion.identity);
            newMissile.GetComponent<Rigidbody2D>().velocity = Vector2.up;
            Destroy(newMissile, 5.0f);
        }
    }
}
