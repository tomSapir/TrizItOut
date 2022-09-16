using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D m_NormalCursor;
    public Texture2D m_HighlightCursor;

    void Start()
    {
        Cursor.SetCursor(m_NormalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeCursorToHighlight()
    {
        Cursor.SetCursor(m_HighlightCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeCursorToNormal()
    {
        Cursor.SetCursor(m_NormalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
