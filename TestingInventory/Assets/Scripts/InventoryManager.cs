using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private Button m_OpenInventoryBtn;
    [SerializeField]
    private Button m_CloseInventoryBtn;

    [SerializeField]
    private GameObject m_Inventory;

    // Start is called before the first frame update
    void Start()
    {
        /*
        m_Inventory = GameObject.Find("Inventory");

        if (m_Inventory == null)
        {
            Debug.LogError("Cant find inventory game object.");
        }
        */
    }

    public void OnOpenInventoryBtnClick()
    {
        m_Inventory.SetActive(true);
        m_OpenInventoryBtn.gameObject.SetActive(false);
    }

    public void OnCloseInventoryBtnClick()
    {
        m_Inventory.SetActive(false);
        m_OpenInventoryBtn.gameObject.SetActive(true);
    }
}
