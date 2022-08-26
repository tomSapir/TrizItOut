using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeBoxMission : MonoBehaviour
{
    [SerializeField]
    private Sprite m_SafeBoxOpenZoomSprite;

    [SerializeField]
    private GameObject m_ScrewDriver; 

    [SerializeField]
    public TextMeshProUGUI uiText;

    private DisplayManagerLevel1 m_DisplayManager;
    private bool m_SafeBoxOpend = false;

    [SerializeField]
    public GameObject m_CanvasOfButtons;

    public GameObject m_SafeBox_Closed;
    public GameObject m_SafeBox_Open;

    [SerializeField]
    private GameObject m_BackButton;

    void Start()
    {
        m_DisplayManager = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();

        if(m_DisplayManager ==  null)
        {
            Debug.LogError("DisplayManager in SafeBoxMission is null.");
        }
    }

    private void Update()
    {
        changeImage();
    }

    IEnumerator WaitIfPasswordCorrect(int sec)
    {
        m_DisplayManager.CurrentState = DisplayManagerLevel1.State.busy;
        uiText.color = Color.green;
        uiText.text = "Correct Password";
        yield return new WaitForSeconds(sec);
        SoundManager.PlaySound(SoundManager.k_DoorOpenSoundName);
        m_DisplayManager.GetComponent<SpriteRenderer>().sprite = m_SafeBoxOpenZoomSprite;
        m_ScrewDriver.SetActive(true);
        uiText.text = "";
        m_SafeBoxOpend = true;
        uiText.color = new Color(0.5f, 0.5f, 0.517f);
        Destroy(m_SafeBox_Closed);
        m_SafeBox_Open.SetActive(true);
        m_DisplayManager.CurrentState = DisplayManagerLevel1.State.zoom;
    }

    IEnumerator WaitIfPasswordInCorrect(int sec)
    {
        m_DisplayManager.CurrentState = DisplayManagerLevel1.State.busy;
        uiText.color = Color.red;
        uiText.text = "Wrong Password";
        yield return new WaitForSeconds(sec);
        uiText.text = "";
        uiText.color = new Color(0.5f, 0.5f, 0.517f);
        m_DisplayManager.CurrentState = DisplayManagerLevel1.State.zoom;
    }

    public void ApplyPasswordCorrect()
    {
        StartCoroutine(WaitIfPasswordCorrect(2));
    }

    public void ApplyPasswrodInCorrect()
    {
        StartCoroutine(WaitIfPasswordInCorrect(2));
    }

    private void changeImage()
    {
        if(m_SafeBoxOpend)
        {
            m_CanvasOfButtons.SetActive(false);
            m_DisplayManager.GetComponent<SpriteRenderer>().sprite = m_SafeBoxOpenZoomSprite;
        }
    }
}
