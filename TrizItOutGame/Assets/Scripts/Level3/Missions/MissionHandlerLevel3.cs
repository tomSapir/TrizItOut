using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandlerLevel3 : MonoBehaviour
{
    private InventoryManager m_InventoryManager;

    void Start()
    {
        m_InventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

}
