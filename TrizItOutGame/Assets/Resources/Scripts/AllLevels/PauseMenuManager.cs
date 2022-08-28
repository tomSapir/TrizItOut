using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static bool s_GameIsPaused = false;
    public GameObject m_PauseMenuUI;
   
    void Start()
    {
        
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

    private void Pause()
    {
        m_PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        s_GameIsPaused = true;
    } 

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
}
