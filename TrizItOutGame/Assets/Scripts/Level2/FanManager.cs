using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanManager : MonoBehaviour, IInteractable
{
    private GameObject m_Inventory;

    void Start()
    {
        m_Inventory = GameObject.Find("/Canvas/Inventory");
        if (m_Inventory == null)
        {
            Debug.LogError("m_Inventory is null.");
        }
    }


    void Update()
    {
        
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        InventoryManager inventoryManager = m_Inventory.GetComponent<InventoryManager>();
        GameObject currSelectedSlot = inventoryManager.m_currentSelectedSlot;

        if (currSelectedSlot != null &&
            currSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == "TwoScrews")
        {
            // move fan to the right position
            gameObject.transform.position = new Vector3(-0.029f, 2.096f, 0);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            // TODO: put screws

            // clear slot
            currSelectedSlot.GetComponent<SlotManager>().ClearSlot();
            inventoryManager.m_currentSelectedSlot = null;

        }
    }
}
