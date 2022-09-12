using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                if (name == m_UnlockItem2 && !FanRazersManager.m_NeedToSpin) // Coin
                {
                    StartCoroutine(ScrewPickedUpEnumerator());
                    m_Inventory.GetComponent<InventoryManager>().RemoveFromInventory("Note");
                }
                else if (name == m_UnlockItem2 && FanRazersManager.m_NeedToSpin)
                {
                    GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel2>().ShowMsg("You can't remove the screws while the fan is spinning, too dangerous.");
                }
                else
                {
                    StartCoroutine(ScrewPickedUpEnumerator());
                }
            }
            else
            {
                findCommunicationManagerAndShowMsg("The screw cannot be removed with this object..");
            }
        }
        else
        {
            findCommunicationManagerAndShowMsg("An object is required for this screw..");
        }
    }

    IEnumerator ScrewPickedUpEnumerator()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(SoundManager.k_ScrewOpenSoundName);
        m_Animation.Play("Screw");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        m_Inventory.GetComponent<InventoryManager>().m_CurrentSelectedSlot.GetComponent<SlotManager>().ClearSlot();
        ScrewRemovedHandler?.Invoke();
    }

    private void findCommunicationManagerAndShowMsg(string i_Msg)
    {
        if (SceneManager.GetActiveScene().name == "Level1_Scene")
        {
            GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel1>().ShowMsg(i_Msg);
        }
        else if (SceneManager.GetActiveScene().name == "Level2_Scene")
        {
            GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel2>().ShowMsg(i_Msg);
        }
        else if (SceneManager.GetActiveScene().name == "Level3_Scene")
        {
            GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel3>().ShowMsg(i_Msg);
        }
    }
}