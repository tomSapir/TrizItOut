using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSoundManager : MonoBehaviour
{
    private float m_Timer;

    void Update()
    {
        if(!LightningManager.s_TornComputerCablePickedUp)
        {
            manageElectricSound();
        }       
    }

    private void manageElectricSound()
    {
        if (LightningManager.s_NeedToPlayElectricSound)
        {
            m_Timer = m_Timer + Time.deltaTime;

            if (m_Timer >= 2f)
            {
                SoundManager.PlaySound(SoundManager.k_ElectricitySoundName);
                m_Timer = 0;
            }
        }
        else
        {
            SoundManager.StopSound();
        }
    }
}
