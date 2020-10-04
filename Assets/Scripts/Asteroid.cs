using UnityEngine;

public class Asteroid : MonoBehaviour, ICollisionHandler
{
    public bool IsIdle => isIdle;

    public CollisionObjectType Type => CollisionObjectType.Asteroid;

    public void Initialize(
        Vector2 position,
        Vector2 forceVector,
        Vector2 gravitySource,
        float gravityForceMag,
        bool isIdle,
        SfxPlayer sfxPlayer)
    {
        this.transform.position = position;
        this.v = forceVector;
        this.gravitySource = gravitySource;
        this.gravityForceMag = gravityForceMag;
        this.isIdle = isIdle;
        this.sfxPlayer = sfxPlayer;

        var scale = Random.Range(0.6f, 1.4f);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    [SerializeField]
    SpriteRenderer spriteRenderer = default;

    [SerializeField]
    Sprite dyingSprite1 = default;

    [SerializeField]
    Sprite dyingSprite2 = default;

    Collider2D currentCollider = default;

    const float destroyNotSoonerThanSec = 5;
    const float dyingDuration = 0.12f;
    float r;
    Vector2 v;
    Vector2 gravitySource;
    float gravityForceMag;
    float createdAt;
    bool isIdle;
    SfxPlayer sfxPlayer;
    float timeTillDead = float.MinValue;

    bool DyingAnimation => timeTillDead != float.MinValue;

    void Awake()
    {
        createdAt = Time.time;
        r = this.GetComponent<CircleCollider2D>().radius;
        currentCollider = this.GetComponent<CircleCollider2D>();
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

        if (!DyingAnimation)
        {
            var hits = Physics2D.OverlapCircleAll(this.transform.position, r);
            this.HandleHits(hits);
        }

        CheckIfShouldDestroyBecauseWentOffScreen();

        HandleDying();
    }

    void ICollisionHandler.CollidedWith(CollisionObjectType objectType)
    {
        if (spriteRenderer.isVisible)
        {
            var noSoundAfterLength = 30f;
            var volume = (noSoundAfterLength - Mathf.Clamp(transform.position.magnitude, 0f, noSoundAfterLength)) / noSoundAfterLength;
            sfxPlayer.PlayRockCollision(volume);
        }

        this.currentCollider.enabled = false;

        if (objectType == CollisionObjectType.Planet)
            v = new Vector2(0, 0);

        timeTillDead = dyingDuration;
    }

    void CheckIfShouldDestroyBecauseWentOffScreen()
    {
        if (Time.time - createdAt > destroyNotSoonerThanSec
            && transform.position.magnitude > AsteroidSpawner.SpawnDistance * 0.95f)
        {
            Destroy(gameObject);
        }
    }

    void HandleDying()
    {
        if (DyingAnimation)
        {
            timeTillDead -= Time.deltaTime;

            var a = Mathf.Clamp(timeTillDead / dyingDuration, 0, 1);
            spriteRenderer.color = new Color(1, 1, 1, a);

            if (a > 0.5f)
                spriteRenderer.sprite = dyingSprite1;
            else
                spriteRenderer.sprite = dyingSprite2;

            if (timeTillDead <= 0f)
                Destroy(gameObject);
        }
    }
}
