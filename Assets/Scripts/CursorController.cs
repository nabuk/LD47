using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Awake()
    {
#if UNITY_WEBGL
        Cursor.SetCursor(new Texture2D(32, 32), new Vector2(0, 0), CursorMode.ForceSoftware);
#else
        //Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
#endif

        Cursor.visible = false;



    }

    void Start()
    {
#if UNITY_WEBGL
        Cursor.SetCursor(new Texture2D(32, 32), new Vector2(0, 0), CursorMode.ForceSoftware);
#else
        //Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
#endif

        Cursor.visible = false;
    }

    void Update()
    {
        //if (Cursor.visible)
            Cursor.visible = false;

        var cursorPosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (Vector2)cursorPosInWorld;
    }
}
