using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Slots = new GameObject[6]; 
    public GameObject m_currentSelectedSlot { get; set; }
    public GameObject m_previouslySelectedSlot { get; set; }

    private const string k_EmptyItemName = "Empty_Item";
    private const string k_InventoryItemSpritePath = "Sprites/AllLevels/Items/";

    private void Start()
    {
        InitializeInventory();
    }

    private void Update()
    {
        SelectedSlot();
    }

    void InitializeInventory()
    {
        foreach(GameObject slot in m_Slots)
        {
            slot.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/AllLevels/Inventory/" + k_EmptyItemName);
        }
    }

    private void SelectedSlot()
    {
        foreach(GameObject slot in m_Slots)
        {
            if(slot == m_currentSelectedSlot && slot.GetComponent<SlotManager>().ItemProperty == SlotManager.Property.usable && slot.GetComponent<SlotManager>().IsEmpty == false)
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
        Debug.Log("inside");
        SoundManager.PlaySound(SoundManager.k_TakeItemSoundName);
        if (i_Item.m_AmountOfUsage != 0)
        {
            foreach (GameObject slot in m_Slots)
            {
                if (slot.transform.GetChild(0).GetComponent<Image>().sprite.name == k_EmptyItemName)
                {
                    slot.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(k_InventoryItemSpritePath + i_Item.m_DisplaySprite);
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
                slot.GetComponent<SlotManager>().m_AmountOfUsage = 1;
                slot.GetComponent<SlotManager>().ClearSlot();
            }
        }

        return res;
    }
}
