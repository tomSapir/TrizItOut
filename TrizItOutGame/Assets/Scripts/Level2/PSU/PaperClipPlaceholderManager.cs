using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperClipPlaceholderManager : MonoBehaviour, IInteractable
{
    public delegate void PaperClipPressedDelegate(int i_Id);

    public int m_Id;

    public event PaperClipPressedDelegate PaperClipPressed;

    private string m_UnlockItem = "Box_Of_PaperClips";
    private GameObject m_Inventory;
    private SpriteRenderer m_ChildSpriteRenderer;

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
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        InventoryManager inventoryManager = m_Inventory.GetComponent<InventoryManager>();
        GameObject currSelectedSlot = inventoryManager.m_currentSelectedSlot;

        if(m_ChildSpriteRenderer.enabled == true)
        {
            togglePaperClipSpriteAndNotify();
        }

        if (currSelectedSlot != null &&
            currSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == m_UnlockItem)
        {
            inventoryManager.m_currentSelectedSlot = null;
            togglePaperClipSpriteAndNotify();
        }
    }

    private void togglePaperClipSpriteAndNotify()
    {
        m_ChildSpriteRenderer.enabled = !m_ChildSpriteRenderer.enabled;

        if (PaperClipPressed != null)
        {
            PaperClipPressed(m_Id);
        }
    }
}
