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


    // Start is called before the first frame update
    void Start()
    {
        LockedPanel.GetComponent<ClosedPanelManager>().panelOpenedHandler += onUnlockPanel;
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
        SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
        GameObject button = EventSystem.current.currentSelectedGameObject;

        button.transform.Rotate(new Vector3(0, 0, button.transform.rotation.z + 90));
        Color32 color = UltraLighting.GetComponent<SpriteRenderer>().color;
        s_AmountRedPressed = (s_AmountRedPressed + 1) % 4;
        if(s_AmountRedPressed != 0)
        {
            color.r += 20;
        }
        else
        {
            color.r -= 60;
        }

        UltraLighting.GetComponent<SpriteRenderer>().color = color;


    }
}
