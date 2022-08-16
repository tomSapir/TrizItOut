using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanRazersManager : MonoBehaviour, IInteractable
{
    public delegate void FanStoppedDelegate();
    
    private bool m_NeedToSpin = true;
    private GameObject m_Inventory;

    [SerializeField]
    private GameObject m_PaperClip;
    [SerializeField]
    private GameObject m_Note;

    private string m_UnlockItem = "Box_Of_PaperClips";

    public event FanStoppedDelegate FanStopped;

    void Start()
    {
        m_Inventory = GameObject.Find("/Canvas/Inventory");
        if(m_Inventory == null)
        {
            Debug.LogError("m_Inventory is null.");
        }
    }


    void Update()
    {
        manageRazersRotation();
    }

    private void manageRazersRotation()
    {
        if(m_NeedToSpin)
        {
            transform.Rotate(0f, 0f, -250 * Time.deltaTime, Space.Self);
        }
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        InventoryManager inventoryManager = m_Inventory.GetComponent<InventoryManager>();
        GameObject currSelectedSlot = inventoryManager.m_currentSelectedSlot;
 
        if (currSelectedSlot != null &&
            currSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            handleFanStopped(inventoryManager);
        }
    }

    private void handleFanStopped(InventoryManager i_InventoryManager)
    {
        m_NeedToSpin = false;
        m_PaperClip.SetActive(true);
        m_Note.layer = 0;
        //currSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        i_InventoryManager.m_currentSelectedSlot = null;

        if(FanStopped != null)
        {
            FanStopped();
        }
    }
}
