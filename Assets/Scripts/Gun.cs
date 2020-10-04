using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Vector2 ProjectileSpawnPoint => projectileSpawnPoint.position;

    public void Shake()
    {
        if (!shaking)
        {
            shaking = true;
            StartCoroutine(ShakeEnumerator());
        }
    }

    [SerializeField]
    Transform projectileSpawnPoint = default;

    const float shakeDuration = 0.1f;
    bool shaking;

    IEnumerator ShakeEnumerator()
    {
        var org = transform.localPosition;
        var retracted = org + new Vector3(0, -0.05f, 0);

        transform.localPosition = retracted;

        yield return new WaitForSeconds(shakeDuration);

        transform.localPosition = org;

        shaking = false;
    }
}