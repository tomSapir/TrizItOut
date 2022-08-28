using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screw : MonoBehaviour, IInteractable
{
    public delegate void ScrewRemovedDelegate();

    [SerializeField]
    private string m_UnlockItem;
    private string m_UnlockItem2 = "trizCoin";
    private GameObject m_Inventory;
    private Animation m_Animation;
    public event ScrewRemovedDelegate ScrewRemovedHandler;

    void Start()
    {
        m_Animation = gameObject.GetComponent<Animation>();
        m_Inventory = GameObject.Find("Inventory");
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if (m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot != null)
        {
            string name = m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;

            if (name == m_UnlockItem || name == m_UnlockItem2)
            {
                if (name == m_UnlockItem2)
                {
                    m_Inventory.GetComponent<InventoryManager>().RemoveFromInventory("Note");
                }

                StartCoroutine(ScrewPickedUpEnumerator());
            }
        }
    }

    IEnumerator ScrewPickedUpEnumerator()
    {
        SoundManager.PlaySound(SoundManager.k_ScrewOpenSoundName);
        m_Animation.Play("Screw");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        ScrewRemovedHandler?.Invoke();
    }
}