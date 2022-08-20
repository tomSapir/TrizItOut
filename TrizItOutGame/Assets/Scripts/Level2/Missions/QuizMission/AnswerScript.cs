using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool m_IsCorrect = false;
    public QuizMissionHandler m_QuizManager;

    public void Answer()
    {
        if(m_IsCorrect)
        {
            m_QuizManager.Correct();
        }
        else
        {
            m_QuizManager.Wrong();
        }
    }
}
