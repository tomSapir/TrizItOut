using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMissionHandler : MonoBehaviour
{
    public static int s_AmountOfScrewsRemoved { get; set; } = 0;

    public GameObject m_PaperClip;
    public GameObject m_ZoomPaperClip;

    public GameObject m_FanRazers;
    public GameObject m_ZoomFanRazers;

    public GameObject m_Note;
    public GameObject m_ZoomNote;

    public GameObject m_ScrewHider;
    public GameObject m_BottomRightScrew;
    public GameObject m_TopLeftScrew;

    public GameObject m_TwoScrewsPrefab;

    private float m_Timer;

    void Start()
    {
        SoundManager.PlaySound(SoundManager.k_FanSoundName);
        m_ZoomFanRazers.GetComponent<FanRazersManager>().FanStopped += OnFanStopped;
        m_ZoomNote.GetComponent<PickUpItem>().OnPickUp += OnNotePickedUp;

        m_BottomRightScrew.GetComponent<Screw>().ScrewRemovedHandler += OnBottomRightScrewRemoved;
        m_BottomRightScrew.GetComponent<Screw>().ScrewRemovedHandler += OnScrewRemoved;
        m_TopLeftScrew.GetComponent<Screw>().ScrewRemovedHandler += OnScrewRemoved;
    }

    void Update()
    {
        if(FanRazersManager.m_NeedToSpin)
        {
            m_Timer = m_Timer + Time.deltaTime;

            if (m_Timer >= 1f)
            {
                SoundManager.PlaySound(SoundManager.k_FanSoundName);
                m_Timer = 0;
            }
        }
    }

    private void OnScrewRemoved()
    {

        s_AmountOfScrewsRemoved++;
        if (s_AmountOfScrewsRemoved == 2)
        {
            GameObject inventory = GameObject.Find("/Canvas/Inventory");
            if (inventory != null)
            {
                GameObject twoScrews = Instantiate(m_TwoScrewsPrefab);
                twoScrews.GetComponent<PickUpItem>().Interact(null);
            }
        }
    }

    private void OnFanStopped()
    {
        print("OnFanStopped");
        m_PaperClip.SetActive(true);
        m_ZoomPaperClip.SetActive(true);

        m_ZoomNote.layer = 0;
    }

    private void OnNotePickedUp()
    {
        Destroy(m_Note);
    }

    private void OnBottomRightScrewRemoved()
    {
        m_ScrewHider.SetActive(true);
    }
}
