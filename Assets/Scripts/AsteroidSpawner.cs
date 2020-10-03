using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public void Spawn()
    {
        //// barely passing slow asteroids
        //var gravityForceMag = 60f;
        //var minRotationDeg = 52;
        //var maxRotationDeg = 52;
        //var minInitialForce = 1.5f;
        //var maxInitialForce = 1.5f;
        //var distance = 10f;


        // barely passing slow asteroids
        var gravityForceMag = 60f;
        var minRotationDeg = 30;
        var maxRotationDeg = 58;
        var minInitialForce = 1.5f;
        var maxInitialForce = 3f;
        var distance = 10f;

        var position = Quaternion.Euler(0, 0, Random.Range(0, 360f)) * Vector2.up * distance;
        var initialForce = Random.Range(minInitialForce, maxInitialForce);
        var towardsPlanet = (-position).normalized;
        var zRotation = Random.Range(minRotationDeg, maxRotationDeg) * (Random.Range(-1, 1) < 0 ? -1 : 1);
        Vector2 v = Quaternion.Euler(0, 0, zRotation) * towardsPlanet * initialForce;


        var asteroid = Instantiate(asteroidPrefab, this.transform);
        asteroid.Initialize(
            position: position,
            forceVector: v,
            gravitySource: new Vector2(0, 0),
            gravityForceMag: gravityForceMag);
    }

    [SerializeField]
    Asteroid asteroidPrefab = default;
}