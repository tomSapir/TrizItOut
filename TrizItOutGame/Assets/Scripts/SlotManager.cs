using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject m_SlotItemImage;
    [SerializeField]
    private GameObject m_ZoomInWindow;
    [SerializeField]
    private bool m_IsEmpty = true;

    private InventoryItemManager m_InventoryItemManager = null; // Not in use yet...
    private GameObject m_inventory;

    public enum Property { usable, displayable };
    public Property ItemProperty { get; private set; }
    private string m_displayImage; // For displayable objects - the more informative image for the zoomIn window.

    void Start()
    {
        m_inventory = GameObject.Find("Inventory");
    }

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
        if(m_IsEmpty == false)
        {
            if(ItemProperty == Property.usable)
            {
                m_ZoomInWindow.transform.Find("Item").GetComponent<Image>().sprite = m_SlotItemImage.GetComponent<Image>().sprite;
            }
            else
            {
                m_ZoomInWindow.transform.Find("Item").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Displayables/" + m_displayImage);
            }
            m_ZoomInWindow.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(m_IsEmpty == false)
        {
           m_inventory.GetComponent<InventoryManager>().m_previouslySelectedSlot = m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot;
           m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot = this.gameObject;
        }
    }

    public void AssignPtoperty(int i_ordrNumber, string i_displayImage)
    {
        ItemProperty = (Property)i_ordrNumber;
        this.m_displayImage = i_displayImage;
    }

}
