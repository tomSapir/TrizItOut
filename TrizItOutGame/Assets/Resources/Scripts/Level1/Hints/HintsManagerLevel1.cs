using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class HintsManagerLevel1 : MonoBehaviour
{
    private string m_CurrentHintKey = null;
    private bool m_Played = false;

    public GameObject m_HintWindow;
    public GameObject m_ShowHintBtn;
    public GameObject m_HintWindowText;
    public GameObject m_Inventory;

    public GameObject m_Kattle;
    private Dictionary<string, string> m_Hints = new Dictionary<string, string>();

    void Start()
    {
        fillHintsData();
    }

    private void fillHintsData()
    {
        m_Hints.Add("Merging", 
            string.Format("Triz Tip: Merging {0}Make two diffrent objects work together to get a diffrent functionality.", 
            Environment.NewLine));
        m_Hints.Add("Universality", 
            string.Format("Triz Tip: Universality {0}Make a part or object perform multiple functions.", 
            Environment.NewLine));
        m_Hints.Add("Segmentation",
           string.Format("Triz Tip: Segmentation {0}Divide an object into independent parts.",
            Environment.NewLine));
    }

    public void OnClickShowHintBtn()
    {
        showHint();
    }

    void Update()
    {
        findHint();
    }

    private void findHint()
    {
        InventoryManager inventoryManager = m_Inventory.GetComponent<InventoryManager>();
        DisplayManagerLevel1 displayManagerLevel1 = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();

        bool sprayInInventory = inventoryManager.DoesItemInInventory("Spray");
        bool strawInIventory = inventoryManager.DoesItemInInventory("Straw");
        bool kattleCableIsConnected = m_Kattle.GetComponent<Kattle>().m_isConnected;
        bool computerCableIsConnected = displayManagerLevel1.m_ComputerCableIsSpawned;
        bool laptopInInventory = inventoryManager.DoesItemInInventory("Laptop");

        if ((sprayInInventory && !strawInIventory) || (!sprayInInventory && strawInIventory))
        {
            m_CurrentHintKey = "Merging";
        }
        else if(computerCableIsConnected || kattleCableIsConnected)
        {
            m_CurrentHintKey = "Universality";
        }
        else if(laptopInInventory)
        {
            m_CurrentHintKey = "Segmentation";
        }
        else
        {
            m_CurrentHintKey = null;
            m_ShowHintBtn.SetActive(true);
        }

        if(m_CurrentHintKey != null)
        {
            if(!m_Played)
            {
                SoundManager.PlaySound(SoundManager.k_HintSound);
                m_Played = true;
            }
            m_ShowHintBtn.GetComponent<Image>().color = new Color32(207, 94, 40, 152);
        }
        else
        {
            m_Played = false;
            m_ShowHintBtn.GetComponent<Image>().color = new Color32(56, 56, 56, 152);
        }
    }

    private void showHint()
    {
        if(m_CurrentHintKey != null)
        {
            m_ShowHintBtn.SetActive(false);
            m_HintWindow.SetActive(true);
            m_HintWindowText.SetActive(true);
            m_HintWindowText.GetComponent<TextMeshProUGUI>().text = m_Hints[m_CurrentHintKey];
        }
    }

    public void OnClickCloseHintWindow()
    {
        m_HintWindow.SetActive(false);
        m_ShowHintBtn.SetActive(true);
    }
}


