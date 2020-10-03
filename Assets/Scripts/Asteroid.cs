using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public void Initialize(
        Vector2 position,
        Vector2 forceVector,
        Vector2 gravitySource,
        float gravityForceMag)
    {
        this.transform.position = position;
        this.v = forceVector;
        this.gravitySource = gravitySource;
        this.gravityForceMag = gravityForceMag;
    }

    Vector2 v;
    Vector2 gravitySource;
    float gravityForceMag;

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
    }
}
