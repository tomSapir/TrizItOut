using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceHolder : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject m_MyPrefab;
    [SerializeField]
    private string m_PlaceFor;
    [SerializeField]
    private string m_ActiveBy;
    private GameObject m_Inventory;
    [SerializeField]
    private string m_WhereToSpawn;
    [SerializeField]
    private bool m_ActivatedByEvent;
    [SerializeField]
    private bool m_IsReusable;
      

    // Start is called before the first frame update
    void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ActivatedByEvent == true)
        {
           GameObject PlaceFor = GameObject.Find(m_PlaceFor);

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
        if (m_Inventory.GetComponent<InventoryManager>().m_currentSelectedSlot != null &&
           m_Inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_ActiveBy)
        {
            var newCable = Instantiate(m_MyPrefab, GameObject.Find(m_WhereToSpawn).transform);
            newCable.name = m_MyPrefab.name;
            
            
            m_Inventory.GetComponent<InventoryManager>().m_currentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
            m_Inventory.GetComponent<InventoryManager>().m_currentSelectedSlot = null;

            if(m_IsReusable == false)
            {
                newCable.layer = 2;
            }
        }
    }
}
