using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintsManagerLevel3 : MonoBehaviour
{
    private string m_CurrentHintKey = null;
    private bool m_Played = false;

    public GameObject m_HintWindow;
    public GameObject m_ShowHintBtn;
    public Text m_HintWindowTitleText;
    public Text m_HintWindowDescriptionText;
    public GameObject m_Inventory;

    public Sprite m_ShowHintBtnNormalSprite;
    public Sprite m_ShowHintBtnHighlightSprite;

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
            if (!m_Played)
            {
                SoundManager.PlaySound(SoundManager.k_HintSoundName);
                m_Played = true;
            }

            m_ShowHintBtn.GetComponent<Button>().interactable = true;
            m_ShowHintBtn.GetComponent<Image>().sprite = m_ShowHintBtnHighlightSprite;
        }
        else
        {
            m_Played = false;
            m_ShowHintBtn.GetComponent<Button>().interactable = false;
            m_ShowHintBtn.GetComponent<Image>().sprite = m_ShowHintBtnNormalSprite;
        }
    }

    private void showHint()
    {
        if (m_CurrentHintKey != null)
        {
            m_ShowHintBtn.SetActive(false);
            m_HintWindow.SetActive(true);
            m_HintWindowTitleText.text = "Triz Tip: " + m_CurrentHintKey;
            m_HintWindowDescriptionText.text = m_Hints[m_CurrentHintKey];
        }
    }

    public void OnClickCloseHintWindow()
    {
        m_HintWindow.SetActive(false);
        m_ShowHintBtn.SetActive(true);
    }
}
