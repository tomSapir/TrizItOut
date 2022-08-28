using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI m_WhereToWrite;
    private string m_TextToWrite;
    private float m_TimePerCharacter;
    private float m_Timer;
    private int m_CharacterIndex;

    public void AddWriter(TextMeshProUGUI i_WhereToWrite, string i_Text, float i_TimePerCharacter)
    {
        m_WhereToWrite = i_WhereToWrite;
        m_TextToWrite = i_Text;
        m_TimePerCharacter = i_TimePerCharacter;
        m_CharacterIndex = 0;
    }

    void Update()
    {
        if (m_WhereToWrite != null && m_CharacterIndex < m_TextToWrite.Length)
        {
            m_Timer -= Time.deltaTime;
            if(m_Timer <= 0f)
            {
                m_Timer += m_TimePerCharacter;
                m_CharacterIndex++;
                m_WhereToWrite.text = m_TextToWrite.Substring(0, m_CharacterIndex);
            }
        }
    }
}
