using UnityEngine;

public class GunsController : MonoBehaviour
{
    [SerializeField]
    ProjectileSpawner projectileSpawner = default;

    Gun[] guns;

    void Awake()
    {
        guns = GetComponentsInChildren<Gun>();
    }

    void Update()
    {
        var mousePosition = GetMousePosition();
        if (mousePosition != Vector2.zero)
        {
            var angle = Vector2.SignedAngle(Vector2.right, mousePosition) - 90f;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var projectileSpeed = 16f;
            var zAngle = transform.eulerAngles.z;
            Vector2 v = Quaternion.Euler(0, 0, zAngle) * (Vector2.up * projectileSpeed);

            foreach (var gun in guns)
            {
                projectileSpawner.Spawn(
                    gun.ProjectileSpawnPoint,
                    zAngle,
                    v
                    );
            }
                
        }
    }

    Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

