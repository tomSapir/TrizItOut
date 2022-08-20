using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject m_SecHand;
    public GameObject m_MinHand;
    public GameObject m_HourHand;

    private string m_OldSeconds;

    void Update()
    {
        string seconds = System.DateTime.Now.ToString("ss");

        if(seconds != m_OldSeconds)
        {
            UpdateTime();
        }

        m_OldSeconds = seconds;
    }

    private void UpdateTime()
    {
        int secondsInt = int.Parse(System.DateTime.Now.ToString("ss"));
        int minutesInt = int.Parse(System.DateTime.Now.ToString("mm"));
        int hoursInt = int.Parse(System.DateTime.Now.ToString("hh"));
        float hourDistance = (float)(minutesInt) / 60f;

        iTween.RotateTo(m_SecHand, iTween.Hash("z", secondsInt * 6 * -1, "time", 1, "easetype", "easeOutQuint"));
        iTween.RotateTo(m_MinHand, iTween.Hash("z", minutesInt * 6 * -1, "time", 1, "easetype", "easeOutElastic"));
        iTween.RotateTo(m_HourHand, iTween.Hash("z", (hoursInt + hourDistance) * 360 / 12 * -1, "time", 1, "easetype", "easeOutQuint"));
    }
}
