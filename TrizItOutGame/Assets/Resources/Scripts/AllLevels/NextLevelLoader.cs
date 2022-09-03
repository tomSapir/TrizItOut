using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelLoader : MonoBehaviour
{
    public delegate void LoadingNextLevelDelegate(int i_Level);

    public Animator m_Transition;
    public float m_TransitionTime = 1f;
    public Text m_Text;
    private int m_NextLevelIndex;
    public GameObject m_ContinueBtn;
    public GameObject m_QuitBtn;

    public event LoadingNextLevelDelegate LoadingNextLevel;

    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");

        if (gameManager != null)
        {
            LoadingNextLevel += gameManager.GetComponent<Game>().OnLevelLoading;
        }

        m_NextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        m_Text.text = "Level " + (m_NextLevelIndex - 1);
        m_Transition.SetTrigger("Start");
        m_ContinueBtn.SetActive(true);
        m_QuitBtn.SetActive(true);

        yield return new WaitForSeconds(m_TransitionTime);
        LoadingNextLevel?.Invoke(m_NextLevelIndex - 1);
    }

    public void OnClickContinueBtn()
    {
        SceneManager.LoadScene(m_NextLevelIndex);
    }

    public void OnClickQuitBtn()
    {
        SceneManager.LoadScene(0);
    }
}
