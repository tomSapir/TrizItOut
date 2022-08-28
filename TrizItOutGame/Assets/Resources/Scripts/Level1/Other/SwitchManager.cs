using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour, IInteractable
{
    public delegate void OnSwitchAction(bool i_IsOn);

    public Sprite m_SwitchOnSprite;
    public Sprite m_SwitchOffSprite;
    public GameObject m_MonitorScreenSaver;
    public GameObject m_Dark;
    public GameObject m_ComputerScreen;
    public GameObject m_SafeBoxCanvas;
    private bool m_IsLightOn = false;

    public event OnSwitchAction OnSwitch;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        SoundManager.PlaySound(SoundManager.k_SwitchSoundName);
        if(m_IsLightOn)
        {
            GetComponent<SpriteRenderer>().sprite = m_SwitchOffSprite;
            m_Dark.SetActive(true);
            m_ComputerScreen.SetActive(false);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = m_SwitchOnSprite;
            m_ComputerScreen.SetActive(true);
            m_Dark.SetActive(false);
        }

        m_IsLightOn = !m_IsLightOn;

        m_SafeBoxCanvas.GetComponent<SafeBoxBtnManager>().OnSwitchChanged(m_IsLightOn);
        OnSwitch?.Invoke(m_IsLightOn);
    }
}
