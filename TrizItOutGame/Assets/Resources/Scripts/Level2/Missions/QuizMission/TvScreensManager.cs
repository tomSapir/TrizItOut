using UnityEngine;

public class TvScreensManager : MonoBehaviour
{
    private float m_Timer;
    public GameObject m_QuizMiniTitle;
    public GameObject m_ArrowDown1, m_ArrowDown2;

    void Update()
    {
        m_Timer = m_Timer + Time.deltaTime;
        if (m_Timer >= 0.5)
        {
            m_QuizMiniTitle.GetComponent<SpriteRenderer>().enabled = false;
            m_ArrowDown1.GetComponent<SpriteRenderer>().enabled = false;
            m_ArrowDown2.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (m_Timer >= 1)
        {
            m_QuizMiniTitle.GetComponent<SpriteRenderer>().enabled = true;
            m_ArrowDown1.GetComponent<SpriteRenderer>().enabled = true;
            m_ArrowDown2.GetComponent<SpriteRenderer>().enabled = true;
            m_Timer = 0;
        }
    }
}