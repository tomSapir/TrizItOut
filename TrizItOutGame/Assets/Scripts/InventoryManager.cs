using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Slots = new GameObject[6];

    private void Start()
    {
        InitializeInventory();
    }

    void InitializeInventory()
    {
        var slots = GameObject.Find("Items_Parent");
        
        foreach(Transform slot in slots.transform)
        {
            slot.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Inventory/empty_item");
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
