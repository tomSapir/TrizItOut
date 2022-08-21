using UnityEngine;

public class TvScreen2 : MonoBehaviour
{
    private float m_Timer;
    public GameObject m_QuizMiniTitle;

    void Update()
    {
        m_Timer = m_Timer + Time.deltaTime;
        if (m_Timer >= 0.5)
        {
            m_QuizMiniTitle.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (m_Timer >= 1)
        {
            m_QuizMiniTitle.GetComponent<SpriteRenderer>().enabled = true;
            m_Timer = 0;
        }
    }
}
