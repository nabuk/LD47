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
        ApplySpriteByLives();
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
    SpriteRenderer beepSpriteRenderer = default;

    [SerializeField]
    Collider2D crewCapsuleCollider = default;

    [SerializeField]
    Sprite capsule0Hits = default;

    [SerializeField]
    Sprite capsule1Hit = default;

    [SerializeField]
    Sprite capsule2Hits = default;

    [SerializeField]
    Sprite capsule3Hits = default;

    const float invincibleAlpha = 0.5f;
    const float cooldownAfterHitSec = 2f;
    const float beepPeriod = 2f;
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
        beepSpriteRenderer.color = new Color(c.r, c.g, c.b, alpha);

        ApplyBeep();
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

            ApplySpriteByLives();

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

    void ApplySpriteByLives()
    {
        switch (lives)
        {
            case 3: spriteRenderer.sprite = capsule0Hits; break;
            case 2: spriteRenderer.sprite = capsule1Hit; break;
            case 1: spriteRenderer.sprite = capsule2Hits; break;
            case 0: spriteRenderer.sprite = capsule3Hits; break;
        }
    }

    void ApplyBeep()
    {
        var p = Mathf.Repeat(Time.time, beepPeriod) / beepPeriod;
        bool beepOn = lives > 0 && p > 0.5f;
        beepSpriteRenderer.enabled = beepOn;
    }
}
