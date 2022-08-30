using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager m_Instance;
    private AudioSource m_AudioSource;
    private int m_PrevSceneIndex = 0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        if(m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex != m_PrevSceneIndex)
        {
           // if (currentSceneIndex >= 2)
          //  {
          //      Destroy(this.gameObject);
          //  }
            /*
            switch(currentSceneIndex)
            {
                case 2:
                    {
                        m_AudioSource.volume = 0;
                        break;
                    }
                case 3:
                    {
                        // TODO: change this
                        m_AudioSource.volume = 0;
                        break;
                    }
                case 4:
                    {
                        // TODO: change this
                        m_AudioSource.volume = 0;
                        break;
                    }
            }
            
    */

            m_PrevSceneIndex = currentSceneIndex;
        }
    }

    public void OnToggleChanged()
    {
        m_AudioSource.mute = !m_AudioSource.mute;
    }
}
