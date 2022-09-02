using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HintsManagerLevel2 : MonoBehaviour
{
    private string m_CurrentHintKey = null;
    private bool m_Played = false;

    public GameObject m_HintWindow;
    public GameObject m_ShowHintBtn;
    public GameObject m_HintWindowText;
    public GameObject m_Inventory;

    private InventoryManager m_InventoryManager;
    private MainCameraManagerLevel2 m_MainCameraManager;
    private FanMissionHandler m_FanMissionHandler;

    private Dictionary<string, string> m_Hints = new Dictionary<string, string>();

    void Start()
    {
        m_InventoryManager = m_Inventory.GetComponent<InventoryManager>();
        m_MainCameraManager = GameObject.Find("Main Camera").GetComponent<MainCameraManagerLevel2>();
        m_FanMissionHandler = GameObject.Find("Fan_Mission").GetComponent<FanMissionHandler>();
        fillHintsData();
    }

    private void fillHintsData()
    {
        m_Hints.Add("Reduction",
            string.Format("Triz Tip:Reduction {0}Try to remove objects if it turns out that they are not required",
            Environment.NewLine));
        m_Hints.Add("Universality",
             string.Format("Maybe the Universality princple is relvent here as well."));
    }

    public void OnClickShowHintBtn()
    {
        showHint();
    }

    private void Update()
    {
        findHint();
    }

    private void findHint()
    {
        bool trizCoinInInventory = m_InventoryManager.DoesItemInInventory("trizCoin");

        if (m_MainCameraManager.CurrentWallIndex == -2 && FanMissionHandler.s_AmountOfScrewsRemoved < 2)
        {
            m_CurrentHintKey = "Reduction";
        }
        else if(trizCoinInInventory && m_MainCameraManager.CurrentWallIndex == 3)
        {
            m_CurrentHintKey = "Universality";
        }
        else
        {
            m_CurrentHintKey = null;
            m_ShowHintBtn.SetActive(true);
        }

        if (m_CurrentHintKey != null)
        {
            if (!m_Played)
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
        if (m_CurrentHintKey != null)
        {
            m_ShowHintBtn.SetActive(false);
            m_HintWindow.SetActive(true);
            m_HintWindowText.SetActive(true);
            m_HintWindowText.GetComponent<Text>().text = m_Hints[m_CurrentHintKey];
        }
    }

    public void OnClickCloseHintWindow()
    {
        m_HintWindow.SetActive(false);
        m_ShowHintBtn.SetActive(true);
    }
}
