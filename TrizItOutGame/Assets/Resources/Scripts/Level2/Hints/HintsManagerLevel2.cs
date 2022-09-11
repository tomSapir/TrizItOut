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
    public Text m_HintWindowTitleText;
    public Text m_HintWindowDescriptionText;
    public GameObject m_Inventory;

    public Sprite m_ShowHintBtnNormalSprite;
    public Sprite m_ShowHintBtnHighlightSprite;

    private InventoryManager m_InventoryManager;
    private MainCameraManagerLevel2 m_MainCameraManager;
    private FanMissionHandler m_FanMissionHandler;

    private Dictionary<string, string> m_Hints = new Dictionary<string, string>();

    void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
        m_InventoryManager = m_Inventory.GetComponent<InventoryManager>();
        m_MainCameraManager = GameObject.Find("Main Camera").GetComponent<MainCameraManagerLevel2>();
        m_FanMissionHandler = GameObject.Find("Fan_Mission").GetComponent<FanMissionHandler>();
        fillHintsData();
    }

    private void fillHintsData()
    {
        m_Hints.Add("Reduction",
            string.Format("Try to remove objects if it turns out that they are not required"));
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

            changeShowHintBtnState(1f, true, m_ShowHintBtnHighlightSprite);
        }
        else
        {
            m_Played = false;
            changeShowHintBtnState(0.2f, false, m_ShowHintBtnNormalSprite);
        }
    }

    private void changeShowHintBtnState(float i_Transperency, bool i_Interactble, Sprite i_Sprite)
    {
        Color currentBtnColor = m_ShowHintBtn.GetComponent<Image>().color;
        m_ShowHintBtn.GetComponent<Image>().color = new Color(currentBtnColor.r, currentBtnColor.g, currentBtnColor.b, i_Transperency);
        m_ShowHintBtn.GetComponent<Button>().interactable = i_Interactble;
        m_ShowHintBtn.GetComponent<Image>().sprite = i_Sprite;
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
