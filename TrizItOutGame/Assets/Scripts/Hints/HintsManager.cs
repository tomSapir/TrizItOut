using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HintWindow;
    [SerializeField]
    private GameObject m_ShowHintBtn;
    [SerializeField]
    private GameObject m_HintWindowText;
    [SerializeField]
    private GameObject m_Inventory;

    void Start()
    {

    }

    public void OnClickShowHintBtn()
    {
        findHint();


    }

    private void findHint()
    {
        InventoryManager inventoryManager = m_Inventory.GetComponent<InventoryManager>();

        bool sprayInInventory = inventoryManager.DoesItemInInventory("Spray");
        bool strawInIventory = inventoryManager.DoesItemInInventory("Straw");
    
        if ((sprayInInventory && !strawInIventory) || (!sprayInInventory && strawInIventory))
        {
            m_ShowHintBtn.SetActive(false);
            m_HintWindow.SetActive(true);
            m_HintWindowText.SetActive(true);

            m_HintWindowText.GetComponent<TextMeshProUGUI>().text = "Triz Tip: Merge";
        }
    }

    public void OnClickCloseHintWindow()
    {
        m_HintWindow.SetActive(false);
        m_ShowHintBtn.SetActive(true);
    }

}


/*
 * HINTS:
 *  
 * 1. hint:  
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */