using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraLightSwitch : MonoBehaviour, IInteractable
{
    public Sprite m_SwitchOnSprite;
    public Sprite m_SwitchOffSprite;
    public GameObject m_UltraVioletLighting;

    private bool m_IsSwitchOn = false;
    public delegate void SwitchIntercatedDelegate();
    public SwitchIntercatedDelegate OnInteractedHandler;



    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        SoundManager.PlaySound(SoundManager.k_SwitchSoundName);
        m_IsSwitchOn = !m_IsSwitchOn;
        GetComponent<SpriteRenderer>().sprite = m_IsSwitchOn is true ? m_SwitchOnSprite : m_SwitchOffSprite;
        m_UltraVioletLighting.GetComponent<SpriteRenderer>().enabled = m_IsSwitchOn;

        for(int i = 0; i < m_UltraVioletLighting.transform.childCount - 1; i++)
        {
            m_UltraVioletLighting.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = m_IsSwitchOn;
        }

        OnInteractedHandler?.Invoke();
    }
}
