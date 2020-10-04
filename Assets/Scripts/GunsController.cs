using UnityEngine;

public class GunsController : MonoBehaviour
{
    public bool GunsActive { get; set; }

    [SerializeField]
    ProjectileSpawner projectileSpawner = default;

    [SerializeField]
    SfxPlayer sfxPlayer = default;

    Gun[] guns;
    bool skipNextFrame; // to avoid firing as continue

    void Awake()
    {
        guns = GetComponentsInChildren<Gun>();
    }

    void Update()
    {
        if (!GunsActive)
        {
            skipNextFrame = true;
            return;
        }

        var mousePosition = GetMousePosition();
        if (mousePosition != Vector2.zero)
        {
            var angle = Vector2.SignedAngle(Vector2.right, mousePosition) - 90f;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        if (!skipNextFrame && Input.GetMouseButtonDown(0))
        {
            sfxPlayer.PlayLaserShot();
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
                gun.Shake();
            }
                
        }

        skipNextFrame = false;
    }

    Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

