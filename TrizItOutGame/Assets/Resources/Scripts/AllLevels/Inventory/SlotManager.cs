using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IPointerClickHandler
{
    public delegate void ZoomBtnDelegate();
    public enum Property { usable, displayable, empty };

    public GameObject m_SlotItemImage;
    public GameObject m_ZoomInWindow;
    public bool IsEmpty { get; set; } = true;
    private InventoryManager m_InventoryManager;
    public string CombinationItem { get; set; }
    public Property ItemProperty { get;  set; }
    public int AmountOfUsage { get; set; }
    public string m_displayImage; // For displayable objects - the more informative image for the zoomIn window.

    public event ZoomBtnDelegate OnClickZoomBtn;

    void Start()
    {
        m_InventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    public void OnClickMagnifierGlass()
    {
        if(IsEmpty == false)
        {
            OnClickZoomBtn?.Invoke();
            if (ItemProperty == Property.usable)
            {
                m_ZoomInWindow.transform.Find("Item").GetComponent<Image>().sprite = m_SlotItemImage.GetComponent<Image>().sprite;
            }
            else
            {
                m_ZoomInWindow.transform.Find("Item").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level1/Displayables/" + m_displayImage);
            }
            m_ZoomInWindow.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.gameObject == m_InventoryManager.m_CurrentSelectedSlot)
        {
            m_InventoryManager.m_CurrentSelectedSlot = null;
        }
        else if(IsEmpty == false)
        {
           m_InventoryManager.m_PreviouslySelectedSlot = m_InventoryManager.m_CurrentSelectedSlot;
           m_InventoryManager.m_CurrentSelectedSlot = this.gameObject;
           Combine();
        }
    }

    public void AssignPtoperty(int i_ordrNumber, string i_displayImage, string i_combinationItem, int i_amountOfUsage)
    {
        ItemProperty = (Property)i_ordrNumber;
        this.m_displayImage = i_displayImage;
        this.CombinationItem = i_combinationItem;
        this.AmountOfUsage = i_amountOfUsage;
    }

    public void Combine()
    {
        if(m_InventoryManager.m_PreviouslySelectedSlot != null &&
            m_InventoryManager.m_PreviouslySelectedSlot.GetComponent<SlotManager>().CombinationItem == this.gameObject.GetComponent<SlotManager>().CombinationItem 
            && this.gameObject.GetComponent<SlotManager>().CombinationItem != string.Empty)
        {
            var combinedItem = Instantiate(Resources.Load<GameObject>("Combined Items/" + CombinationItem));
            print(combinedItem.name + " Was spawnd");

            m_InventoryManager.m_PreviouslySelectedSlot.GetComponent<SlotManager>().ClearSlot();
            ClearSlot();
            combinedItem.GetComponent<PickUpItem>().Interact(null);
        }
    }

    public void ClearSlot()
    {
        AmountOfUsage--;
        if(AmountOfUsage == 0)
        {
            ItemProperty = SlotManager.Property.empty;
            m_displayImage = string.Empty;
            CombinationItem = string.Empty;
            IsEmpty = true;
            transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/AllLevels/Inventory/Empty_Item");
        }
    }

    public string GetItemName()
    {
        return m_SlotItemImage.GetComponent<Image>().sprite.name;
    }

    public void ResetSlot()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/AllLevels/Inventory/Empty_Item");
        IsEmpty = true;
        CombinationItem = string.Empty;
        ItemProperty = SlotManager.Property.empty;
        AmountOfUsage = 0;
        m_displayImage = string.Empty;
    }

    public void SetSlotData(SlotTempData i_SlotData)
    {
        m_SlotItemImage.GetComponent<Image>().sprite = i_SlotData.SlotImageSprite;
        IsEmpty = false;
        CombinationItem = i_SlotData.CombinationItem;
        ItemProperty = i_SlotData.Property;
        AmountOfUsage = i_SlotData.AmountOfUsage;
        m_displayImage = i_SlotData.DisplayImageName;
    }
}
