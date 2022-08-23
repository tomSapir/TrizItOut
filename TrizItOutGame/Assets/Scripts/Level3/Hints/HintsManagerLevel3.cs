using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintsManagerLevel3 : MonoBehaviour
{
    private string m_CurrentHintKey = null;

    public GameObject m_HintWindow;
    public GameObject m_ShowHintBtn;
    public GameObject m_HintWindowText;
    public GameObject m_Inventory;

    private InventoryManager m_InventoryManager;
    private MainCameraManagerLevel3 m_MainCameraManager;

    private Dictionary<string, string> m_Hints = new Dictionary<string, string>();

    void Start()
    {
        m_InventoryManager = m_Inventory.GetComponent<InventoryManager>();
        m_MainCameraManager = GameObject.Find("Main Camera").GetComponent<MainCameraManagerLevel3>();
        fillHintsData();
    }

    private void fillHintsData()
    {
        // TODO: implement
    }

    // Update is called once per frame
    void Update()
    {
        findHint();
    }

    public void OnClickShowHintBtn()
    {
        showHint();
    }

    private void findHint()
    {
        if (true)
        {

        }
        else
        {
            m_CurrentHintKey = null;
            m_ShowHintBtn.SetActive(true);
        }

        if (m_CurrentHintKey != null)
        {
            m_ShowHintBtn.GetComponent<Image>().color = new Color32(207, 94, 40, 152);
        }
        else
        {
            m_ShowHintBtn.GetComponent<Image>().color = new Color32(56, 56, 56, 152);
        }
    }

    private void showHint()
    {
        if (m_CurrentHintKey != null)
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
