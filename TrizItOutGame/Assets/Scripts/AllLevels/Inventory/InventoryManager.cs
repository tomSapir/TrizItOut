using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] m_Slots = new GameObject[6]; 
    public GameObject m_CurrentSelectedSlot { get; set; }
    public GameObject m_PreviouslySelectedSlot { get; set; }

    private Sprite m_EmptyItemSprite = null;

    public static readonly string sr_EmptyItemName = "Empty_Item";
    public static readonly string sr_InventoryItemSpritePath = "Sprites/AllLevels/Items/";
    public static readonly string sr_EmptyItemSpritePath = "Sprites/AllLevels/Inventory/";
    private void Start()
    {
        m_EmptyItemSprite = Resources.Load<Sprite>(sr_EmptyItemSpritePath + sr_EmptyItemName);
        initializeInventory();
    }

    private void Update()
    {
        SelectedSlot();
    }

    private void initializeInventory()
    {
        foreach(GameObject slot in m_Slots)
        {
            slot.transform.GetChild(0).GetComponent<Image>().sprite = m_EmptyItemSprite;
        }
    }

    private void SelectedSlot()
    {
        foreach(GameObject slot in m_Slots)
        {
            if(slot == m_CurrentSelectedSlot && slot.GetComponent<SlotManager>().ItemProperty == SlotManager.Property.usable && slot.GetComponent<SlotManager>().IsEmpty == false)
            {
                slot.GetComponent<Image>().color = new Color(0, .55f, .75f, 1);
            }
            else
            {
                slot.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void AddItemToInventory(PickUpItem i_Item)
    {
        SoundManager.PlaySound(SoundManager.k_TakeItemSoundName);
        if (i_Item.m_AmountOfUsage != 0)
        {
            foreach (GameObject slot in m_Slots)
            {
                if(slot.GetComponent<SlotManager>().IsEmpty)
                {
                    slot.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(sr_InventoryItemSpritePath + i_Item.m_DisplaySprite);
                    slot.GetComponent<SlotManager>().IsEmpty = false;
                    slot.GetComponent<SlotManager>().AssignPtoperty((int)i_Item.m_ItemProperty, i_Item.m_DisplayImage, i_Item.m_ResultOfCombinationItemName, i_Item.m_AmountOfUsage);
                    Destroy(i_Item.gameObject);
                    break;
                }
            }
        }

        Destroy(i_Item.gameObject);
    }
    
    public bool DoesItemInInventory(string i_ItemName)
    {
        foreach(GameObject slot in m_Slots)
        {
            if(slot.GetComponent<SlotManager>().GetItemName() == i_ItemName)
            {
                return true;
            }
        }

        return false;
    }

    public bool RemoveFromInventory(string i_ItemName)
    {
        bool res = false;
        foreach (GameObject slot in m_Slots)
        {
            if (slot.GetComponent<SlotManager>().GetItemName() == i_ItemName)
            {
                res = true;
                slot.GetComponent<SlotManager>().AmountOfUsage = 1;
                slot.GetComponent<SlotManager>().ClearSlot();
            }
        }

        return res;
    }
}
