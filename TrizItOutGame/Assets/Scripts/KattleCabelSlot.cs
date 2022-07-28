using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KattleCabelSlot : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string m_UnlockItem;
    private GameObject m_inventory;

    private GameObject m_Kattle;
    [SerializeField]
    private GameObject m_Cable;
    // Start is called before the first frame update

    void Start()
    {
        m_inventory = GameObject.Find("Inventory");
        m_Kattle = GameObject.Find("Kattle");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Cable = GameObject.Find("furniture2/Kettle_Cable");
        
        if (Cable != null)
        {
            gameObject.layer = 2;
            if (m_Kattle != null)
            {
                m_Kattle.GetComponent<Kattle>().m_isConnected = true;
            }
            else
            {
                Debug.LogError("Kattle is null!");    
            }


        }
        else
        {
            gameObject.layer = 0;
            if (m_Kattle != null)
            {
                m_Kattle.GetComponent<Kattle>().m_isConnected = false;
            }
            else
                Debug.Log("Kattle is null.");
        }
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if (m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot != null &&
            m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            var newCable = Instantiate(m_Cable, GameObject.Find("furniture2").transform);
            newCable.name = "Kettle_Cable";
            m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
            m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot = null;
        }
    }
}
