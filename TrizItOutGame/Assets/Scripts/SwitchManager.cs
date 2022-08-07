using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour, IInteractable
{
    public delegate void OnSwitchAction(bool i_IsOn);

    [SerializeField]
    private Sprite m_SwitchOnSprite;
    [SerializeField]
    private Sprite m_SwitchOffSprite;

    [SerializeField]
    private GameObject m_Dark;

    private bool m_IsLightOn = false;

    public event OnSwitchAction OnSwitch;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        SoundManager.PlaySound(SoundManager.k_SwitchSoundName);
        if(m_IsLightOn)
        {
            GetComponent<SpriteRenderer>().sprite = m_SwitchOffSprite;
            m_Dark.SetActive(true);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = m_SwitchOnSprite;
            m_Dark.SetActive(false);
        }

        m_IsLightOn = !m_IsLightOn;

        OnSwitch?.Invoke(m_IsLightOn);
    }
}
