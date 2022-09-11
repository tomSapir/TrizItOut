using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperClipPlaceholderManager : MonoBehaviour, IInteractable
{
    public delegate void PaperClipPressedDelegate(int i_Id);

    public int m_Id;
    public CommunicationManagerLevel2 m_CommunicationManagerLevel2;
    private string m_UnlockItem = "Box_Of_PaperClips";
    private GameObject m_Inventory;
    private SpriteRenderer m_ChildSpriteRenderer;
    public GameObject m_Lightning;

    private static bool m_PaperClipConnectedOnce = false;

    public event PaperClipPressedDelegate PaperClipPressed;

    public void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
        m_ChildSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
        m_Lightning.SetActive(m_ChildSpriteRenderer.enabled);
        
        PaperClipPressed?.Invoke(m_Id);
    }
}
