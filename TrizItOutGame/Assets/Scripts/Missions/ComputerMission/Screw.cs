using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screw : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string m_UnlockItem;
    private string m_UnlockItem2 = "trizCoin";
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
        string name = m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;

        if (name == m_UnlockItem || name == m_UnlockItem2)
        {
            Destroy(gameObject);
            m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        }

    }
}