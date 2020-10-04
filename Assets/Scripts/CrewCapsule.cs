using System;
using UnityEngine;

public class CrewCapsule : MonoBehaviour, ICollisionHandler
{
    public CollisionObjectType Type => CollisionObjectType.CrewCapsule;

    public event Action Died = delegate { };
    public event Action<CollisionObjectType> LostLife = delegate { };

    public void BeginPlaythrough()
    {
        lives = 3;
        invincibleTimeLeft = 0;
        isActivePlaythrough = true;
        crewCapsuleCollider.enabled = true;
    }

    public void StopPlaythrough()
    {
        isActivePlaythrough = false;
    }

    [SerializeField]
    float orbitalPeriod = 4f;

    [SerializeField]
    SpriteRenderer spriteRenderer = default;

    [SerializeField]
    Collider2D crewCapsuleCollider = default;

    const float invincibleAlpha = 0.5f;
    const float cooldownAfterHitSec = 2f;
    int lives;
    float p;
    float r;
    bool isActivePlaythrough = false;
    float invincibleTimeLeft = 0;

    void Awake()
    {
        r = transform.position.magnitude;
        p = 0;
    }

    void Update()
    {
        var timeScale = Time.deltaTime;
        p = Mathf.Repeat(p + timeScale / orbitalPeriod, 1f);
        transform.position = CalculatePosition(p, r);

        if (invincibleTimeLeft > 0)
            invincibleTimeLeft -= Time.deltaTime;
        else
            crewCapsuleCollider.enabled = true;

        var alpha = invincibleTimeLeft > 0
            ? invincibleAlpha + (1f - invincibleAlpha) * (cooldownAfterHitSec - invincibleTimeLeft) / cooldownAfterHitSec
            : 1;
        var c = spriteRenderer.color;
        spriteRenderer.color = new Color(c.r, c.g, c.b, alpha);
    }

    Vector2 CalculatePosition(float p, float r)
    {
        var angleAdjustment = 0.25f; // so that it starts from the top, not right
        var a = (p + angleAdjustment) * 360f * Mathf.Deg2Rad;
        return new Vector2(r * Mathf.Cos(a), r * Mathf.Sin(a));
    }

    void ICollisionHandler.CollidedWith(CollisionObjectType objectType)
    {
        if (!isActivePlaythrough || invincibleTimeLeft > 0)
            return;

        if (lives > 0)
        {
            lives--;

            if (lives == 0)
            {
                Died();
            }
            else
            {
                LostLife(objectType);
                this.invincibleTimeLeft = cooldownAfterHitSec;
                crewCapsuleCollider.enabled = false;
            }
        }
    }
}
