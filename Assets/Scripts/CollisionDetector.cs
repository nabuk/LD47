using UnityEngine;

public static class CollisionDetector
{
    public static void HandleHits<T>(this T original, params Collider2D[] hits)
        where T : MonoBehaviour, ICollisionHandler
    {
        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            if (hit.gameObject != original.gameObject)
            {
                var thisCollisionHandler = original.GetComponent<ICollisionHandler>();
                var otherCollisionHandler = hit.gameObject.GetComponent<ICollisionHandler>();

                thisCollisionHandler.CollidedWith(otherCollisionHandler.Type);
                otherCollisionHandler.CollidedWith(thisCollisionHandler.Type);
                return;
            }
        }
    }
}
