using UnityEngine;

public class Planet : MonoBehaviour, ICollisionHandler
{
    public CollisionObjectType Type => CollisionObjectType.Planet;

    public void CollidedWith(CollisionObjectType objectType)
    {
        // TODO: cooldown ?
    }
}
