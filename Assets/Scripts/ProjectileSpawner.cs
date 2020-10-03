using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public void Spawn(Vector2 position, float zRotationDeg, Vector2 v)
    {
        var projectile = Instantiate(projectilePrefab, this.transform);

        projectile.Initialize(
            position,
            zRotationDeg,
            v);
    }

    [SerializeField]
    Projectile projectilePrefab = default;
}
