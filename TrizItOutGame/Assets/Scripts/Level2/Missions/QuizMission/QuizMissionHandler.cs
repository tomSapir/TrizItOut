using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizMissionHandler : MonoBehaviour
{
    public List<QuestionAndAnswer> m_QnA;
    public GameObject[] m_Options;
    public int m_CurrentQuestion;

    public Text m_QuestionText;

    public GameObject m_TrizGame;
    public GameObject m_TrizStartMenu;

    private void Start()
    {
        generateQuestion();
        setAnswers();
    }

    public void Correct()
    {
        m_QnA.RemoveAt(m_CurrentQuestion);
        generateQuestion();
        SoundManager.PlaySound(SoundManager.k_QuizCorrectAnswerSoundName);
    }

    public void Wrong()
    {
        Debug.Log("Wrong Answer");
        SoundManager.PlaySound(SoundManager.k_QuizWrongAnswerSoundName);
    }

    private void setAnswers()
    {
        for(int i = 0; i < m_Options.Length; i++)
        {
            m_Options[i].GetComponent<AnswerScript>().m_IsCorrect = false;
            m_Options[i].transform.GetChild(0).GetComponent<Text>().text = m_QnA[m_CurrentQuestion].m_Answers[i];

            if(m_QnA[m_CurrentQuestion].m_CurrectAnswer == i + 1)
            {
                m_Options[i].GetComponent<AnswerScript>().m_IsCorrect = true;
            }
        }
    }

    private void generateQuestion()
    {
        if(m_QnA.Count != 0)
        {
            m_CurrentQuestion = UnityEngine.Random.Range(0, m_QnA.Count);
            m_QuestionText.text = m_QnA[m_CurrentQuestion].m_Qustion;
            setAnswers();
        }
        else
        {
            Debug.Log("You won! going to level 3...");
        }
    }

    public void OnClickStartBtn()
    {
        m_TrizGame.SetActive(true);
        m_TrizStartMenu.SetActive(false);
    }
}
