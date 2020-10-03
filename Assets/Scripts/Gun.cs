using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Transform projectileSpawnPoint = default;

    public Vector2 ProjectileSpawnPoint => projectileSpawnPoint.position;
}