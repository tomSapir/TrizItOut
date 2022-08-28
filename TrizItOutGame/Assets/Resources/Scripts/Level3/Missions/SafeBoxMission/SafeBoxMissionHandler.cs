using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SafeBoxMissionHandler : MonoBehaviour
{
    private HashSet<int> m_currentSolution = new HashSet<int>();
    private readonly HashSet<int> m_ExpectedSolution = new HashSet<int>() { 1 };
    private GameObject m_Canvas;

    public GameObject m_SafeBox_Opened_Zoom;
    public GameObject m_SafeBox_Opened;

    public GameObject m_FlyTicket_Zoom;
    public GameObject m_FlyTicket;

    public GameObject m_Indicator;

    void Start()
    {
        m_Canvas = GameObject.Find("/MissionHandler/SafeBoxMission/Canvas");
        m_FlyTicket_Zoom.GetComponent<PickUpItem>().OnPickUp += OnTicketPickedUp;

    }

    private void OnTicketPickedUp()
    {
        Destroy(m_FlyTicket);
    }

    void Update()
    {

    }

    public void OnClick()
    {
        SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
        GameObject button = EventSystem.current.currentSelectedGameObject;
        Image image = button.GetComponent<Button>().image;
        image.color = new Color32(217, 91, 255, 152);
        m_currentSolution.Add(int.Parse(button.name));
    }

    public void SubmitSolution()
    {

        if (m_currentSolution.SetEquals(m_ExpectedSolution))
        {
            SoundManager.PlaySound(SoundManager.k_CorrectPasswordSoundName);
            m_Indicator.GetComponent<Image>().color = new Color32(12, 255, 0, 255);
            //Todo: corotine - wait 5 sec beffore opened
            enabeledSafeBox();
            enabledFlyTicket();
            m_Canvas.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            SoundManager.PlaySound(SoundManager.k_WorngPasswordSoundName);
            m_Indicator.GetComponent<Image>().color = new Color32(255, 0, 71, 255);
            m_currentSolution.Clear();
            for (int i = 0; i < m_Canvas.transform.childCount - 1; i++)
            {
                m_Canvas.transform.GetChild(i).gameObject.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
                //TODO: need to find a way to do this more clean
            }

        }
    }

    private void enabeledSafeBox()
    {
        m_SafeBox_Opened_Zoom.GetComponent<SpriteRenderer>().enabled = true;
        m_SafeBox_Opened.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void enabledFlyTicket()
    {
        m_FlyTicket_Zoom.GetComponent<SpriteRenderer>().enabled = true;
        m_FlyTicket_Zoom.layer = 0;
        m_FlyTicket.GetComponent<SpriteRenderer>().enabled = true;
    }
}
