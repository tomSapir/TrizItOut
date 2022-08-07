using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip s_ButtonSound, s_WorngPasswordSound, s_CorrectPasswordSound;
    public static AudioClip s_SwitchSound, s_SpraySound, s_DoorOpenSound, s_TakeItemSound;
    public static AudioClip s_DrawerSound;

    public static AudioSource audioSource;

    public const string k_ButtonSoundName = "buttonSound";
    public const string k_WorngPasswordSoundName = "worngPasswordSound";
    public const string k_CorrectPasswordSoundName = "correctPasswordSound";
    public const string k_SwitchSoundName = "switchSound";
    public const string k_SpraySoundName = "spraySound";
    public const string k_DoorOpenSoundName = "doorOpenSound";
    public const string k_TakeItemSoundName = "takeItemSound";
    public const string k_DrawerSound = "drawerSound";

    public static readonly string sr_SoundPath = "Sounds/";

    void Start()
    {
        s_ButtonSound = Resources.Load<AudioClip>(sr_SoundPath + k_ButtonSoundName);
        s_WorngPasswordSound = Resources.Load<AudioClip>(sr_SoundPath + k_WorngPasswordSoundName);
        s_CorrectPasswordSound = Resources.Load<AudioClip>(sr_SoundPath + k_CorrectPasswordSoundName);
        s_SwitchSound = Resources.Load<AudioClip>(sr_SoundPath + k_SwitchSoundName);
        s_SpraySound = Resources.Load<AudioClip>(sr_SoundPath + k_SpraySoundName);
        s_DoorOpenSound = Resources.Load<AudioClip>(sr_SoundPath + k_DoorOpenSoundName);
        s_TakeItemSound = Resources.Load<AudioClip>(sr_SoundPath + k_TakeItemSoundName);
        s_DrawerSound = Resources.Load<AudioClip>(sr_SoundPath + k_DrawerSound);

        audioSource = GetComponent<AudioSource>();
    }


    public static void PlaySound(string i_Clip)
    {
        switch(i_Clip)
        {
            case k_ButtonSoundName:
                {
                    audioSource.PlayOneShot(s_ButtonSound);
                    break;
                }
            case k_WorngPasswordSoundName:
                {
                    audioSource.PlayOneShot(s_WorngPasswordSound);
                    break;
                }
            case k_CorrectPasswordSoundName:
                {
                    audioSource.PlayOneShot(s_CorrectPasswordSound);
                    break;
                }
            case k_SwitchSoundName:
                {
                    audioSource.PlayOneShot(s_SwitchSound);
                    break;
                }
            case k_SpraySoundName:
                {
                    audioSource.PlayOneShot(s_SpraySound);
                    break;
                }
            case k_DoorOpenSoundName:
                {
                    audioSource.PlayOneShot(s_DoorOpenSound);
                    break;
                }
            case k_TakeItemSoundName:
                {
                    audioSource.PlayOneShot(s_TakeItemSound);
                    break;
                }
            case k_DrawerSound:
                {
                    audioSource.PlayOneShot(s_DrawerSound);
                    break;
                }
        }
    }
}

// TODO:
// - how to add drawer sound?

