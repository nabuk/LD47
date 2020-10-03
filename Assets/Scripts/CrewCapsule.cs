﻿using UnityEngine;

public class CrewCapsule : MonoBehaviour, ICollisionHandler
{
    public CollisionObjectType Type => CollisionObjectType.CrewCapsule;

    [SerializeField]
    float orbitalPeriod = 4f;

    float p;
    float r;

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
    }

    Vector2 CalculatePosition(float p, float r)
    {
        var angleAdjustment = 0.25f; // so that it starts from the top, not right
        var a = (p + angleAdjustment) * 360f * Mathf.Deg2Rad;
        return new Vector2(r * Mathf.Cos(a), r * Mathf.Sin(a));
    }

    void ICollisionHandler.CollidedWith(CollisionObjectType objectType)
    {
        // TODO: A projectile or asteroid just hit it. React
    }
}
