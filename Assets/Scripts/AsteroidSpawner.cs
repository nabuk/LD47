using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public void Spawn()
    {
        var asteroidObject = Instantiate(asteroidPrefab, this.transform);
        var asteroid = asteroidObject.GetComponent<Asteroid>();
        asteroid.Initialize(
            position: new Vector2(-5, 4),
            forceVector: new Vector2(2, 0),
            gravitySource: new Vector2(0, 0),
            gravityForceMag: 16f);
    }

    [SerializeField]
    GameObject asteroidPrefab = default;
}