using UnityEngine;
using UnityEngine.UI;


public class KattleCabelSlot : MonoBehaviour, IInteractable
{
    public string m_UnlockItem;
    private GameObject m_Inventory;
    private GameObject m_Kattle;
    public GameObject m_Cable;

    void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
        m_Kattle = GameObject.Find("Kattle");
    }

    void Update()
    {
        GameObject Cable = GameObject.Find("Furniture_2/Kettle_Cable");
        
        if (Cable != null)
        {
            gameObject.layer = 2;
            if (m_Kattle != null)
            {
                m_Kattle.GetComponent<Kattle>().IsConnected = true;
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
                m_Kattle.GetComponent<Kattle>().IsConnected = false;
            }
            else
            {
                Debug.Log("Kattle is null.");
            }
        }
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if (m_Inventory.GetComponent<InventoryManager>().CurrentSelectedSlot != null &&
            m_Inventory.GetComponent<InventoryManager>().CurrentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            var newCable = Instantiate(m_Cable, GameObject.Find("Furniture_2").transform);
            newCable.name = "Kettle_Cable";
            m_Inventory.GetComponent<InventoryManager>().CurrentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
            m_Inventory.GetComponent<InventoryManager>().CurrentSelectedSlot = null;
        }
    }
}
