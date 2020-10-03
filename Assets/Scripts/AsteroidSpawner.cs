using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public void Spawn()
    {
        var asteroid = Instantiate(asteroidPrefab, this.transform);
        asteroid.Initialize(
            position: new Vector2(-5, 4),
            forceVector: new Vector2(2, 0),
            gravitySource: new Vector2(0, 0),
            gravityForceMag: 16f);
    }

    [SerializeField]
    Asteroid asteroidPrefab = default;
}