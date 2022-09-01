using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dust : MonoBehaviour, IInteractable
{
    public delegate void CleanDustAction();

    private string m_UnlockItem = "Spray_With_Straw";
    private GameObject m_inventory;

    public event CleanDustAction OnCleanUp;


    void Start()
    {
        m_inventory = GameObject.Find("Inventory");
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        // suppose we have a key that unlocks this drawer, the sprites name of the kwy would be m_unlocks.
        if (m_inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            SoundManager.PlaySound(SoundManager.k_SpraySoundName);
            Destroy(gameObject);
            OnCleanUp?.Invoke();
            m_inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        }
    }
}