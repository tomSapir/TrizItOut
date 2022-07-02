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

    private InventoryItemManager m_InventoryItemManager = null;

    public bool IsEmpty
    {
        set { m_IsEmpty = value; }
        get { return m_IsEmpty; }
    }

    public InventoryItemManager InventoryItemManager
    {
        get { return m_InventoryItemManager; }
        set
        {
            m_InventoryItemManager = value;
            IsEmpty = false;
            m_SlotItemImage.GetComponent<Image>().sprite = m_InventoryItemManager.Image;
            m_InventoryItemManager.InInventory = true;
            m_InventoryItemManager.gameObject.SetActive(false);
        }
    }

    public void OnClickMagnifierGlass()
    {
        Image imageToDisplay = null;

        if (m_SlotItemImage != null)
        {
            imageToDisplay = m_SlotItemImage.GetComponent<Image>();
            Debug.Log("Zoo, window");

        }

        if (imageToDisplay == null)
        {
            Debug.LogError("Could not find the image to zoom in.");
        }
        else if(m_IsEmpty == false)
        {
            m_ZoomInWindow.SetActive(true);
            m_ZoomInWindow.transform.Find("Item").GetComponent<Image>().sprite = imageToDisplay.sprite;
        }
    }
}
