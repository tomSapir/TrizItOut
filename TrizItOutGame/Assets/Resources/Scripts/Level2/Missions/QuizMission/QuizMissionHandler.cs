using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMissionHandler : MonoBehaviour
{
    public List<QuestionAndAnswer> m_QuestionsAndAnswers;
    public GameObject[] m_AnswersOptions;
    public int m_CurrentQuestion;
    public Text m_QuestionText;
    public GameObject m_QuizStartMenu;
    public GameObject m_QuizGame;
    public GameObject m_QuizEndOfGame;
 
    private void Start()
    {
        generateQuestion();
        setAnswers();
    }

    public void Correct()
    {
        GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel2>().ShowMsg("YES! Good job!");
        m_QuestionsAndAnswers.RemoveAt(m_CurrentQuestion);
        generateQuestion();
        SoundManager.PlaySound(SoundManager.k_QuizCorrectAnswerSoundName);
    }

    public void Wrong()
    {
        GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel2>().ShowMsg("Wrong answer, try again.");
        SoundManager.PlaySound(SoundManager.k_QuizWrongAnswerSoundName);
    }

    private void setAnswers()
    {
        for(int i = 0; i < m_AnswersOptions.Length; i++)
        {
            m_AnswersOptions[i].GetComponent<AnswerScript>().m_IsCorrect = false;
            m_AnswersOptions[i].transform.GetChild(0).GetComponent<Text>().text = m_QuestionsAndAnswers[m_CurrentQuestion].m_Answers[i];
            if(m_QuestionsAndAnswers[m_CurrentQuestion].m_CurrectAnswer == i + 1)
            {
                m_AnswersOptions[i].GetComponent<AnswerScript>().m_IsCorrect = true;
            }
        }
    }

    private void generateQuestion()
    {
        if(m_QuestionsAndAnswers.Count != 0)
        {
            m_CurrentQuestion = UnityEngine.Random.Range(0, m_QuestionsAndAnswers.Count);
            m_QuestionText.text = m_QuestionsAndAnswers[m_CurrentQuestion].m_Qustion;
            setAnswers();
        }
        else
        {
            Debug.Log("Going to enter WIN");
            handleWin();
        }
    }

    public void OnClickStartBtn()
    {
        m_QuizGame.SetActive(true);
        m_QuizStartMenu.SetActive(false);
    }

    private void handleWin()
    {
        m_QuizGame.SetActive(false);
        m_QuizEndOfGame.SetActive(true);
    }

    public void OnClickContinueBtn()
    {
        NextLevelLoader nextLevelLoader = GameObject.Find("Next_Level_Loader").GetComponent<NextLevelLoader>();
        nextLevelLoader.LoadNextLevel();
    }
}
