using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderManager : MonoBehaviour
{
    public Slider m_Slider;
    public GameObject m_SliderText;

    public GameObject[] m_LevelsBtns;


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
            m_Slider.value += (float)0.008;
            m_SliderText.GetComponent<TMPro.TextMeshProUGUI>().text = ((int)(m_Slider.value * 100)).ToString() + "%";

            yield return null;
        }

        SceneManager.LoadScene(i_SceneIndex);
    }

}
