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

    public event LoadingNextLevelDelegate LoadingNextLevel;

    void Start()
    {
        Game gameManager = GameObject.Find("GameManager").GetComponent<Game>();
        LoadingNextLevel += gameManager.OnLevelLoading;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int i_LevelIndex)
    {
        m_Text.text = "Level " + (i_LevelIndex - 1);
        m_Transition.SetTrigger("Start");

        yield return new WaitForSeconds(m_TransitionTime);

        SceneManager.LoadScene(i_LevelIndex);
        LoadingNextLevel?.Invoke(i_LevelIndex - 1);
    }
}
