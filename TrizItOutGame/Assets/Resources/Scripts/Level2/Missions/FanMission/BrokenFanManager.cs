using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenFanManager : MonoBehaviour, IInteractable
{
    public delegate void BrokenFanClickedWithoutScrewsDelegate();
    public delegate void BrokenFanSolvedDelegate();

    private GameObject m_Inventory;
    public GameObject m_Screw1, m_Screw2;
    private bool m_IsHanged = false;

    public event BrokenFanClickedWithoutScrewsDelegate BrokenFanClickedWithoutScrews;
    public event BrokenFanSolvedDelegate BrokenFanSolved;

    void Start()
    {
        m_Inventory = GameObject.Find("/Canvas/Inventory");
        if (m_Inventory == null)
        {
            Debug.LogError("m_Inventory is null.");
        }
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        InventoryManager inventoryManager = m_Inventory.GetComponent<InventoryManager>();
        GameObject currSelectedSlot = inventoryManager.CurrentSelectedSlot;

        if (currSelectedSlot != null &&
            currSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == "Two_Screws")
        {
            m_IsHanged = true;
            gameObject.transform.position = new Vector3(-0.029f, 2.096f, 0);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            m_Screw1.GetComponent<SpriteRenderer>().enabled = true;
            m_Screw2.GetComponent<SpriteRenderer>().enabled = true;
            currSelectedSlot.GetComponent<SlotManager>().ClearSlot();
            inventoryManager.CurrentSelectedSlot = null;
            BrokenFanSolved?.Invoke();
        }
        else
        {
            if(!m_IsHanged)
            {
                BrokenFanClickedWithoutScrews?.Invoke();
            }
        }
    }
}
