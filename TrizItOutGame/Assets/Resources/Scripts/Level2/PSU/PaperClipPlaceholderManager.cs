using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperClipPlaceholderManager : MonoBehaviour, IInteractable
{
    public delegate void PaperClipPressedDelegate(int i_Id);

    public int m_Id;
    public CommunicationManagerLevel2 m_CommunicationManagerLevel2;
    public event PaperClipPressedDelegate PaperClipPressed;
    private string m_UnlockItem = "Box_Of_PaperClips";
    private GameObject m_Inventory;
    private SpriteRenderer m_ChildSpriteRenderer;
    private static bool m_PaperClipConnectedOnce = false;

    public void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
        if(m_Inventory == null)
        {
            Debug.LogError("m_Inventory is null.");
        }

        m_ChildSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (m_ChildSpriteRenderer == null)
        {
            Debug.LogError("m_ChildSpriteRenderer is null.");
        }
        m_ChildSpriteRenderer.enabled = false;

        m_CommunicationManagerLevel2 = GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel2>();
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        InventoryManager inventoryManager = m_Inventory.GetComponent<InventoryManager>();
        GameObject currSelectedSlot = inventoryManager.m_CurrentSelectedSlot;

        if (m_ChildSpriteRenderer.enabled == true)
        {
            togglePaperClipSpriteAndNotify();
        }

        if (currSelectedSlot != null &&
            currSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            inventoryManager.m_CurrentSelectedSlot = null;
            togglePaperClipSpriteAndNotify();
            m_PaperClipConnectedOnce = true;
        }

        if(!m_PaperClipConnectedOnce)
        {
            m_CommunicationManagerLevel2.ShowMsg("You need something to conduct electricity.");
        }
   
    }

    private void togglePaperClipSpriteAndNotify()
    {
        m_ChildSpriteRenderer.enabled = !m_ChildSpriteRenderer.enabled;
        PaperClipPressed?.Invoke(m_Id);
    }
}
