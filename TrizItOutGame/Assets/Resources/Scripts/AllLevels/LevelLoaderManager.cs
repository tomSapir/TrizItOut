using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderManager : MonoBehaviour
{
    public Slider m_Slider;
    public GameObject m_SliderText;
    public GameObject[] m_LevelsBtns;

    void Start()
    {
        setLevelsBtnsAppearance();
    }

    public void LoadLevel(int i_SceneIndex)
    {
        StartCoroutine(LoadAsynchronously(i_SceneIndex)); 
    }

    IEnumerator LoadAsynchronously(int i_SceneIndex)
    {
        setActiveAllLevelsBtns(false);
        m_Slider.gameObject.SetActive(true);
        m_Slider.value = 0;
        
        while (m_Slider.value != 1)
        {
            m_Slider.value += (float)0.004;
            m_SliderText.GetComponent<Text>().text = ((int)(m_Slider.value * 100)).ToString() + "%";
            yield return null;
        }

        m_SliderText.GetComponent<Text>().text = "100%";
        SceneManager.LoadScene(i_SceneIndex);
    }

    public void OnClickBackBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void onClickLevelOne()
    {
        DisplayManagerLevel1.m_AlreadyLoadingNextLevel = false;
    }

    private void setActiveAllLevelsBtns(bool i_Active)
    {
        foreach (GameObject levelBtn in m_LevelsBtns)
        {
            levelBtn.SetActive(i_Active);
        }
    }

    private void setLevelBtnAppearance(GameObject i_Btn, float i_Transparency, bool i_ShowLock, bool i_Interact)
    {
        Color currColor = i_Btn.GetComponent<Image>().color;
        Image lockImg = i_Btn.transform.GetChild(0).GetComponent<Image>();

        i_Btn.GetComponent<Image>().color = new Color(currColor.r, currColor.g, currColor.b, i_Transparency);
        lockImg.enabled = i_ShowLock;
        i_Btn.GetComponent<Button>().interactable = i_Interact;
    }

    private void setLevelsBtnsAppearance()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");

        if (gameManagerObject != null)
        {
            Game gameManager = gameManagerObject.GetComponent<Game>();
            int i = 0;

            foreach (GameObject levelBtn in m_LevelsBtns)
            {
                Image currLockImg = levelBtn.transform.GetChild(0).GetComponent<Image>();
                Color currColor = levelBtn.GetComponent<Image>().color;

                if (i < gameManager.ReachedLevel)
                {
                    setLevelBtnAppearance(levelBtn, 1f, false, true);
                }
                else
                {
                    setLevelBtnAppearance(levelBtn, 0.3f, true, false);
                }

                i++;
            }
        }
    }
}
