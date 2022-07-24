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
            slot.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Inventory/empty_item");
        }
    }

    private void SelectedSlot()
    {
        foreach(GameObject slot in m_Slots)
        {
            if(slot == m_currentSelectedSlot && slot.GetComponent<SlotManager>().ItemProperty == SlotManager.Property.usable && slot.GetComponent<SlotManager>().IsEmpty == false)
            {
                slot.GetComponent<Image>().color = new Color(0, .55f, .75f, 0.5f);
            }
            else
            {
                slot.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            
        }
    }

    public void AddItemToTheInventory(InventoryItemManager i_InventoryItemManager)
    {
        foreach(GameObject slot in m_Slots)
        {
            if(slot.GetComponent<SlotManager>().IsEmpty == true)
            {
                slot.GetComponent<SlotManager>().InventoryItemManager = i_InventoryItemManager;
            }
        }
    }
}
