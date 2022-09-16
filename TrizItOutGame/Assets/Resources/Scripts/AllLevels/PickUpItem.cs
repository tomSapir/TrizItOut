using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour, IInteractable
{
    public delegate void PickUpAction();
    public enum eProperty { usable, displayable };

    public string m_ItemName;
    public string m_DisplaySpriteName;    
    public eProperty m_ItemProperty;
    public string m_ExtraDisplaySpriteName; 
    public string m_ResultOfCombinationItemName;
    public int m_AmountOfUsage; 

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
        m_InventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        m_InventoryManager.AddItemToInventory(this);
        OnPickUp?.Invoke();
    }

}
