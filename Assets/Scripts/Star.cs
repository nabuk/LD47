using UnityEngine;

public class Star : MonoBehaviour
{
    SpriteRenderer spriteRenderer = default;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        var zRotation = Random.Range(0f, 360f);
        var xScale = Random.Range(0.5f, 1f);
        var yScale = Random.Range(0.5f, 1f);
        var brightness = Random.Range(0.2f, 0.7f);

        transform.eulerAngles = new Vector3(0, 0, zRotation);
        transform.localScale = new Vector3(xScale, yScale, 1);
        spriteRenderer.color = new Color(brightness, brightness, brightness);
    }
}
