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
    [SerializeField]
    private bool m_IsEmpty = true;

    private InventoryItem m_InventoryItem = null;

    public bool IsEmpty
    {
        set { m_IsEmpty = value; }
        get { return m_IsEmpty; }
    }

    public InventoryItem InventoryItem
    {
        get { return m_InventoryItem; }
        set
        {
            m_InventoryItem = value;
            IsEmpty = false;
            m_SlotItemImage.GetComponent<Image>().sprite = m_InventoryItem.Image;
            m_InventoryItem.InInventory = true;
            m_InventoryItem.gameObject.SetActive(false);
        }
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
            m_ZoomInWindow.transform.Find("Item").GetComponent<Image>().sprite = imageToDisplay.sprite;
        }

    }

}
