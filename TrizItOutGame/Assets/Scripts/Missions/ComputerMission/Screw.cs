using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screw : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string m_UnlockItem;
    private string m_UnlockItem2 = "trizCoin";
    private GameObject m_Inventory;

    // Start is called before the first frame update
    void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        string name = m_Inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;

        if (name == m_UnlockItem || name == m_UnlockItem2)
        {
            if(name == m_UnlockItem2)
            {
                m_Inventory.GetComponent<InventoryManager>().RemoveFromInventory("Note");
            }
            SoundManager.PlaySound(SoundManager.k_ScrewOpenSoundName);
            Destroy(gameObject);
            m_Inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        }

    }
}