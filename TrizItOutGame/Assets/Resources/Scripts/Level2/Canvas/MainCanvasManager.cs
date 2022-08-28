using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    public GameObject m_ZoomInWindow;

    public void OnClickCloseZoomInventory()
    {
        m_ZoomInWindow.SetActive(false);
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
}
