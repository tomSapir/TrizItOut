using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButtonManager : MonoBehaviour
{ 
    public Sprite m_MutedSprite;
    public Sprite m_NotMutedSprite;

    public void OnClickMuteButton()
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        SoundManager.s_IsMuted = !SoundManager.s_IsMuted;
        soundManager.StopSound();

        if(SoundManager.s_IsMuted)
        {
            gameObject.GetComponent<Image>().sprite = m_MutedSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = m_NotMutedSprite;
        }
    }
}
