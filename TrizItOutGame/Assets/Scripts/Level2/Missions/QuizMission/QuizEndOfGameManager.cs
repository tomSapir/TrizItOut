using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizEndOfGameManager : MonoBehaviour
{
    private float m_Timer;
    public GameObject m_QuizEndOfGameTitle;
    
    void Update()
    {
        m_Timer = m_Timer + Time.deltaTime;
        if (m_Timer >= 0.5)
        {
            m_QuizEndOfGameTitle.GetComponent<Text>().enabled = true;
        }
        if (m_Timer >= 1)
        {
            m_QuizEndOfGameTitle.GetComponent<Text>().enabled = false;
            m_Timer = 0;
        }
    }
}
