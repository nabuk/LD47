﻿using UnityEngine;

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
    }

    [SerializeField]
    SpriteRenderer spriteRenderer = default;

    const float destroyNotSoonerThanSec = 5;
    float r;
    Vector2 v;
    Vector2 gravitySource;
    float gravityForceMag;
    float createdAt;
    bool isIdle;
    SfxPlayer sfxPlayer;

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

        if (spriteRenderer.isVisible)
        {
            var noSoundAfterLength = 30f;
            var volume = (noSoundAfterLength - Mathf.Clamp(transform.position.magnitude, 0f, noSoundAfterLength)) / noSoundAfterLength;
            sfxPlayer.PlayRockCollision(volume);
        }
        
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
