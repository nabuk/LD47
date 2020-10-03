using UnityEngine;

public class Planet : MonoBehaviour, ICollisionHandler
{
    public CollisionObjectType Type => CollisionObjectType.Planet;

    void ICollisionHandler.CollidedWith(CollisionObjectType objectType)
    {
        // TODO: cooldown ?
    }
}
