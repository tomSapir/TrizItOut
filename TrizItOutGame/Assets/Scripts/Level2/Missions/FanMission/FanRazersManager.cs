using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanRazersManager : MonoBehaviour, IInteractable
{
    public delegate void FanStoppedDelegate();
    public delegate void RazersClickedAndStillSpinningDelegate();

    public static bool m_NeedToSpin = true;
    private GameObject m_Inventory;
    public GameObject m_PaperClip;
    public GameObject m_Note;
    private string m_UnlockItem = "Box_Of_PaperClips";

    public event FanStoppedDelegate FanStopped;
    public event RazersClickedAndStillSpinningDelegate RazersClickedAndStillSpinning;

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
            transform.Rotate(0f, 0f, -400 * Time.deltaTime, Space.Self);
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
        else
        {
            RazersClickedAndStillSpinning?.Invoke();
        }
    }

    private void handleFanStopped(InventoryManager i_InventoryManager)
    {
        SoundManager.StopSound();
        m_NeedToSpin = false;
        gameObject.layer = 2;
        i_InventoryManager.m_currentSelectedSlot = null;
        FanStopped?.Invoke();
    }
}
