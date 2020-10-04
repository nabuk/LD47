using UnityEngine;

public class ConstantRotator : MonoBehaviour
{
    [SerializeField]
    float zRotationFrom = default;

    [SerializeField]
    float zRotationTo = default;

    float zRotation;

    void Awake()
    {
        zRotation = Random.Range(zRotationFrom, zRotationTo);
    }

    void Update()
    {
        var currentZ = transform.eulerAngles.z;
        var newZ = currentZ + zRotation * Time.deltaTime;
        while (newZ < 0)
            newZ += 360f;
        while (newZ >= 360f)
            newZ -= 360f;

        transform.eulerAngles = new Vector3(0, 0, newZ);
    }
}

