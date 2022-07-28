using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeBoxBtnManager : MonoBehaviour
{
    [SerializeField]
    private Button[] m_NumButtons;

    [SerializeField]
    private TextMeshProUGUI m_screenText;

    string PassCode = "1234";
    string CurrentPassCode = null;
    private int m_Index = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void onclick(string Number)
    {
        if (m_Index < 4)
        {
            m_Index++;
            CurrentPassCode = CurrentPassCode + Number;
            m_screenText.text = CurrentPassCode;
            Debug.LogError("lfmklf");
        }
    }
        
}
