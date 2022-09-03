using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeepHolesManager : MonoBehaviour
{
    public GameObject[] m_PeepHoles;
    public CommunicationManagerLevel2 m_CommunicationManager;
    private bool i_AlreadyPressedPeepHole = false;

    void Start()
    {
        for(int i = 0; i < m_PeepHoles.Length; i++)
        {
            m_PeepHoles[i].GetComponent<ChangeToMission>().MissionWasChosen += OnPeepHolePressed;
        }
    }

    private void OnPeepHolePressed(int i_MissionWallNumber)
    {
        if (!i_AlreadyPressedPeepHole)
        {
            m_CommunicationManager.ShowMsg("Looks like we can take a look around the room, maybe we can find something outside that will help.");
            i_AlreadyPressedPeepHole = true;
        }
    }
}