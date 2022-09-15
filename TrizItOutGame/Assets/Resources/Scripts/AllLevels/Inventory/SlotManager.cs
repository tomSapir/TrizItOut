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
    public string m_DisplayImage; 

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
                m_ZoomInWindow.transform.Find("Item").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level1/Displayables/" + m_DisplayImage);
            }
            m_ZoomInWindow.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.gameObject == m_InventoryManager.CurrentSelectedSlot)
        {
            m_InventoryManager.CurrentSelectedSlot = null;
        }
        else if(IsEmpty == false)
        {
           m_InventoryManager.PreviouslySelectedSlot = m_InventoryManager.CurrentSelectedSlot;
           m_InventoryManager.CurrentSelectedSlot = this.gameObject;
           Combine();
        }
    }

    public void AssignPtoperty(int i_ordrNumber, string i_displayImage, string i_combinationItem, int i_amountOfUsage)
    {
        ItemProperty = (Property)i_ordrNumber;
        this.m_DisplayImage = i_displayImage;
        this.CombinationItem = i_combinationItem;
        this.AmountOfUsage = i_amountOfUsage;
    }

    public void Combine()
    {
        if(m_InventoryManager.PreviouslySelectedSlot != null &&
            m_InventoryManager.PreviouslySelectedSlot.GetComponent<SlotManager>().CombinationItem == this.gameObject.GetComponent<SlotManager>().CombinationItem 
            && this.gameObject.GetComponent<SlotManager>().CombinationItem != string.Empty)
        {
            var combinedItem = Instantiate(Resources.Load<GameObject>("Combined Items/" + CombinationItem));
            print(combinedItem.name + " Was spawnd");

            m_InventoryManager.PreviouslySelectedSlot.GetComponent<SlotManager>().ClearSlot();
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
            m_DisplayImage = string.Empty;
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
        m_DisplayImage = string.Empty;
    }

    public void SetSlotData(SlotTempData i_SlotData)
    {
        m_SlotItemImage.GetComponent<Image>().sprite = i_SlotData.SlotImageSprite;
        IsEmpty = false;
        CombinationItem = i_SlotData.CombinationItem;
        ItemProperty = i_SlotData.Property;
        AmountOfUsage = i_SlotData.AmountOfUsage;
        m_DisplayImage = i_SlotData.DisplayImageName;
    }
}
