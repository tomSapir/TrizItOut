using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string m_UnlockItem;
    private GameObject m_inventory;

    // Start is called before the first frame update
    void Start()
    {
        m_inventory = GameObject.Find("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        // suppose we have a key that unlocks this drawer, the sprites name of the kwy would be m_unlocks.
       if( m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            Debug.Log("Unlock");
            m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        }
    }
}
