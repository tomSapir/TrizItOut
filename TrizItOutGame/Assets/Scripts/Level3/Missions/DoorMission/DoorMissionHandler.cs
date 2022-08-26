using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMissionHandler : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI m_ScreenText;

    private string m_CorrectCode = "7135";
    private string m_CurrentCode = null;
    private int m_CurrentIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
  
    }

    public void OnClickNumberBtn(char i_Number)
    {
        SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
        if (m_CurrentIndex < 4)
        {
            m_CurrentIndex++;
            m_CurrentCode = m_CurrentCode + i_Number;
            m_ScreenText.text = m_CurrentCode;
        }

        if (m_CurrentIndex == 4)
        {
            if (m_CurrentCode == m_CorrectCode)
            {
                Debug.Log("Correct PASS!!!");
            }
            else
            {
                Debug.Log("Wrong PASS!!!");
                m_ScreenText.text = string.Empty;
                m_CurrentIndex = 0;
                m_CurrentCode = string.Empty;
            }
        }
    }

}
