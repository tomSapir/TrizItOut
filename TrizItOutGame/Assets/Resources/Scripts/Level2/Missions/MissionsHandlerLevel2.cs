using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsHandlerLevel2 : MonoBehaviour
{
    private bool m_PsuSolved = false;
    private bool m_StaticFanSolved = false;
    private bool m_BrokenFanSolved = false;
    private GameObject m_Inventory;

    public GameObject m_TvScreen1;
    public GameObject m_TvScreen2;

    void Start()
    {
        m_Inventory = GameObject.Find("Inventory");
        if(m_Inventory == null)
        {
            Debug.Log("m_Inventory is null");
        }

        GameObject fanRazers = GameObject.Find("Razers");
        fanRazers.GetComponent<FanRazersManager>().FanStopped += OnFanStopped;

        GameObject psu = GameObject.Find("PSU");
        psu.GetComponent<PSUManager>().PsuMissionSolved += OnPsuSolved;

        GameObject brokenFan = GameObject.Find("Broken_Fan");
        brokenFan.GetComponent<BrokenFanManager>().BrokenFanSolved += OnBrokenFanSolved;
    }

    void Update()
    {
        checkIfFanAndPsuSolved();
        checkIfBrokenFanAndPsuSolved();
    }

    private void checkIfBrokenFanAndPsuSolved()
    {
        if(m_BrokenFanSolved && m_PsuSolved)
        {
            m_TvScreen1.SetActive(true);
            m_TvScreen2.SetActive(true);
            BackgroundMusicManager.QuizStartWorking = true;
        }
    }

    public void OnFanStopped()
    {
        m_StaticFanSolved = true;
    }

    public void OnBrokenFanSolved()
    {
        m_BrokenFanSolved = true;
    }

    public void OnPsuSolved()
    {
        m_PsuSolved = true;
    }

    private void checkIfFanAndPsuSolved()
    {
        if(m_PsuSolved && m_StaticFanSolved)
        {
            m_Inventory.GetComponent<InventoryManager>().RemoveFromInventory("Box_Of_PaperClips");
        }
    }
}
