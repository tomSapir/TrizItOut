using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsHandler : MonoBehaviour
{
    private bool m_PsuSolved = false;
    private bool m_FanSolved = false;
    private GameObject m_Inventory;

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
    }

    void Update()
    {
        checkIfFanAndPsuSolved();
    }

    public void OnFanStopped()
    {
        m_FanSolved = true;
    }

    public void OnPsuSolved()
    {
        m_PsuSolved = true;
        Debug.Log("PSU Solved!");
    }

    private void checkIfFanAndPsuSolved()
    {
        if(m_PsuSolved && m_FanSolved)
        {
            m_Inventory.GetComponent<InventoryManager>().RemoveFromInventory("Box_Of_PaperClips");
        }
    }
}
