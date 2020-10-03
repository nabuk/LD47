public interface ICollisionHandler
{
    CollisionObjectType Type { get; }
    void CollidedWith(CollisionObjectType objectType);
}
