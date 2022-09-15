using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Button m_LeftBtn;
    public Button m_RightBtn;
    public GameObject m_TutorialWindow;
    public Text m_SlideNumberText;
    public GameObject m_Slide;
    public Text m_SlideTitle;
    public Sprite[] m_SlidesSprites;
    public string[] m_SlidesTitles;
    private int m_CurrentSlideIndex = 1;
    public GameObject m_ZoomInWindow;

    void Start()
    {
        m_Slide.GetComponent<SpriteRenderer>().sprite = m_SlidesSprites[0];
        m_SlideTitle.text = m_SlidesTitles[0];

        GameObject[] slots = GameObject.FindGameObjectsWithTag("slot");

        foreach(GameObject slot in slots)
        {
            slot.GetComponent<SlotManager>().OnClickZoomBtn += OnClickCloseTutorialBtn;
        }
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
        m_ZoomInWindow.SetActive(false);
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
        m_SlideTitle.text = m_SlidesTitles[CurrentSlideIndex - 1];
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
