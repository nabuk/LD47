using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void Initialize(Vector2 position, float zRotationDeg, Vector2 v)
    {
        this.transform.position = position;
        this.transform.eulerAngles = new Vector3(0, 0, zRotationDeg);

        this.v = v;
        this.timeOfCreation = Time.time;
    }

    const float destroyAfterSeconds = 4;
    Vector2 v;
    float timeOfCreation;


    void Update()
    {
        this.transform.position += (Vector3)(v * Time.deltaTime);

        if (Time.time - timeOfCreation > destroyAfterSeconds)
        {
            Destroy(gameObject);
        }
    }
}

