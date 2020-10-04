using UnityEngine;

public class Projectile : MonoBehaviour, ICollisionHandler
{
    public CollisionObjectType Type => CollisionObjectType.Projectile;

    public void Initialize(Vector2 position, float zRotationDeg, Vector2 v, SfxPlayer sfxPlayer)
    {
        this.transform.position = position;
        this.transform.eulerAngles = new Vector3(0, 0, zRotationDeg);

        this.v = v;
        this.timeOfCreation = Time.time;
        this.sfxPlayer = sfxPlayer;
    }

    [SerializeField]
    Transform basePoint = default;

    [SerializeField]
    SpriteRenderer spriteRenderer = default;

    const float destroyAfterSeconds = 2;
    Vector2 v;
    float timeOfCreation;
    SfxPlayer sfxPlayer;
    

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
        if (spriteRenderer.isVisible)
        {
            var noSoundAfterLength = 30f;
            var volume = (noSoundAfterLength - Mathf.Clamp(transform.position.magnitude, 0f, noSoundAfterLength)) / noSoundAfterLength;
            sfxPlayer.PlayLaserHit(volume);
        }

        Destroy(gameObject);
    }
}

