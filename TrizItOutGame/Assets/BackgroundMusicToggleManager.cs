using UnityEngine;

public class BackgroundMusicToggleManager : MonoBehaviour
{
    public static BackgroundMusicToggleManager m_Instance;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
