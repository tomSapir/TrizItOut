using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightingMissionHandler : MonoBehaviour
{
    public GameObject LockedPanel;
    public GameObject OpenedPanel;
    public GameObject UltraLighting;
    public TextMeshProUGUI HidingHint;

    static int s_AmountRedPressed = 0;
    static int s_AmountGreenPressed = 0;
    static int s_AmountBluePressed = 0;

    private SoundManager m_SoundManager;

    // Start is called before the first frame update
    void Start()
    {
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        LockedPanel.GetComponent<ClosedPanelManager>().panelOpenedHandler += onUnlockPanel;
        GameObject.Find("UltraLightSwitch").GetComponent<UltraLightSwitch>().OnInteractedHandler += checkColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onUnlockPanel()
    {
        OpenedPanel.layer = 0;
        Destroy(LockedPanel);
    }

    public void Red_OnClick()
    {
        m_SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
        rotateButton();

        Color32 color = UltraLighting.GetComponent<SpriteRenderer>().color;
        s_AmountRedPressed = (s_AmountRedPressed + 1) % 4;
        if(s_AmountRedPressed != 0)
        {
            color.r += 40;
        }
        else
        {
            color.r -= 120;
        }

        UltraLighting.GetComponent<SpriteRenderer>().color = color;
        checkColor();
    }

    public void Green_OnClick()
    {
        m_SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
        rotateButton();

        Color32 color = UltraLighting.GetComponent<SpriteRenderer>().color;
        s_AmountGreenPressed = (s_AmountGreenPressed + 1) % 4;
        if (s_AmountGreenPressed != 0)
        {
            color.g += 40;
        }
        else
        {
            color.g -= 120;
        }

        UltraLighting.GetComponent<SpriteRenderer>().color = color;
        checkColor();
    }

    public void Blue_OnClick()
    {
        m_SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
        rotateButton();

        Color32 color = UltraLighting.GetComponent<SpriteRenderer>().color;
        s_AmountBluePressed = (s_AmountBluePressed + 1) % 4;
        if (s_AmountBluePressed != 0)
        {
            color.b -= 40;
        }
        else
        {
            color.b += 120;
        }

        UltraLighting.GetComponent<SpriteRenderer>().color = color;
        checkColor();
    }

    private void rotateButton()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;

        button.transform.Rotate(new Vector3(0, 0, button.transform.rotation.z - 90));
    }

    private void checkColor()
    {
        if(s_AmountBluePressed == 1 && s_AmountGreenPressed == 2 && s_AmountRedPressed == 3 && UltraLighting.GetComponent<SpriteRenderer>().enabled == true)
        {
            HidingHint.enabled = true;
        }
        else
        {
            HidingHint.enabled = false;
        }
    }
}
