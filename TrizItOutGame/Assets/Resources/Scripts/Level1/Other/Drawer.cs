using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour, IInteractable
{
    public string m_UnlockItem;
    private GameObject m_Inventory;

    void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(m_Inventory.GetComponent<InventoryManager>().CurrentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            m_Inventory.GetComponent<InventoryManager>().CurrentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        }
    }
}
