using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Cursor.visible)
            Cursor.visible = false;

        var cursorPosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (Vector2)cursorPosInWorld;
    }
}
