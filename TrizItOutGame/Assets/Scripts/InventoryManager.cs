using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Slots = new GameObject[6];
   
    public void AddItemToTheInventory(InventoryItem i_InventoryItem)
    {
        for (int i = 0; i < m_Slots.Length; i++)
        {
            if (m_Slots[i].GetComponent<SlotManager>().IsEmpty == true)
            {
                m_Slots[i].GetComponent<SlotManager>().InventoryItem = i_InventoryItem;
            }
        }
    }

}
