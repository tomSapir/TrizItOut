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
        Game gameManager = GameObject.Find("GameManager").GetComponent<Game>();
        int reachedLevel = gameManager.ReachedLevel;

        for (int i = 0; i < m_LevelsBtns.Length; i++)
        {
            Debug.Log(i < reachedLevel);
            m_LevelsBtns[i].GetComponent<Button>().interactable = i < reachedLevel;
        }
    }

    public void LoadLevel(int i_SceneIndex)
    {
        StartCoroutine(LoadAsynchronously(i_SceneIndex)); 
    }

    IEnumerator LoadAsynchronously(int i_SceneIndex)
    {
        foreach(GameObject levelBtn in m_LevelsBtns)
        {
            levelBtn.SetActive(false);
        }

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

}
