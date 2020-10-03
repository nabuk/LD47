using UnityEngine;

public class Projectile : MonoBehaviour, ICollisionHandler
{
    public CollisionObjectType Type => CollisionObjectType.Projectile;

    public void Initialize(Vector2 position, float zRotationDeg, Vector2 v)
    {
        this.transform.position = position;
        this.transform.eulerAngles = new Vector3(0, 0, zRotationDeg);

        this.v = v;
        this.timeOfCreation = Time.time;
    }

    [SerializeField]
    Transform basePoint = default;

    const float destroyAfterSeconds = 2;
    Vector2 v;
    float timeOfCreation;

    void Update()
    {
        this.transform.position += (Vector3)(v * Time.deltaTime);

        if (Time.time - timeOfCreation > destroyAfterSeconds)
        {
            Destroy(gameObject);
        }

        var hit = Physics2D.Raycast(basePoint.position, v, (transform.position - basePoint.position).magnitude * 2);
        if (hit.collider != null)
            this.HandleHits(hit.collider);
    }

    void ICollisionHandler.CollidedWith(CollisionObjectType objectType)
    {
        Destroy(gameObject);
    }
}

