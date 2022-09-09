using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceHolder : MonoBehaviour, IInteractable
{
    public delegate void PrefabSpawnedAction();

    [SerializeField]
    private GameObject m_MyPrefab; //the prefab we Instantiate
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

    public event PrefabSpawnedAction OnPrefabSpawned;

    void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
    }

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
        if (m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot != null &&
           m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_ActiveBy)
        {
            GameObject newCable = Instantiate(m_MyPrefab, GameObject.Find(m_WhereToSpawn).transform);

            newCable.name = m_MyPrefab.name;

            newCable.GetComponent<SpriteRenderer>().sortingOrder = 2;
            newCable.layer = 0;
            OnPrefabSpawned?.Invoke();


            m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
            m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot = null;

            if(m_IsReusable == false)
            {
                newCable.layer = 2;
            }

            if(m_PlaceFor == "Power_Fuze")
            {
                newCable.transform.position = new Vector3(newCable.transform.position.x + 0.69f, newCable.transform.position.y - 0.06f, newCable.transform.position.z);
                newCable.transform.localScale = new Vector3(0.028f, 0.029f, 5.79f);
            }
        }
    }
}
