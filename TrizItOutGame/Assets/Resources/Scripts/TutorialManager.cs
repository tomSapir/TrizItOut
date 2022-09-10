using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Button m_LeftBtn;
    public Button m_RightBtn;
    public GameObject m_TutorialWindow;
    public Text m_SlideNumberText;
    public GameObject m_Slide;
    public Sprite[] m_SlidesSprites;
    private int m_SlideIndex = 1;
    private int m_CurrentSlideIndex = 1;
    private int m_PreviousSlideIndex = 0;

    void Start()
    {
        m_Slide.GetComponent<SpriteRenderer>().sprite = m_SlidesSprites[0];
        
        

    }

    public int CurrentSlideIndex
    {
        get { return m_CurrentSlideIndex; }
        set
        {
            if(value == 5)
            {
                m_CurrentSlideIndex = 1;
            }
            else if(value == 0)
            {
                m_CurrentSlideIndex = 4;
            }
            else
            {
                m_CurrentSlideIndex = value;
            }
        }
    }

    public void OnClickShowTutorialBtn()
    {
        m_TutorialWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickLeftBtn()
    {
        CurrentSlideIndex--;
        updateSlideAndSlideNumber();
    }

    public void OnClickRightBtn()
    {
        CurrentSlideIndex++;
        updateSlideAndSlideNumber();
    }

    public void OnClickCloseTutorialBtn()
    {
        m_TutorialWindow.SetActive(false);
        Time.timeScale = 1f;
    }

    private void updateSlideAndSlideNumber()
    {
        m_SlideNumberText.text = CurrentSlideIndex + "/4";
        m_Slide.GetComponent<SpriteRenderer>().sprite = m_SlidesSprites[CurrentSlideIndex - 1];
        m_Slide.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;

        switch(CurrentSlideIndex)
        {
            case 1:
                {
                    m_Slide.GetComponent<SpriteRenderer>().size = new Vector2(490, 170);
                    break;
                }

            case 2:
                {
                    m_Slide.GetComponent<SpriteRenderer>().size = new Vector2(515, 170);
                    break;
                }

            case 3:
                {
                    m_Slide.GetComponent<SpriteRenderer>().size = new Vector2(515, 200);
                    break;
                }
            case 4:
                {
                    m_Slide.GetComponent<SpriteRenderer>().size = new Vector2(515, 180);
                    break;
                }
        }
    }
}
