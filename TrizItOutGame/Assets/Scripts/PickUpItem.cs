using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string m_DisplaySprite; // We need to know wich sprite it should active with.

    private GameObject m_InventorySlots;

   
    void Start()
    {
        m_InventorySlots = GameObject.Find("Items_Parent"); // To loop up for the available slot.
    }

 
    void Update()
    {
        
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        ItemPickUp();
    }

    private void ItemPickUp()
    {
        Debug.Log("Got into Pick Up Item function. Item name: " + m_DisplaySprite);
        foreach(Transform slot in m_InventorySlots.transform)
        {
            if(slot.transform.GetChild(0).GetComponent<Image>().sprite.name == "empty_item")
            {
                slot.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Inventory/" + m_DisplaySprite);
                slot.GetComponent<SlotManager>().IsEmpty = false;
                Destroy(gameObject);
                break;
            }
        }
    }
}
