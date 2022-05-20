using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SlotItemImage;
    [SerializeField]
    private GameObject m_ZoomInWindow;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void OnClickMagnifierGlass()
    {
        Image imageToDisplay = m_SlotItemImage.GetComponent<Image>();

        if (imageToDisplay == null)
        {
            Debug.LogError("Could not find the image.");
        }
        else
        {
            m_ZoomInWindow.SetActive(true);
            // TODO: stop here
            //m_ZoomInWindow.
        }

    }

}
