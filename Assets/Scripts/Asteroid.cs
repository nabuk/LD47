using UnityEngine;

public class Asteroid : MonoBehaviour, ICollisionHandler
{
    public CollisionObjectType Type => CollisionObjectType.Asteroid;

    public void Initialize(
        Vector2 position,
        Vector2 forceVector,
        Vector2 gravitySource,
        float gravityForceMag)
    {
        this.transform.position = position;
        this.v = forceVector;
        this.gravitySource = gravitySource;
        this.gravityForceMag = gravityForceMag;
    }

    const float destroyNotSoonerThanSec = 5;
    float r;
    Vector2 v;
    Vector2 gravitySource;
    float gravityForceMag;
    float createdAt;
    

    void Awake()
    {
        createdAt = Time.time;
        r = this.GetComponent<CircleCollider2D>().radius;
    }


    void Update()
    {
        var timeScale = Time.deltaTime;
        var p = (Vector2)transform.position;
        
        var gv = gravitySource - p;
        var gvdir = gv.normalized;
        var gvmag2 = gv.sqrMagnitude;

        var gForceVector = gvdir * gravityForceMag / gvmag2;

        v += gForceVector * timeScale;

        transform.position += (Vector3)(v * timeScale);

        var hits = Physics2D.OverlapCircleAll(this.transform.position, r);
        this.HandleHits(hits);

        CheckIfShouldDestroyBecauseWentOffScreen();
    }

    void ICollisionHandler.CollidedWith(CollisionObjectType objectType)
    {
        //TODO: some effect before

        Destroy(gameObject);
    }

    void CheckIfShouldDestroyBecauseWentOffScreen()
    {
        if (Time.time - createdAt > destroyNotSoonerThanSec
            && transform.position.magnitude > AsteroidSpawner.SpawnDistance * 0.95f)
        {
            Destroy(gameObject);
        }
    }
}
