using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour, IInteractable
{
    public delegate void OnSwitchAction(bool i_IsOn);

    public GameObject m_CommunicationInterface;
    public Sprite m_SwitchOnSprite;
    public Sprite m_SwitchOffSprite;
    public GameObject m_MonitorScreenSaver;
    public GameObject m_Dark;
    public GameObject m_ComputerScreen;
    public GameObject m_SafeBoxCanvas;
    private bool m_IsLightOn = false;
    private bool m_OnForTheFirstTime = false;

    public event OnSwitchAction OnSwitch;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(SoundManager.k_SwitchSoundName);

        if(m_IsLightOn)
        {
            GetComponent<SpriteRenderer>().sprite = m_SwitchOffSprite;
            m_Dark.SetActive(true);
        }
        else
        {
            if(!m_OnForTheFirstTime)
            {
                m_CommunicationInterface.GetComponent<CommunicationManagerLevel1>().ShowMsg("the light is on but the computer is still off");
                m_OnForTheFirstTime = true;
            }
            GetComponent<SpriteRenderer>().sprite = m_SwitchOnSprite;
            m_Dark.SetActive(false);
        }

        m_IsLightOn = !m_IsLightOn;

        m_SafeBoxCanvas.GetComponent<SafeBoxBtnManager>().OnSwitchChanged(m_IsLightOn);
        OnSwitch?.Invoke(m_IsLightOn);
    }
}
