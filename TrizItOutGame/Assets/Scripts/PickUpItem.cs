using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour, IInteractable
{
    private enum property { usable, displayable };

    [SerializeField]
    private string m_DisplaySprite; // We need to know wich sprite it should active with.  
    [SerializeField]
    private property m_itemProperty;
    [SerializeField]
    private string m_DisplayImage; // The more informative image of an object.
    [SerializeField]
    private string m_combinationItem; // is needs to combine with another item to perform an action - like the spray and the straw.

    private GameObject m_InventorySlots;
    void Start()
    {
        m_InventorySlots = GameObject.Find("Items_Parent"); // To loop up for the available slot.
    }

 
    void Update()
    {
        
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        ItemPickUp();
    }

    private void ItemPickUp()
    {
        m_InventorySlots = GameObject.Find("Items_Parent"); // To loop up for the available slot.

        foreach (Transform slot in m_InventorySlots.transform)
        {
            if(slot.transform.GetChild(0).GetComponent<Image>().sprite.name == "empty_item")
            {
                slot.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Inventory/" + m_DisplaySprite);
                slot.GetComponent<SlotManager>().IsEmpty = false;
                slot.GetComponent<SlotManager>().AssignPtoperty((int)m_itemProperty, m_DisplayImage, m_combinationItem);
                Destroy(gameObject);
                break;
            }
        }
    }

}
