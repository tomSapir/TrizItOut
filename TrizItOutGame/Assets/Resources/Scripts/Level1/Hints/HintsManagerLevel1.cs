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
    public Text m_HintWindowTitleText;
    public Text m_HintWindowDescriptionText;
    public GameObject m_Inventory;

    public Sprite m_ShowHintBtnNormalSprite;
    public Sprite m_ShowHintBtnHighlightSprite;

    public GameObject m_Kattle;
    private Dictionary<string, string> m_Hints = new Dictionary<string, string>();
    public static bool m_IsLoadingNextLevel = false;

    void Start()
    {
        fillHintsData();

        GameObject.Find("Next_Level_Loader").GetComponent<NextLevelLoader>().LoadingNextLevel += OnLoadingNextLevel;
    }

    private void OnLoadingNextLevel(int i_Level)
    {
        Debug.Log("On LoadingNextLevel");
        m_IsLoadingNextLevel = true;
    }

    private void fillHintsData()
    {
        m_Hints.Add("Merging", 
            string.Format("Make two diffrent objects work together to get a diffrent functionality.", 
            Environment.NewLine));
        m_Hints.Add("Universality", 
            string.Format("Make a part or object perform multiple functions.", 
            Environment.NewLine));
        m_Hints.Add("Segmentation",
           string.Format("Divide an object into independent parts.",
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
        else if((computerCableIsConnected || kattleCableIsConnected) && !m_IsLoadingNextLevel)
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

        if (m_CurrentHintKey != null)
        {
            if(!m_Played)
            {
                GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(SoundManager.k_HintSoundName);
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
        if(m_CurrentHintKey != null)
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


