using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour, IInteractable
{
    public delegate void PickUpAction();
    public enum eProperty { usable, displayable };

    [SerializeField]
    public string m_DisplaySprite;    // We need to know which sprite it should active with (On Inventory)
    [SerializeField]
    public eProperty m_ItemProperty;
    [SerializeField]
    public string m_DisplayImage; // The more informative image of an object (In case property is displyable)
    [SerializeField]
    public string m_ResultOfCombinationItemName; // is needs to combine with another item to perform an action - like the spray and the straw.
    [SerializeField]
    public int m_AmountOfUsage; // will be transferd to the slot when get picked up to know when it should be ot of the inventory.

    public event PickUpAction OnPickUp;

    private InventoryManager m_InventoryManager;

    void Start()
    {
        m_InventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        itemPickUp();
    }

    private void itemPickUp()
    {
        Debug.Log("Item picked up: " + m_DisplaySprite);
        //Debug.Break();
        m_InventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        m_InventoryManager.AddItemToInventory(this);
        OnPickUp?.Invoke();
    }

}
