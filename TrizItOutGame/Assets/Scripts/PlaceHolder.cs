using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceHolder : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject m_myPrefab;
    [SerializeField]
    private string m_placeFor;
    [SerializeField]
    private string m_activeBy;
    private GameObject m_inventory;
    [SerializeField]
    private string m_whereToSpawn;
    [SerializeField]
    private bool m_activatedByEvent;
      

    // Start is called before the first frame update
    void Start()
    {
        m_inventory = GameObject.Find("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        if(m_activatedByEvent == true)
        {
           GameObject PlaceFor = GameObject.Find(m_placeFor);

            if (PlaceFor != null)
            {
                gameObject.layer = 2;
            }
            else
            {
                gameObject.layer = 0;
            }
        }
 
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if (m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot != null &&
           m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_activeBy)
        {
            var newCable = Instantiate(m_myPrefab, GameObject.Find(m_whereToSpawn).transform);
            newCable.name = m_myPrefab.name;
            m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
            m_inventory.GetComponent<InventoryManager>().m_currentSelectedSlot = null;
        }
    }
}
