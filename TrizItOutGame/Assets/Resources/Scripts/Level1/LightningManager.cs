using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public GameObject m_Lightning1, m_Lightning2;
    public GameObject m_ComputerCable;

    private float m_Timer;
    public static bool s_NeedToPlayElectricSound = false;
    public static bool s_TornComputerCablePickedUp = false;

    void Start()
    {
        m_ComputerCable.GetComponent<PickUpItem>().OnPickUp += OnTornComputerCablePickedUp;
        GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>().PowerFellOff += OnPowerFellOfFirstTime;
    }

    public void OnTornComputerCablePickedUp()
    {
        m_Lightning1.SetActive(false);
        m_Lightning2.SetActive(false);
        s_NeedToPlayElectricSound = false;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound();

        s_TornComputerCablePickedUp = true;
    }

    public void OnPowerFellOfFirstTime()
    {
        s_NeedToPlayElectricSound = true;
    }
}
