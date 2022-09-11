using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    public GameObject m_ComputerMission;
    public GameObject m_SafeBoxMission;
    public GameObject m_CommunicationInterface;
    public GameObject m_CommunicationWindow;

    public void ActiveRelevantMission(string i_Mission)
    {
        switch (i_Mission)
        {
            case "PCSide_ZoomIN_Close":
            case "PCSide_ZoomIn_Open":
                {
                    m_SafeBoxMission.SetActive(false);
                    m_ComputerMission.SetActive(true);  
                    break;
                }
            case "SafeBox_Code_Zoom":
            case "SafeBox_Open_ZoomIn":
                {
                    m_CommunicationWindow.SetActive(false);
                    GUI.enabled = true;
                    m_ComputerMission.SetActive(false);
                    m_SafeBoxMission.SetActive(true);
                    GUI.enabled = false;
                    break;
                }
        }
    }

    public void TurnOff()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
