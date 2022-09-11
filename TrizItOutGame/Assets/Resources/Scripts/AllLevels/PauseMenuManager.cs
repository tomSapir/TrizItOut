using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager m_Instance;
    public static bool s_GameIsPaused = false;
    public GameObject m_PauseMenuUI;

    public Toggle m_BackgroundMusicToggle;
    public BackgroundMusicManager m_BackgroundMusicManager;
    public GameObject m_BackgroundNoisesToggle;
   
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

        GameObject backgroundMusic = GameObject.Find("Background_Music_Manager");
        if(backgroundMusic != null)
        {
            m_BackgroundMusicManager = backgroundMusic.GetComponent<BackgroundMusicManager>();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(s_GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        m_PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        s_GameIsPaused = false;
    }

    public void Pause()
    {
        m_PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        s_GameIsPaused = true;
    } 

    public void OnClickMainMenuBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OnBackgroundMusicToggleChanged()
    {
        if(m_BackgroundMusicManager != null)
        {
            m_BackgroundMusicManager.OnToggleChanged();
        }
    }
}
