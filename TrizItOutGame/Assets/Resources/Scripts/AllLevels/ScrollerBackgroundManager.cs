using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollerBackgroundManager : MonoBehaviour
{
    public RawImage m_Image;
    public float m_X, m_Y;

    void Update()
    {
        m_Image.uvRect = new Rect(m_Image.uvRect.position + new Vector2(m_X, m_Y) * Time.deltaTime, m_Image.uvRect.size);
    }
}
 